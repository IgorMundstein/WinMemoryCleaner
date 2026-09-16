using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Text;

namespace WinMemoryCleaner.Engine
{
    /// <summary>Result of an optimization run for a single memory area.</summary>
    internal sealed class AreaResult
    {
        public string Name { get; set; }
        public double DurationSeconds { get; set; }
        public string Error { get; set; }
        public bool Success => string.IsNullOrEmpty(Error);
    }

    /// <summary>
    /// Faithful port of the memory-optimization logic from the original
    /// Service/ComputerService.cs. The native call sequences are unchanged; only the
    /// presentation (progress reporting, logging) is decoupled from WPF.
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal sealed class Optimizer
    {
        private readonly OsInfo _os;
        private readonly IReadOnlyCollection<string> _processExclusionList;

        /// <summary>Raised as each area begins: (stepIndex, areaName).</summary>
        public event Action<int, string> OnProgress;

        public Optimizer(OsInfo os, IEnumerable<string> processExclusionList)
        {
            _os = os;
            _processExclusionList = new HashSet<string>(
                processExclusionList ?? Enumerable.Empty<string>(),
                StringComparer.OrdinalIgnoreCase);
        }

        public IReadOnlyList<AreaResult> Optimize(MemoryAreas areas)
        {
            var results = new List<AreaResult>();
            if (areas == MemoryAreas.None)
                return results;

            var step = 0;
            var sw = new Stopwatch();

            void Run(string name, Action action)
            {
                OnProgress?.Invoke(++step, name);
                sw.Restart();
                var result = new AreaResult { Name = name };
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    result.Error = GetMessage(e);
                }
                finally
                {
                    result.DurationSeconds = sw.Elapsed.TotalSeconds;
                    results.Add(result);
                }
            }

            // Order preserved from the original ComputerService.Optimize.
            if ((areas & MemoryAreas.WorkingSet) != 0)
                Run("Working Set", OptimizeWorkingSet);

            if ((areas & MemoryAreas.SystemFileCache) != 0)
                Run("System File Cache", OptimizeSystemFileCache);

            if ((areas & MemoryAreas.ModifiedPageList) != 0)
                Run("Modified Page List", OptimizeModifiedPageList);

            if ((areas & (MemoryAreas.StandbyList | MemoryAreas.StandbyListLowPriority)) != 0)
            {
                var lowPriority = (areas & MemoryAreas.StandbyListLowPriority) != 0;
                Run(lowPriority ? "Standby List (low priority)" : "Standby List",
                    () => OptimizeStandbyList(lowPriority));
            }

            if ((areas & MemoryAreas.CombinedPageList) != 0)
                Run("Combined Page List", OptimizeCombinedPageList);

            if ((areas & MemoryAreas.RegistryCache) != 0)
                Run("Registry Cache", OptimizeRegistryCache);

            if ((areas & MemoryAreas.ModifiedFileCache) != 0)
                Run("Modified File Cache", OptimizeModifiedFileCache);

            // Garbage Collector step (mirrors App.ReleaseMemory in the original).
            OnProgress?.Invoke(++step, "Garbage Collector");
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch { /* ignored */ }

            return results;
        }

        #region Privilege / volume helpers (ported verbatim)

        private static Microsoft.Win32.SafeHandles.SafeFileHandle OpenVolumeHandle(string driveLetter)
        {
            if (string.IsNullOrWhiteSpace(driveLetter))
                return null;

            return NativeMethods.CreateFile(
                @"\\.\" + driveLetter.TrimEnd(':', '\\') + ":",
                FileAccess.ReadWrite,
                FileShare.Read | FileShare.Write,
                IntPtr.Zero,
                FileMode.Open,
                (int)FileAttributes.Normal | Constants.Windows.File.FlagsNoBuffering,
                IntPtr.Zero);
        }

