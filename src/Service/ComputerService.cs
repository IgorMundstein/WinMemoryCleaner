using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Computer Service
    /// </summary>
    internal class ComputerService : IComputerService
    {
        #region Fields

        private readonly Memory _memory = new Memory();
        private readonly Structs.Windows.MemoryStatusEx _memoryStatusEx = new Structs.Windows.MemoryStatusEx();
        private OperatingSystem _operatingSystem;

        #endregion

        #region Methods (Computer)

        /// <summary>
        /// Gets the memory info
        /// </summary>
        /// <returns></returns>
        public Memory GetMemory()
        {
            if (!NativeMethods.GlobalMemoryStatusEx(_memoryStatusEx))
                Logger.Debug(new Win32Exception(Marshal.GetLastWin32Error()).GetBaseException().Message);
            else
            {
                _memory.Free = _memoryStatusEx.ullAvailPhys.ByteSizeToString();
                _memory.FreePercentage = 100 - _memoryStatusEx.dwMemoryLoad;
                _memory.Total = _memoryStatusEx.ullTotalPhys.ByteSizeToString();
                _memory.Used = (_memoryStatusEx.ullTotalPhys - _memoryStatusEx.ullAvailPhys).ByteSizeToString();
                _memory.UsedPercentage = _memoryStatusEx.dwMemoryLoad;
            }

            return _memory;
        }

        /// <summary>
        /// Gets the operating system info
        /// </summary>
        /// <returns></returns>
        public OperatingSystem GetOperatingSystem()
        {
            var operatingSystem = Environment.OSVersion;

            return _operatingSystem ?? (_operatingSystem = new OperatingSystem
            {
                Is64Bit = Environment.Is64BitOperatingSystem,
                IsWindows8OrGreater = operatingSystem.Version.Major >= 6.2,
                IsWindowsVistaOrGreater = operatingSystem.Version.Major >= 6,
                IsWindowsXpOrGreater = operatingSystem.Version.Major >= 5.1
            });
        }

        /// <summary>
        /// Increase the Privilege using a privilege name
        /// </summary>
        /// <param name="privilegeName">The name of the privilege that needs to be increased</param>
        /// <returns></returns>
        private bool SetIncreasePrivilege(string privilegeName)
        {
            bool result = false;

            using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
            {
                Structs.Windows.TokenPrivileges newState;
                newState.Count = 1;
                newState.Luid = 0L;
                newState.Attr = Constants.Windows.PrivilegeAttribute.Enabled;

                // Retrieves the uid used on a specified system to locally represent the specified privilege name
                if (NativeMethods.LookupPrivilegeValue(null, privilegeName, ref newState.Luid))
                {
                    // Enables or disables privileges in a specified access token
                    result = NativeMethods.AdjustTokenPrivileges(current.Token, false, ref newState, 0, IntPtr.Zero, IntPtr.Zero);
                }

                if (!result)
                    Logger.Error(new Win32Exception(Marshal.GetLastWin32Error()));
            }

            return result;
        }

        #endregion

        #region Methods (Memory)

        /// <summary>
        /// Cleans the computer memory
        /// </summary>
        /// <param name="areas">The areas.</param>
        public void CleanMemory(Enums.Memory.Area areas)
        {
            if (areas == Enums.Memory.Area.None)
                return;

            var errorLog = new StringBuilder();
            var errorLogFormat = "{0} ({1}: {2})";
            var infoLog = new StringBuilder();
            var infoLogFormat = "{0} ({1}) ({2:0.0} {3})";
            var runtime = new TimeSpan();
            var stopwatch = new Stopwatch();

            // Clean Processes Working Set
            if ((areas & Enums.Memory.Area.ProcessesWorkingSet) != 0)
            {
                try
                {
                    stopwatch.Restart();

                    MemoryCleanProcessesWorkingSet();

                    runtime = runtime.Add(stopwatch.Elapsed);

                    infoLog.AppendLine(string.Format(Localizer.Culture, infoLogFormat, Localizer.String.MemoryProcessesWorkingSet, Localizer.String.Optimized, stopwatch.Elapsed.TotalSeconds, Localizer.String.Seconds.ToLower(Localizer.Culture)));
                }
                catch (Exception e)
                {
                    errorLog.AppendLine(string.Format(Localizer.Culture, errorLogFormat, Localizer.String.MemoryProcessesWorkingSet, Localizer.String.Error, e.GetBaseException().Message));
                }
            }

            // Clean System Working Set
            if ((areas & Enums.Memory.Area.SystemWorkingSet) != 0)
            {
                try
                {
                    stopwatch.Restart();

                    MemoryCleanSystemWorkingSet();

                    runtime = runtime.Add(stopwatch.Elapsed);

                    infoLog.AppendLine(string.Format(Localizer.Culture, infoLogFormat, Localizer.String.MemorySystemWorkingSet, Localizer.String.Optimized, stopwatch.Elapsed.TotalSeconds, Localizer.String.Seconds.ToLower(Localizer.Culture)));
                }
                catch (Exception e)
                {
                    errorLog.AppendLine(string.Format(Localizer.Culture, errorLogFormat, Localizer.String.MemorySystemWorkingSet, Localizer.String.Error, e.GetBaseException().Message));
                }
            }

            // Clean Modified Page List
            if ((areas & Enums.Memory.Area.ModifiedPageList) != 0)
            {
                try
                {
                    stopwatch.Restart();

                    MemoryCleanModifiedPageList();

                    runtime = runtime.Add(stopwatch.Elapsed);

                    infoLog.AppendLine(string.Format(Localizer.Culture, infoLogFormat, Localizer.String.MemoryModifiedPageList, Localizer.String.Optimized, stopwatch.Elapsed.TotalSeconds, Localizer.String.Seconds.ToLower(Localizer.Culture)));
                }
                catch (Exception e)
                {
                    errorLog.AppendLine(string.Format(Localizer.Culture, errorLogFormat, Localizer.String.MemoryModifiedPageList, Localizer.String.Error, e.GetBaseException().Message));
                }
            }

            // Clean Standby List
            if ((areas & (Enums.Memory.Area.StandbyList | Enums.Memory.Area.StandbyListLowPriority)) != 0)
            {
                var lowPriority = (areas & Enums.Memory.Area.StandbyListLowPriority) != 0;

                try
                {
                    stopwatch.Restart();

                    MemoryCleanStandbyList(lowPriority);

                    runtime = runtime.Add(stopwatch.Elapsed);

                    infoLog.AppendLine(string.Format(Localizer.Culture, infoLogFormat, lowPriority ? Localizer.String.MemoryStandbyListLowPriority : Localizer.String.MemoryStandbyList, Localizer.String.Optimized, stopwatch.Elapsed.TotalSeconds, Localizer.String.Seconds.ToLower(Localizer.Culture)));
                }
                catch (Exception e)
                {
                    errorLog.AppendLine(string.Format(Localizer.Culture, errorLogFormat, lowPriority ? Localizer.String.MemoryStandbyListLowPriority : Localizer.String.MemoryStandbyList, Localizer.String.Error, e.GetBaseException().Message));
                }
            }

            // Clean Combined Page List
            if ((areas & Enums.Memory.Area.CombinedPageList) != 0)
            {
                try
                {
                    stopwatch.Restart();

                    MemoryCleanCombinedPageList();

                    runtime = runtime.Add(stopwatch.Elapsed);

                    infoLog.AppendLine(string.Format(Localizer.Culture, infoLogFormat, Localizer.String.MemoryCombinedPageList, Localizer.String.Optimized, stopwatch.Elapsed.TotalSeconds, Localizer.String.Seconds.ToLower(Localizer.Culture)));
                }
                catch (Exception e)
                {
                    errorLog.AppendLine(string.Format(Localizer.Culture, errorLogFormat, Localizer.String.MemoryCombinedPageList, Localizer.String.Error, e.GetBaseException().Message));
                }
            }

            // Log
            if (infoLog.Length > 0)
            {
                infoLog.Insert(0, string.Format(Localizer.Culture, "{0} ({1:0.0} {2}){3}{3}", Localizer.String.MemoryAreas.ToUpper(Localizer.Culture), runtime.TotalSeconds, Localizer.String.Seconds.ToLower(Localizer.Culture), Environment.NewLine));

                Logger.Information(infoLog.ToString());

                infoLog.Clear();
            }

            if (errorLog.Length > 0)
            {
                errorLog.Insert(0, string.Format(Localizer.Culture, "{0}{1}{1}", Localizer.String.MemoryAreas.ToUpper(Localizer.Culture), Environment.NewLine));

                Logger.Error(errorLog.ToString());

                errorLog.Clear();
            }

            // Garbage Collector
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Cleans the combined page list.
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        private void MemoryCleanCombinedPageList()
        {
            // Windows minimum version
            if (!GetOperatingSystem().HasCombinedPageList)
                throw new Exception(string.Format(Localizer.Culture, Localizer.String.ErrorFeatureIsNotSupported, Localizer.String.MemoryCombinedPageList));

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
                throw new Exception(string.Format(Localizer.Culture, Localizer.String.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeProfSingleProcessName));

            GCHandle handle = GCHandle.Alloc(0);

            try
            {
                Structs.Windows.MemoryCombineInformationEx memoryCombineInformationEx = new Structs.Windows.MemoryCombineInformationEx();

                handle = GCHandle.Alloc(memoryCombineInformationEx, GCHandleType.Pinned);

                int length = Marshal.SizeOf(memoryCombineInformationEx);

                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemInformationClass.SystemCombinePhysicalMemoryInformation, handle.AddrOfPinnedObject(), length) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            finally
            {
                try
                {
                    if (handle.IsAllocated)
                        handle.Free();
                }
                catch (InvalidOperationException)
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Clean the modified page list.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// </exception>
        private void MemoryCleanModifiedPageList()
        {
            // Windows minimum version
            if (!GetOperatingSystem().HasModifiedPageList)
                throw new Exception(string.Format(Localizer.Culture, Localizer.String.ErrorFeatureIsNotSupported, Localizer.String.MemoryModifiedPageList));

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
                throw new Exception(string.Format(Localizer.Culture, Localizer.String.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeProfSingleProcessName));


            GCHandle handle = GCHandle.Alloc(Constants.Windows.SystemMemoryListCommand.MemoryFlushModifiedList, GCHandleType.Pinned);

            try
            {
                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemInformationClass.SystemMemoryListInformation, handle.AddrOfPinnedObject(), Marshal.SizeOf(Constants.Windows.SystemMemoryListCommand.MemoryFlushModifiedList)) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            finally
            {
                try
                {
                    if (handle.IsAllocated)
                        handle.Free();
                }
                catch (InvalidOperationException)
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Cleans the processes working set.
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        private void MemoryCleanProcessesWorkingSet()
        {
            // Windows minimum version
            if (!GetOperatingSystem().HasProcessesWorkingSet)
                throw new Exception(string.Format(Localizer.Culture, Localizer.String.ErrorFeatureIsNotSupported, Localizer.String.MemoryProcessesWorkingSet));

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeDebugName))
                throw new Exception(string.Format(Localizer.Culture, Localizer.String.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeDebugName));

            var errors = new StringBuilder();
            var processes = Process.GetProcesses().Where(process => process != null && !Settings.ProcessExclusionList.Contains(process.ProcessName));

            foreach (var process in processes)
            {
                using (process)
                {
                    try
                    {
                        if (!NativeMethods.EmptyWorkingSet(process.Handle))
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                    catch (Win32Exception e)
                    {
                        if (e.NativeErrorCode != Constants.Windows.SystemErrorCode.ErrorAccessDenied)
                            errors.Append(string.Format(Localizer.Culture, "{0}: {1} | ", process.ProcessName, e.GetBaseException().Message));
                    }
                }
            }

            if (errors.Length > 3)
            {
                errors.Remove(errors.Length - 3, 3);
                throw new Exception(errors.ToString());
            }
        }

        /// <summary>
        /// Cleans the standby list.
        /// </summary>
        /// <param name="lowPriority">if set to <c>true</c> [low priority].</param>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        /// <exception cref="Win32Exception"></exception>
        private void MemoryCleanStandbyList(bool lowPriority = false)
        {
            // Windows minimum version
            if (!GetOperatingSystem().HasStandbyList)
                throw new Exception(string.Format(Localizer.Culture, Localizer.String.ErrorFeatureIsNotSupported, Localizer.String.MemoryStandbyList));

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
                throw new Exception(string.Format(Localizer.Culture, Localizer.String.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeProfSingleProcessName));

            object memoryPurgeStandbyList = lowPriority ? Constants.Windows.SystemMemoryListCommand.MemoryPurgeLowPriorityStandbyList : Constants.Windows.SystemMemoryListCommand.MemoryPurgeStandbyList;
            GCHandle handle = GCHandle.Alloc(memoryPurgeStandbyList, GCHandleType.Pinned);

            try
            {
                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemInformationClass.SystemMemoryListInformation, handle.AddrOfPinnedObject(), Marshal.SizeOf(memoryPurgeStandbyList)) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            finally
            {
                try
                {
                    if (handle.IsAllocated)
                        handle.Free();
                }
                catch (InvalidOperationException)
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Cleans the system working set.
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// </exception>
        /// <exception cref="Win32Exception"></exception>
        private void MemoryCleanSystemWorkingSet()
        {
            // Windows minimum version
            if (!GetOperatingSystem().HasSystemWorkingSet)
                throw new Exception(string.Format(Localizer.Culture, Localizer.String.ErrorFeatureIsNotSupported, Localizer.String.MemorySystemWorkingSet));

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeIncreaseQuotaName))
                throw new Exception(string.Format(Localizer.Culture, Localizer.String.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeIncreaseQuotaName));

            GCHandle handle = GCHandle.Alloc(0);

            try
            {
                object systemCacheInformation;

                if (GetOperatingSystem().Is64Bit)
                    systemCacheInformation = new Structs.Windows.SystemCacheInformation64 { MinimumWorkingSet = -1L, MaximumWorkingSet = -1L };
                else
                    systemCacheInformation = new Structs.Windows.SystemCacheInformation32 { MinimumWorkingSet = uint.MaxValue, MaximumWorkingSet = uint.MaxValue };

                handle = GCHandle.Alloc(systemCacheInformation, GCHandleType.Pinned);

                int length = Marshal.SizeOf(systemCacheInformation);

                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemInformationClass.SystemFileCacheInformation, handle.AddrOfPinnedObject(), length) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            finally
            {
                try
                {
                    if (handle.IsAllocated)
                        handle.Free();
                }
                catch (InvalidOperationException)
                {
                    // ignored
                }
            }

            IntPtr fileCacheSize = IntPtr.Subtract(IntPtr.Zero, 1); // Flush

            if (!NativeMethods.SetSystemFileCacheSize(fileCacheSize, fileCacheSize, 0))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        #endregion
    }
}
