using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.VisualBasic.Devices;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Computer Helper
    /// </summary>
    internal static class ComputerHelper
    {
        #region Constructor

        /// <summary>
        /// Initializes the <see cref="ComputerHelper"/> class.
        /// </summary>
        static ComputerHelper()
        {
            _computer = new ComputerInfo();
        }

        #endregion

        #region Fields

        private static readonly ComputerInfo _computer;

        #endregion

        #region Methods

        /// <summary>
        /// Gets the memory available.
        /// </summary>
        /// <returns></returns>
        internal static long GetMemoryAvailable()
        {
            return Convert.ToInt64(_computer.AvailablePhysicalMemory);
        }

        /// <summary>
        /// Gets the size of the memory.
        /// </summary>
        /// <returns></returns>
        internal static long GetMemorySize()
        {
            return Convert.ToInt64(_computer.TotalPhysicalMemory);
        }

        /// <summary>
        /// Gets the memory usage.
        /// </summary>
        /// <returns></returns>
        internal static long GetMemoryUsage()
        {
            return Convert.ToInt64(100 - GetMemoryAvailable() / (double)GetMemorySize() * 100);
        }

        /// <summary>
        /// Memory clean
        /// </summary>
        /// <remarks>
        /// - Empty Working Sets
        /// - Empty System Working Set
        /// - Empty Modified Page List
        /// - Empty Standby List
        /// - Invoke Garbage Collection
        /// </remarks>
        internal static void MemoryClean()
        {
            try
            {
                int length;
                GCHandle handle;

                // Empty Working Sets
                IList<Process> processes = Process.GetProcesses().Where(w => w != null).ToList();

                foreach (Process process in processes)
                {
                    try
                    {
                        using (process)
                        {
                            NativeMethods.EmptyWorkingSet(process.Handle);
                        }
                    }
                    catch (Win32Exception)
                    {
                        // ignored
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }

                // Empty System Working Set
                if (SetIncreasePrivilege(NativeMethods.SE_INCREASE_QUOTA_NAME))
                {
                    // 64 bits
                    if (Marshal.SizeOf(typeof(IntPtr)) == 8)
                    {
                        NativeMethods.SystemCacheInformation64 information64Bit = new NativeMethods.SystemCacheInformation64
                        {
                            MinimumWorkingSet = -1L,
                            MaximumWorkingSet = -1L
                        };

                        length = Marshal.SizeOf(information64Bit);
                        handle = GCHandle.Alloc(information64Bit, GCHandleType.Pinned);

                        try
                        {
                            NativeMethods.NtSetSystemInformation(NativeMethods.SYSTEM_FILE_CACHE_INFORMATION, handle.AddrOfPinnedObject(), length);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }

                        handle.Free();
                    }
                    else // 32 bits
                    {
                        NativeMethods.SystemCacheInformation32 cacheInformation = new NativeMethods.SystemCacheInformation32
                        {
                            MinimumWorkingSet = uint.MaxValue,
                            MaximumWorkingSet = uint.MaxValue
                        };

                        length = Marshal.SizeOf(cacheInformation);
                        handle = GCHandle.Alloc(cacheInformation, GCHandleType.Pinned);

                        try
                        {
                            NativeMethods.NtSetSystemInformation(NativeMethods.SYSTEM_FILE_CACHE_INFORMATION, handle.AddrOfPinnedObject(), length);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }

                        handle.Free();
                    }
                }

                // Empty Modified Page List & Empty Standby List
                if (SetIncreasePrivilege(NativeMethods.SE_PROFILE_SINGLE_PROCESS_NAME))
                {
                    // Empty Modified Page List
                    length = Marshal.SizeOf(NativeMethods.MEMORY_FLUSH_MODIFIED_LIST);
                    handle = GCHandle.Alloc(NativeMethods.MEMORY_FLUSH_MODIFIED_LIST, GCHandleType.Pinned);

                    try
                    {
                        NativeMethods.NtSetSystemInformation(NativeMethods.SYSTEM_MEMORY_LIST_INFORMATION, handle.AddrOfPinnedObject(), length);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }

                    handle.Free();

                    // Empty Standby List
                    length = Marshal.SizeOf(NativeMethods.MEMORY_PURGE_STANDBY_LIST);
                    handle = GCHandle.Alloc(NativeMethods.MEMORY_PURGE_STANDBY_LIST, GCHandleType.Pinned);

                    try
                    {
                        NativeMethods.NtSetSystemInformation(NativeMethods.SYSTEM_MEMORY_LIST_INFORMATION, handle.AddrOfPinnedObject(), length);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }

                    handle.Free();
                }

                // Garbage collection
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Sets the increase privilege.
        /// </summary>
        /// <param name="privilegeName">Name of the privilege.</param>
        /// <returns></returns>
        private static bool SetIncreasePrivilege(string privilegeName)
        {
            try
            {
                using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
                {
                    NativeMethods.TokPriv1Luid newst;
                    newst.Count = 1;
                    newst.Luid = 0L;
                    newst.Attr = NativeMethods.SE_PRIVILEGE_ENABLED;

                    // Retrieves the LUID used on a specified system to locally represent the specified privilege name
                    if (NativeMethods.LookupPrivilegeValue(null, privilegeName, ref newst.Luid))
                    {
                        // Enables or disables privileges in a specified access token
                        int result = NativeMethods.AdjustTokenPrivileges(current.Token, false, ref newst, 0, IntPtr.Zero, IntPtr.Zero) ? 1 : 0;

                        return result != 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return false;
        }

        #endregion
    }
}