        private static bool SetIncreasePrivilege(string privilegeName)
        {
            var result = false;

            using (var current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
            {
                Structs.Windows.TokenPrivileges newState;
                newState.Count = 1;
                newState.Luid = 0L;
                newState.Attr = Constants.Windows.PrivilegeAttribute.Enabled;

                if (NativeMethods.LookupPrivilegeValue(null, privilegeName, ref newState.Luid))
                    result = NativeMethods.AdjustTokenPrivileges(current.Token, false, ref newState, 0, IntPtr.Zero, IntPtr.Zero);
            }

            return result;
        }

        #endregion

        #region Area routines (ported verbatim from ComputerService)

        private void OptimizeCombinedPageList()
        {
            if (!_os.HasCombinedPageList)
                throw new Exception("Combined Page List optimization is not supported on this operating system.");

            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
                throw new Exception("Administrator privilege required: " + Constants.Windows.Privilege.SeProfSingleProcessName);

            var handle = GCHandle.Alloc(0);
            try
            {
                var info = new Structs.Windows.MemoryCombineInformationEx();
                handle = GCHandle.Alloc(info, GCHandleType.Pinned);

                if (NativeMethods.NtSetSystemInformation(
                        Constants.Windows.SystemInformationClass.SystemCombinePhysicalMemoryInformation,
                        handle.AddrOfPinnedObject(),
                        (uint)Marshal.SizeOf(info)) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            finally
            {
                FreeHandle(ref handle);
            }
        }

        private void OptimizeModifiedFileCache()
        {
            if (!_os.HasModifiedFileCache)
                throw new Exception("Modified File Cache optimization is not supported on this operating system.");

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive == null || drive.DriveType != DriveType.Fixed || string.IsNullOrWhiteSpace(drive.Name))
                    continue;

                using (var handle = OpenVolumeHandle(drive.Name))
                {
                    if (handle == null || handle.IsInvalid)
                        continue;

                    int bytesReturned;

                    if (_os.IsWindows7OrGreater)
                    {
                        try
                        {
                            var buffer = Marshal.AllocHGlobal(1);
                            try
                            {
                                if (!NativeMethods.DeviceIoControl(handle, Constants.Windows.Drive.IoControlResetWriteOrder,
                                        buffer, 1, IntPtr.Zero, 0, out bytesReturned, IntPtr.Zero))
                                    throw new Win32Exception(Marshal.GetLastWin32Error());
                            }
                            finally
                            {
                                Marshal.FreeHGlobal(buffer);
                            }
                        }
                        catch { /* ignored */ }

                        if (_os.IsWindows8OrGreater)
                        {
                            try
                            {
                                if (!NativeMethods.DeviceIoControl(handle, Constants.Windows.Drive.FsctlDiscardVolumeCache,
                                        IntPtr.Zero, 0, IntPtr.Zero, 0, out bytesReturned, IntPtr.Zero))
                                    throw new Win32Exception(Marshal.GetLastWin32Error());
                            }
                            catch { /* ignored */ }
                        }
                    }

                    if (!NativeMethods.FlushFileBuffers(handle))
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        private void OptimizeModifiedPageList()
        {
            if (!_os.HasModifiedPageList)
                throw new Exception("Modified Page List optimization is not supported on this operating system.");

            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
                throw new Exception("Administrator privilege required: " + Constants.Windows.Privilege.SeProfSingleProcessName);

            var handle = GCHandle.Alloc(Constants.Windows.SystemMemoryListCommand.MemoryFlushModifiedList, GCHandleType.Pinned);
            try
            {
                if (NativeMethods.NtSetSystemInformation(
                        Constants.Windows.SystemInformationClass.SystemMemoryListInformation,
                        handle.AddrOfPinnedObject(),
                        (uint)Marshal.SizeOf(Constants.Windows.SystemMemoryListCommand.MemoryFlushModifiedList)) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            finally
            {
                FreeHandle(ref handle);
            }
        }

        private void OptimizeRegistryCache()
        {
            if (!_os.HasRegistryHive)
                throw new Exception("Registry Cache optimization is not supported on this operating system.");

            if (NativeMethods.NtSetSystemInformation(
                    Constants.Windows.SystemInformationClass.SystemRegistryReconciliationInformation,
                    IntPtr.Zero, 0) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        private void OptimizeStandbyList(bool lowPriority = false)
        {
            if (!_os.HasStandbyList)
                throw new Exception("Standby List optimization is not supported on this operating system.");

            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
                throw new Exception("Administrator privilege required: " + Constants.Windows.Privilege.SeProfSingleProcessName);

            object command = lowPriority
                ? Constants.Windows.SystemMemoryListCommand.MemoryPurgeLowPriorityStandbyList
                : Constants.Windows.SystemMemoryListCommand.MemoryPurgeStandbyList;

            var handle = GCHandle.Alloc(command, GCHandleType.Pinned);
            try
            {
                if (NativeMethods.NtSetSystemInformation(
                        Constants.Windows.SystemInformationClass.SystemMemoryListInformation,
                        handle.AddrOfPinnedObject(),
                        (uint)Marshal.SizeOf(command)) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            finally
            {
                FreeHandle(ref handle);
            }
        }

        private void OptimizeSystemFileCache()
        {
            if (!_os.HasSystemFileCache)
                throw new Exception("System File Cache optimization is not supported on this operating system.");

            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeIncreaseQuotaName))
                throw new Exception("Administrator privilege required: " + Constants.Windows.Privilege.SeIncreaseQuotaName);

            var handle = GCHandle.Alloc(0);
            try
            {
                object info;
                if (_os.Is64Bit)
                    info = new Structs.Windows.SystemFileCacheInformation64 { MinimumWorkingSet = -1L, MaximumWorkingSet = -1L };
                else
                    info = new Structs.Windows.SystemFileCacheInformation32 { MinimumWorkingSet = int.MaxValue, MaximumWorkingSet = int.MaxValue };

                handle = GCHandle.Alloc(info, GCHandleType.Pinned);

                if (NativeMethods.NtSetSystemInformation(
                        Constants.Windows.SystemInformationClass.SystemFileCacheInformation,
                        handle.AddrOfPinnedObject(),
                        (uint)Marshal.SizeOf(info)) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            finally
            {
                FreeHandle(ref handle);
            }

            var fileCacheSize = IntPtr.Subtract(IntPtr.Zero, 1); // Flush
            if (!NativeMethods.SetSystemFileCacheSize(fileCacheSize, fileCacheSize, 0))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        private void OptimizeWorkingSet()
        {
            if (!_os.HasWorkingSet)
                throw new Exception("Working Set optimization is not supported on this operating system.");

            var errors = new StringBuilder();

            if (_processExclusionList.Count > 0)
            {
                if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeDebugName))
                    throw new Exception("Administrator privilege required: " + Constants.Windows.Privilege.SeDebugName);

                var processes = Process.GetProcesses()
                    .Where(p => p != null && !_processExclusionList.Contains(p.ProcessName));

                foreach (var process in processes)
                {
                    using (process)
                    {
                        try
                        {
                            if (!NativeMethods.EmptyWorkingSet(process.Handle))
                                throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                        catch (InvalidOperationException) { /* ignored */ }
                        catch (Win32Exception e)
                        {
                            if (e.NativeErrorCode != Constants.Windows.SystemErrorCode.ErrorAccessDenied)
                                errors.Append(process.ProcessName + ": " + GetMessage(e) + " | ");
                        }
                    }
                }

                if (errors.Length > 3)
                    errors.Remove(errors.Length - 3, 3);
            }
            else
            {
                if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
                    throw new Exception("Administrator privilege required: " + Constants.Windows.Privilege.SeProfSingleProcessName);

                var handle = GCHandle.Alloc(Constants.Windows.SystemMemoryListCommand.MemoryEmptyWorkingSets, GCHandleType.Pinned);
                try
                {
                    if (NativeMethods.NtSetSystemInformation(
                            Constants.Windows.SystemInformationClass.SystemMemoryListInformation,
                            handle.AddrOfPinnedObject(),
                            (uint)Marshal.SizeOf(Constants.Windows.SystemMemoryListCommand.MemoryEmptyWorkingSets)) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    {
                        var e = new Win32Exception(Marshal.GetLastWin32Error());
                        if (errors.Length > 0) errors.Append(" | ");
                        errors.Append(GetMessage(e));
                    }
                }
                finally
                {
                    FreeHandle(ref handle);
                }
            }

            if (errors.Length > 0)
                throw new Exception(errors.ToString());
        }

        #endregion

        private static void FreeHandle(ref GCHandle handle)
        {
            try
            {
                if (handle.IsAllocated)
                    handle.Free();
            }
            catch (InvalidOperationException) { /* ignored */ }
        }

        internal static string GetMessage(Exception e)
        {
            var message = e?.Message ?? string.Empty;
            var inner = e?.InnerException;
            while (inner != null)
            {
                message += " " + inner.Message;
                inner = inner.InnerException;
            }
            return message.Trim();
        }
    }
}
