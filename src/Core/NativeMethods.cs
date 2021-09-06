using System;
using System.Runtime.InteropServices;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Windows Native Methods
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AdjustTokenPrivileges(IntPtr tokenHandle, [MarshalAs(UnmanagedType.Bool)]bool disableAllPrivileges, ref Structs.Windows.TokenPrivileges newState, int bufferLength, IntPtr previousState, IntPtr returnLength);

        [DllImport("psapi.dll", SetLastError = true)]
        internal static extern int EmptyWorkingSet(IntPtr hProcess);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, ref long lpLuid);

        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern UInt32 NtSetSystemInformation(int infoClass, IntPtr info, int length);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetSystemFileCacheSize(IntPtr minimumFileCacheSize, IntPtr maximumFileCacheSize, int flags);
    }
}
