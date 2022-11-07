using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Computer Service
    /// </summary>
    internal class ComputerService : IComputerService
    {
        #region Fields

        private readonly Memory _memory;
        private readonly Structs.Windows.MemoryStatusEx _memoryStatusEx;
        private OperatingSystem _operatingSystem;

        #endregion

        #region Constructor

        internal ComputerService()
        {
            _memory = new Memory();
            _memoryStatusEx = new Structs.Windows.MemoryStatusEx();
        }

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
                _memory.Available = _memoryStatusEx.ullAvailPhys.ByteSizeToString();
                _memory.Total = _memoryStatusEx.ullTotalPhys.ByteSizeToString();
                _memory.Usage = _memoryStatusEx.dwMemoryLoad;
            }

            return _memory;
        }

        /// <summary>
        /// Gets the operating system info
        /// </summary>
        /// <returns></returns>
        public OperatingSystem GetOperatingSystem()
        {
            return _operatingSystem ?? (_operatingSystem = new OperatingSystem
            {
                Is64Bit = Environment.Is64BitOperatingSystem,
                IsWindows8OrAbove = Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6.2,
                IsWindowsVistaOrAbove = Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6,
                IsWindowsXp64BitOrAbove = Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 5.2
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

                // Retrieves the LUID used on a specified system to locally represent the specified privilege name
                if (NativeMethods.LookupPrivilegeValue(null, privilegeName, ref newState.Luid))
                {
                    // Enables or disables privileges in a specified access token
                    result = NativeMethods.AdjustTokenPrivileges(current.Token, false, ref newState, 0, IntPtr.Zero, IntPtr.Zero);
                }
            }

            if (!result)
                Logger.Error(new Win32Exception(Marshal.GetLastWin32Error()));

            return result;
        }

        #endregion

        #region Methods (Memory)

        /// <summary>
        /// Cleans the computer memory
        /// </summary>
        /// <param name="areas">The areas.</param>
        public void MemoryClean(Enums.Memory.Area areas)
        {
            // Clean Processes Working Set
            if ((areas & Enums.Memory.Area.ProcessesWorkingSet) != 0)
            {
                try
                {
                    MemoryCleanProcessesWorkingSet();

                    Logger.Information(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Localization.MemoryProcessesWorkingSet, Localization.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            // Clean System Working Set
            if ((areas & Enums.Memory.Area.SystemWorkingSet) != 0)
            {
                try
                {
                    MemoryCleanSystemWorkingSet();

                    Logger.Information(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Localization.MemorySystemWorkingSet, Localization.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            // Clean Modified Page List
            if ((areas & Enums.Memory.Area.ModifiedPageList) != 0)
            {
                try
                {
                    MemoryCleanModifiedPageList();

                    Logger.Information(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Localization.MemoryModifiedPageList, Localization.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            // Clean Standby List
            if ((areas & (Enums.Memory.Area.StandbyList | Enums.Memory.Area.StandbyListLowPriority)) != 0)
            {
                try
                {
                    var lowPriority = (areas & Enums.Memory.Area.StandbyListLowPriority) != 0;

                    MemoryCleanStandbyList(lowPriority);

                    Logger.Information(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", lowPriority ? Localization.MemoryLowPriorityStandbyList : Localization.MemoryStandbyList, Localization.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            // Clean Combined Page List
            if ((areas & Enums.Memory.Area.CombinedPageList) != 0)
            {
                try
                {
                    MemoryCleanCombinedPageList();

                    Logger.Information(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Localization.MemoryCombinedPageList, Localization.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            // Garbage Collector
            MemoryGarbageCollection();
        }

        /// <summary>
        /// Cleans the combined page list.
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        private void MemoryCleanCombinedPageList()
        {
            // Windows minimum version
            if (!GetOperatingSystem().HasCombinedPageList)
            {
                Logger.Error(string.Format(CultureInfo.CurrentCulture, Localization.ErrorFeatureIsNotSupported, Localization.MemoryCombinedPageList));
                return;
            }

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
            {
                Logger.Error(string.Format(CultureInfo.CurrentCulture, Localization.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeProfSingleProcessName));
                return;
            }

            GCHandle handle = GCHandle.Alloc(0);

            try
            {
                Structs.Windows.MemoryCombineInformationEx memoryCombineInformationEx = new Structs.Windows.MemoryCombineInformationEx();

                handle = GCHandle.Alloc(memoryCombineInformationEx, GCHandleType.Pinned);

                int length = Marshal.SizeOf(memoryCombineInformationEx);

                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemInformationClass.SystemCombinePhysicalMemoryInformation, handle.AddrOfPinnedObject(), length) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            catch (Win32Exception e)
            {
                Logger.Error(e);
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
            {
                Logger.Error(string.Format(CultureInfo.CurrentCulture, Localization.ErrorFeatureIsNotSupported, Localization.MemoryModifiedPageList));
                return;
            }

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
            {
                Logger.Error(string.Format(CultureInfo.CurrentCulture, Localization.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeProfSingleProcessName));
                return;
            }

            GCHandle handle = GCHandle.Alloc(Constants.Windows.SystemMemoryListCommand.MemoryFlushModifiedList, GCHandleType.Pinned);

            try
            {
                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemInformationClass.SystemMemoryListInformation, handle.AddrOfPinnedObject(), Marshal.SizeOf(Constants.Windows.SystemMemoryListCommand.MemoryFlushModifiedList)) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            catch (Win32Exception e)
            {
                Logger.Error(e);
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
            if (!GetOperatingSystem().HasProcessWorkingSet)
            {
                Logger.Error(string.Format(CultureInfo.CurrentCulture, Localization.ErrorFeatureIsNotSupported, Localization.MemoryProcessesWorkingSet));
                return;
            }

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeDebugName))
            {
                Logger.Error(string.Format(CultureInfo.CurrentCulture, Localization.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeDebugName));
                return;
            }

            var processes = Process.GetProcesses().Where(process => process != null);

            foreach (Process process in processes)
            {
                using (process)
                {
                    try
                    {
                        if (!process.HasExited && NativeMethods.EmptyWorkingSet(process.Handle) == 0)
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                    catch (Win32Exception e)
                    {
                        if (e.NativeErrorCode != Constants.Windows.SystemErrorCode.ErrorAccessDenied)
                            Logger.Error(e);
                    }
                }
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
            {
                Logger.Error(string.Format(CultureInfo.CurrentCulture, Localization.ErrorFeatureIsNotSupported, Localization.MemoryStandbyList));
                return;
            }

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
            {
                Logger.Error(string.Format(CultureInfo.CurrentCulture, Localization.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeProfSingleProcessName));
                return;
            }

            object memoryPurgeStandbyList = lowPriority ? Constants.Windows.SystemMemoryListCommand.MemoryPurgeLowPriorityStandbyList : Constants.Windows.SystemMemoryListCommand.MemoryPurgeStandbyList;
            GCHandle handle = GCHandle.Alloc(memoryPurgeStandbyList, GCHandleType.Pinned);

            try
            {
                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemInformationClass.SystemMemoryListInformation, handle.AddrOfPinnedObject(), Marshal.SizeOf(memoryPurgeStandbyList)) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            catch (Win32Exception e)
            {
                Logger.Error(e);
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
            {
                Logger.Error(string.Format(CultureInfo.CurrentCulture, Localization.ErrorFeatureIsNotSupported, Localization.MemorySystemWorkingSet));
                return;
            }

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeIncreaseQuotaName))
            {
                Logger.Error(string.Format(CultureInfo.CurrentCulture, Localization.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeIncreaseQuotaName));
                return;
            }

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
            catch (Win32Exception e)
            {
                Logger.Error(e);
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

            try
            {
                IntPtr fileCacheSize = IntPtr.Subtract(IntPtr.Zero, 1); // Flush

                if (!NativeMethods.SetSystemFileCacheSize(fileCacheSize, fileCacheSize, 0))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            catch (Win32Exception e)
            {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// Forces garbage collection.
        /// </summary>
        private void MemoryGarbageCollection()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch
            {
                // ignored
            }
        }

        #endregion
    }
}
