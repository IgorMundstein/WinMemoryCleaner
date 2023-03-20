using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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

        private readonly Memory _memory;
        private readonly Structs.Windows.MemoryStatusEx _memoryStatusEx;
        private OperatingSystem _operatingSystem;

        #endregion

        #region Constructors

        public ComputerService()
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
            StringBuilder errorLog = new StringBuilder();
            StringBuilder infoLog = new StringBuilder();

            // Clean Processes Working Set
            if ((areas & Enums.Memory.Area.ProcessesWorkingSet) != 0)
            {
                try
                {
                    MemoryCleanProcessesWorkingSet();

                    infoLog.AppendLine(string.Format("- {0} ({1})", Localization.MemoryProcessesWorkingSet, Localization.Completed.ToUpper()));
                }
                catch (Exception e)
                {
                    errorLog.AppendLine(string.Format("- {0} ({1}: {2})", Localization.MemoryProcessesWorkingSet, Localization.Error.ToUpper(), e.GetBaseException().Message));
                }
            }

            // Clean System Working Set
            if ((areas & Enums.Memory.Area.SystemWorkingSet) != 0)
            {
                try
                {
                    MemoryCleanSystemWorkingSet();

                    infoLog.AppendLine(string.Format("- {0} ({1})", Localization.MemorySystemWorkingSet, Localization.Completed.ToUpper()));
                }
                catch (Exception e)
                {
                    errorLog.AppendLine(string.Format("- {0} ({1}: {2})", Localization.MemorySystemWorkingSet, Localization.Error.ToUpper(), e.GetBaseException().Message));
                }
            }

            // Clean Modified Page List
            if ((areas & Enums.Memory.Area.ModifiedPageList) != 0)
            {
                try
                {
                    MemoryCleanModifiedPageList();

                    infoLog.AppendLine(string.Format("- {0} ({1})", Localization.MemoryModifiedPageList, Localization.Completed.ToUpper()));
                }
                catch (Exception e)
                {
                    errorLog.AppendLine(string.Format("- {0} ({1}: {2})", Localization.MemoryModifiedPageList, Localization.Error.ToUpper(), e.GetBaseException().Message));
                }
            }

            // Clean Standby List
            if ((areas & (Enums.Memory.Area.StandbyList | Enums.Memory.Area.StandbyListLowPriority)) != 0)
            {
                var lowPriority = (areas & Enums.Memory.Area.StandbyListLowPriority) != 0;

                try
                {
                    MemoryCleanStandbyList(lowPriority);

                    infoLog.AppendLine(string.Format("- {0} ({1})", lowPriority ? Localization.MemoryStandbyListLowPriority : Localization.MemoryStandbyList, Localization.Completed.ToUpper()));
                }
                catch (Exception e)
                {
                    errorLog.AppendLine(string.Format("- {0} ({1}: {2})", lowPriority ? Localization.MemoryStandbyListLowPriority : Localization.MemoryStandbyList, Localization.Error.ToUpper(), e.GetBaseException().Message));
                }
            }

            // Clean Combined Page List
            if ((areas & Enums.Memory.Area.CombinedPageList) != 0)
            {
                try
                {
                    MemoryCleanCombinedPageList();

                    infoLog.AppendLine(string.Format("- {0} ({1})", Localization.MemoryCombinedPageList, Localization.Completed.ToUpper()));
                }
                catch (Exception e)
                {
                    errorLog.AppendLine(string.Format("- {0} ({1}: {2})", Localization.MemoryCombinedPageList, Localization.Error.ToUpper(), e.GetBaseException().Message));
                }
            }

            // Log
            if (infoLog.Length > 0)
            {
                infoLog.Insert(0, string.Format("{0}{1}{1}", Localization.MemoryCleanReport.ToUpper(), Environment.NewLine));

                Logger.Information(infoLog.ToString());

                infoLog.Clear();
            }

            if (errorLog.Length > 0)
            {
                errorLog.Insert(0, string.Format("{0}{1}{1}", Localization.MemoryCleanReport.ToUpper(), Environment.NewLine));

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
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Localization.ErrorFeatureIsNotSupported, Localization.MemoryCombinedPageList));

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Localization.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeProfSingleProcessName));

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
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Localization.ErrorFeatureIsNotSupported, Localization.MemoryModifiedPageList));

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Localization.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeProfSingleProcessName));


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
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Localization.ErrorFeatureIsNotSupported, Localization.MemoryProcessesWorkingSet));

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeDebugName))
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Localization.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeDebugName));

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
                            errors.Append(string.Format("{0}: {1} | ", process.ProcessName, e.GetBaseException().Message));
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
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Localization.ErrorFeatureIsNotSupported, Localization.MemoryStandbyList));

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeProfSingleProcessName))
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Localization.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeProfSingleProcessName));

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
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Localization.ErrorFeatureIsNotSupported, Localization.MemorySystemWorkingSet));

            // Check privilege
            if (!SetIncreasePrivilege(Constants.Windows.Privilege.SeIncreaseQuotaName))
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Localization.ErrorAdminPrivilegeRequired, Constants.Windows.Privilege.SeIncreaseQuotaName));

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
