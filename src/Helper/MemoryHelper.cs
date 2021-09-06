using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using WinMemoryCleaner.Properties;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Memory Helper
    /// </summary>
    internal static class MemoryHelper
    {
        /// <summary>
        /// Memory clean
        /// </summary>
        /// <param name="areas">Memory areas</param>
        internal static void Clean(Enums.Memory.Area areas)
        {
            // Clean Processes Working Set
            if (areas.HasFlag(Enums.Memory.Area.ProcessesWorkingSet))
            {
                try
                {
                    CleanProcessesWorkingSet();

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperProcessesWorkingSet, Resources.LogCleaned.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }

            // Clean System Working Set
            if (areas.HasFlag(Enums.Memory.Area.SystemWorkingSet))
            {
                try
                {
                    CleanSystemWorkingSet();

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperSystemWorkingSet, Resources.LogCleaned.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }

            // Clean Modified Page List
            if (areas.HasFlag(Enums.Memory.Area.ModifiedPageList))
            {
                try
                {
                    CleanModifiedPageList();

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperModifiedPageList, Resources.LogCleaned.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }

            // Clean Standby List
            if (areas.HasFlag(Enums.Memory.Area.StandbyList) || areas.HasFlag(Enums.Memory.Area.StandbyListLowPriority))
            {
                try
                {
                    CleanStandbyList(areas.HasFlag(Enums.Memory.Area.StandbyListLowPriority));

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Settings.MemoryAreas.HasFlag(Enums.Memory.Area.StandbyListLowPriority) ? Resources.MemoryHelperLowPriorityStandbyList : Resources.MemoryHelperStandbyList, Resources.LogCleaned.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }

            // Clean Combined Page List
            if (areas.HasFlag(Enums.Memory.Area.CombinedPageList))
            {
                try
                {
                    CleanCombinedPageList();

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperCombinedPageList, Resources.LogCleaned.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }
        }
        
        /// <summary>
        /// Cleans the combined page list.
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        private static void CleanCombinedPageList()
        {
            // Windows minimum version
            if (!ComputerHelper.IsWindows8OrAbove)
            {
                LogHelper.Error(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperFeatureIsNotSupported, Resources.MemoryHelperCombinedPageList));
                return;
            }

            // Check privilege
            if (!ComputerHelper.SetIncreasePrivilege(Constants.Windows.ProfileSingleProcessName))
            {
                LogHelper.Error(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperAdminPrivilegeRequired, Constants.Windows.ProfileSingleProcessName));
                return;
            }

            GCHandle handle = GCHandle.Alloc(0);

            try
            {
                Structs.Windows.MemoryCombineInformationEx memoryCombineInformationEx = new Structs.Windows.MemoryCombineInformationEx();

                handle = GCHandle.Alloc(memoryCombineInformationEx, GCHandleType.Pinned);

                int length = Marshal.SizeOf(memoryCombineInformationEx);

                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemCombinePhysicalMemoryInformation, handle.AddrOfPinnedObject(), length) != (int)Enums.Windows.SystemErrorCode.ERROR_SUCCESS)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            catch (Win32Exception e)
            {
                LogHelper.Error(e);
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
        private static void CleanModifiedPageList()
        {
            // Windows minimum version
            if (!ComputerHelper.IsWindowsVistaOrAbove)
            {
                LogHelper.Error(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperFeatureIsNotSupported, Resources.MemoryHelperModifiedPageList));
                return;
            }

            // Check privilege
            if (!ComputerHelper.SetIncreasePrivilege(Constants.Windows.ProfileSingleProcessName))
            {
                LogHelper.Error(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperAdminPrivilegeRequired, Constants.Windows.ProfileSingleProcessName));
                return;
            }

            GCHandle handle = GCHandle.Alloc(Constants.Windows.MemoryFlushModifiedList, GCHandleType.Pinned);

            try
            {
                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemMemoryListInformation, handle.AddrOfPinnedObject(), Marshal.SizeOf(Constants.Windows.MemoryFlushModifiedList)) != (int)Enums.Windows.SystemErrorCode.ERROR_SUCCESS)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            catch (Win32Exception e)
            {
                LogHelper.Error(e);
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
        /// Cleans the standby list.
        /// </summary>
        /// <param name="lowPriority">if set to <c>true</c> [low priority].</param>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        /// <exception cref="Win32Exception"></exception>
        private static void CleanStandbyList(bool lowPriority = false)
        {
            // Windows minimum version
            if (!ComputerHelper.IsWindowsVistaOrAbove)
            {
                LogHelper.Error(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperFeatureIsNotSupported, Resources.MemoryHelperStandbyList));
                return;
            }

            // Check privilege
            if (!ComputerHelper.SetIncreasePrivilege(Constants.Windows.ProfileSingleProcessName))
            {
                LogHelper.Error(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperAdminPrivilegeRequired, Constants.Windows.ProfileSingleProcessName));
                return;
            }

            object memoryPurgeStandbyList = lowPriority ? Constants.Windows.MemoryPurgeLowPriorityStandbyList : Constants.Windows.MemoryPurgeStandbyList;
            GCHandle handle = GCHandle.Alloc(memoryPurgeStandbyList, GCHandleType.Pinned);

            try
            {
                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemMemoryListInformation, handle.AddrOfPinnedObject(), Marshal.SizeOf(memoryPurgeStandbyList)) != (int)Enums.Windows.SystemErrorCode.ERROR_SUCCESS)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            catch (Win32Exception e)
            {
                LogHelper.Error(e);
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
        private static void CleanSystemWorkingSet()
        {
            // Windows minimum version
            if (!ComputerHelper.IsWindowsVistaOrAbove)
            {
                LogHelper.Error(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperFeatureIsNotSupported, Resources.MemoryHelperSystemWorkingSet));
                return;
            }

            // Check privilege
            if (!ComputerHelper.SetIncreasePrivilege(Constants.Windows.IncreaseQuotaName))
            {
                LogHelper.Error(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperAdminPrivilegeRequired, Constants.Windows.IncreaseQuotaName));
                return;
            }

            GCHandle handle = GCHandle.Alloc(0);

            try
            {
                object systemCacheInformation;

                if (ComputerHelper.Is64Bit)
                    systemCacheInformation = new Structs.Windows.SystemCacheInformation64 { MinimumWorkingSet = -1L, MaximumWorkingSet = -1L };
                else
                    systemCacheInformation = new Structs.Windows.SystemCacheInformation32 { MinimumWorkingSet = uint.MaxValue, MaximumWorkingSet = uint.MaxValue };

                handle = GCHandle.Alloc(systemCacheInformation, GCHandleType.Pinned);

                int length = Marshal.SizeOf(systemCacheInformation);

                if (NativeMethods.NtSetSystemInformation(Constants.Windows.SystemFileCacheInformation, handle.AddrOfPinnedObject(), length) != (int)Enums.Windows.SystemErrorCode.ERROR_SUCCESS)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            catch (Win32Exception e)
            {
                LogHelper.Error(e);
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
                LogHelper.Error(e);
            }
        }
        
        /// <summary>
        /// Cleans the processes working set.
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        private static void CleanProcessesWorkingSet()
        {
            // Windows minimum version
            if (!ComputerHelper.IsWindowsVistaOrAbove)
            {
                LogHelper.Error(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperFeatureIsNotSupported, Resources.MemoryHelperProcessesWorkingSet));
                return;
            }

            // Check privilege
            if (!ComputerHelper.SetIncreasePrivilege(Constants.Windows.DebugPrivilege))
            {
                LogHelper.Error(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperAdminPrivilegeRequired, Constants.Windows.DebugPrivilege));
                return;
            }

            foreach (Process process in Process.GetProcesses().Where(process => process != null))
            {
                try
                {
                    using (process)
                    {
                        if (!process.HasExited && NativeMethods.EmptyWorkingSet(process.Handle) == 0)
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                catch (InvalidOperationException)
                {
                    // ignored
                }
                catch (Win32Exception e)
                {
                    if (e.NativeErrorCode != (int)Enums.Windows.SystemErrorCode.ERROR_ACCESS_DENIED)
                        LogHelper.Error(e);
                }
            }
        }
    }
}
