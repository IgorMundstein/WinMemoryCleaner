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
        /// <param name="emptyWorkingSets">if set to <c>true</c> [empty working sets].</param>
        /// <param name="emptySystemWorkingSet">if set to <c>true</c> [empty system working set].</param>
        /// <param name="emptyModifiedPageList">if set to <c>true</c> [empty modified page list].</param>
        /// <param name="emptyStandbyList">if set to <c>true</c> [empty standby list].</param>
        /// <param name="emptyLowPriorityStandbyList">if set to <c>true</c> [empty low priority standby list].</param>
        /// <param name="combineMemoryList">if set to <c>true</c> [combine memory list].</param>
        /// <param name="garbageCollection">if set to <c>true</c> [garbage collection].</param>
        /// <remarks>
        /// - Empty Working Sets
        /// - Empty System Working Set
        /// - Empty Modified Page List
        /// - Empty Standby List
        /// - Combine Memory List
        /// - Garbage Collection
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Logged")]
        internal static void Clean(bool emptyWorkingSets = true, bool emptySystemWorkingSet = true, bool emptyModifiedPageList = true, bool emptyStandbyList = true, bool emptyLowPriorityStandbyList = true, bool combineMemoryList = true, bool garbageCollection = true)
        {
            // Empty Working Sets
            if (emptyWorkingSets)
            {
                try
                {
                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperEmptyWorkingSets, Resources.Started.ToUpper(CultureInfo.CurrentCulture)));

                    EmptyWorkingSets();

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperEmptyWorkingSets, Resources.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }

            // Empty System Working Set
            if (emptySystemWorkingSet)
            {
                try
                {
                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperEmptySystemWorkingSet, Resources.Started.ToUpper(CultureInfo.CurrentCulture)));

                    EmptySystemWorkingSet();

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperEmptySystemWorkingSet, Resources.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }

            // Empty Modified Page List
            if (emptyModifiedPageList)
            {
                try
                {
                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperEmptyModifiedPageList, Resources.Started.ToUpper(CultureInfo.CurrentCulture)));

                    EmptyModifiedPageList();

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperEmptyModifiedPageList, Resources.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }

            // Empty Standby List
            if (emptyStandbyList)
            {
                try
                {
                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperEmptyStandbyList, Resources.Started.ToUpper(CultureInfo.CurrentCulture)));

                    EmptyStandbyList(emptyLowPriorityStandbyList);

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperEmptyStandbyList, Resources.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }

            // Combine Memory List
            if (combineMemoryList)
            {
                try
                {
                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperCombineMemoryList, Resources.Started.ToUpper(CultureInfo.CurrentCulture)));

                    CombineMemoryList();

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperCombineMemoryList, Resources.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }

            // Garbage Collection
            if (garbageCollection)
            {
                try
                {
                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperGarbageCollection, Resources.Started.ToUpper(CultureInfo.CurrentCulture)));

                    GarbageCollection();

                    LogHelper.Info(string.Format(CultureInfo.CurrentCulture, "{0} ({1})", Resources.MemoryHelperGarbageCollection, Resources.Completed.ToUpper(CultureInfo.CurrentCulture)));
                }
                catch (Exception e)
                {
                    LogHelper.Error(e);
                }
            }
        }

        /// <summary>
        /// Combines the memory list.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// </exception>
        private static void CombineMemoryList()
        {
            // Windows minimum version
            if (!ComputerHelper.IsWindows10OrAbove)
            {
                LogHelper.Warning(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperFeatureIsNotSupported, Resources.MemoryHelperCombineMemoryList));
                return;
            }

            if (ComputerHelper.SetIncreasePrivilege(Constants.Windows.ProfileSingleProcessName))
            {
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
        }

        /// <summary>
        /// Empty the standby list.
        /// </summary>
        /// <param name="lowPriority">if set to <c>true</c> [low priority].</param>
        /// <exception cref="Win32Exception">
        /// </exception>
        private static void EmptyStandbyList(bool lowPriority = true)
        {
            // Windows minimum version
            if (!ComputerHelper.IsWindowsVistaOrAbove)
            {
                LogHelper.Warning(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperFeatureIsNotSupported, Resources.MemoryHelperEmptyStandbyList));
                return;
            }

            if (ComputerHelper.SetIncreasePrivilege(Constants.Windows.ProfileSingleProcessName))
            {
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
        }

        /// <summary>
        /// Empty the modified page list.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// </exception>
        private static void EmptyModifiedPageList()
        {
            // Windows minimum version
            if (!ComputerHelper.IsWindowsVistaOrAbove)
            {
                LogHelper.Warning(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperFeatureIsNotSupported, Resources.MemoryHelperEmptyModifiedPageList));
                return;
            }

            if (ComputerHelper.SetIncreasePrivilege(Constants.Windows.ProfileSingleProcessName))
            {
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
        }

        /// <summary>
        /// Empty the system working set.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// </exception>
        private static void EmptySystemWorkingSet()
        {
            // Windows minimum version
            if (!ComputerHelper.IsWindowsVistaOrAbove)
            {
                LogHelper.Warning(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperFeatureIsNotSupported, Resources.MemoryHelperEmptySystemWorkingSet));
                return;
            }

            if (ComputerHelper.SetIncreasePrivilege(Constants.Windows.IncreaseQuotaName))
            {
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
        }

        /// <summary>
        /// Empty the working sets.
        /// </summary>
        /// <exception cref="Win32Exception"></exception>
        private static void EmptyWorkingSets()
        {
            // Windows minimum version
            if (!ComputerHelper.IsWindowsVistaOrAbove)
            {
                LogHelper.Warning(string.Format(CultureInfo.CurrentCulture, Resources.MemoryHelperFeatureIsNotSupported, Resources.MemoryHelperEmptyWorkingSets));
                return;
            }

            foreach (Process process in Process.GetProcesses().Where(w => w != null))
            {
                try
                {
                    using (process)
                    {
                        if (NativeMethods.EmptyWorkingSet(process.Handle) == 0)
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                catch (Win32Exception e)
                {
                    if (e.NativeErrorCode != (int)Enums.Windows.SystemErrorCode.ERROR_ACCESS_DENIED)
                        LogHelper.Error(e);
                }
            }
        }

        /// <summary>
        /// Garbage Collection
        /// </summary>
        private static void GarbageCollection()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
