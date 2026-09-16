using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace WinMemoryCleaner.Engine
{
    /// <summary>
    /// Windows API. Ported verbatim (signatures unchanged) from the original
    /// WinMemoryCleaner Interop/NativeMethods.cs so the optimization behavior is identical.
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal static class NativeMethods
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AdjustTokenPrivileges(IntPtr tokenHandle, [MarshalAs(UnmanagedType.Bool)] bool disableAllPrivileges, ref Structs.Windows.TokenPrivileges newState, int bufferLength, IntPtr previousState, IntPtr returnLength);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern SafeFileHandle CreateFile([MarshalAs(UnmanagedType.LPWStr)] string lpFileName, FileAccess dwDesiredAccess, FileShare dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeviceIoControl(SafeFileHandle hDevice, int dwIoControlCode, IntPtr lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, out int lpBytesReturned, IntPtr lpOverlapped);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EmptyWorkingSet(IntPtr hProcess);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FlushFileBuffers(SafeFileHandle hFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GlobalMemoryStatusEx([In, Out] Structs.Windows.MemoryStatusEx lpBuffer);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, ref long lpLuid);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern int NtSetSystemInformation(int SystemInformationClass, IntPtr SystemInformation, uint SystemInformationLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetSystemFileCacheSize(IntPtr minimumFileCacheSize, IntPtr maximumFileCacheSize, int flags);
    }

    /// <summary>
    /// Structs. Ported verbatim from the original Core/Structs.cs.
    /// </summary>
    internal static class Structs
    {
        internal static class Windows
        {
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct MemoryCombineInformationEx
            {
                public IntPtr Handle;
                public IntPtr PagesCombined;
                public long Flags;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public class MemoryStatusEx
            {
                public readonly int Length;
                public int MemoryLoad;
                public long TotalPhys;
                public long AvailPhys;
                public long TotalPageFile;
                public long AvailPageFile;
                public long TotalVirtual;
                public long AvailVirtual;
                public long AvailExtendedVirtual;

                public MemoryStatusEx()
                {
                    Length = Marshal.SizeOf(typeof(MemoryStatusEx));
                }
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct SystemFileCacheInformation32
            {
                public int CurrentSize;
                public int PeakSize;
                public int PageFaultCount;
                public int MinimumWorkingSet;
                public int MaximumWorkingSet;
                public int CurrentSizeIncludingTransitionInPages;
                public int PeakSizeIncludingTransitionInPages;
                public int TransitionRePurposeCount;
                public int Flags;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct SystemFileCacheInformation64
            {
                public long CurrentSize;
                public long PeakSize;
                public long PageFaultCount;
                public long MinimumWorkingSet;
                public long MaximumWorkingSet;
                public long CurrentSizeIncludingTransitionInPages;
                public long PeakSizeIncludingTransitionInPages;
                public long TransitionRePurposeCount;
                public long Flags;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct TokenPrivileges
            {
                public int Count;
                public long Luid;
                public int Attr;
            }
        }
    }

    /// <summary>
    /// Windows-specific constants. Ported verbatim from the original Core/Constants.cs (Windows section).
    /// </summary>
    internal static class Constants
    {
        internal static class Windows
        {
            internal static class Drive
            {
                public const int FsctlDiscardVolumeCache = 589828; // FSCTL_DISCARD_VOLUME_CACHE
                public const int IoControlResetWriteOrder = 589832; // FSCTL_RESET_WRITE_ORDER
            }

            internal static class File
            {
                public const int FlagsNoBuffering = 536870912; // FILE_FLAG_NO_BUFFERING
            }

            internal static class Privilege
            {
                public const string SeDebugName = "SeDebugPrivilege";
                public const string SeIncreaseQuotaName = "SeIncreaseQuotaPrivilege";
                public const string SeProfSingleProcessName = "SeProfileSingleProcessPrivilege";
            }

            internal static class PrivilegeAttribute
            {
                public const int Enabled = 2;
            }

            internal static class SystemErrorCode
            {
                public const int ErrorAccessDenied = 5;
                public const int ErrorSuccess = 0;
            }

            internal static class SystemInformationClass
            {
                public const int SystemCombinePhysicalMemoryInformation = 130;
                public const int SystemFileCacheInformation = 21;
                public const int SystemMemoryListInformation = 80;
                public const int SystemRegistryReconciliationInformation = 155;
            }

            internal static class SystemMemoryListCommand
            {
                public const int MemoryEmptyWorkingSets = 2;
                public const int MemoryFlushModifiedList = 3;
                public const int MemoryPurgeLowPriorityStandbyList = 5;
                public const int MemoryPurgeStandbyList = 4;
            }
        }
    }
}
