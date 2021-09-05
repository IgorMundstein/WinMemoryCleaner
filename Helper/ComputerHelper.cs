using System;
using System.Security.Principal;
using Microsoft.VisualBasic.Devices;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Computer Helper
    /// </summary>
    internal static class ComputerHelper
    {
        #region Fields

        private static readonly ComputerInfo _computer = new ComputerInfo();

        #endregion

        #region Properties

        /// <summary>
        /// Determines whether the current operating system is a 64-bit operating system
        /// </summary>
        /// <value>
        ///   <c>true</c> if it 64-bit; otherwise, <c>false</c>.
        /// </value>
        internal static bool Is64Bit
        {
            get
            {
                return Environment.Is64BitOperatingSystem;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is windows 10 or above.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is windows 10 or above; otherwise, <c>false</c>.
        /// </value>
        internal static bool IsWindows8OrAbove
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6.2;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is windows vista or above.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is windows vista or above; otherwise, <c>false</c>.
        /// </value>
        internal static bool IsWindowsVistaOrAbove
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6;
            }
        }

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
            return Convert.ToInt64(100 - ((GetMemoryAvailable() / (double)GetMemorySize()) * 100));
        }

        /// <summary>
        /// Sets the increase privilege.
        /// </summary>
        /// <param name="privilegeName">Name of the privilege.</param>
        /// <returns></returns>
        internal static bool SetIncreasePrivilege(string privilegeName)
        {
            using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
            {
                Structs.Windows.TokenPrivileges newState;
                newState.Count = 1;
                newState.Luid = 0L;
                newState.Attr = Constants.Windows.PrivilegeEnabled;

                // Retrieves the LUID used on a specified system to locally represent the specified privilege name
                if (NativeMethods.LookupPrivilegeValue(null, privilegeName, ref newState.Luid))
                {
                    // Enables or disables privileges in a specified access token
                    int result = NativeMethods.AdjustTokenPrivileges(current.Token, false, ref newState, 0, IntPtr.Zero, IntPtr.Zero) ? 1 : 0;

                    return result != 0;
                }
            }

            return false;
        }

        #endregion
    }
}
