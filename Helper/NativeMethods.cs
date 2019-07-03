using System;
using System.Runtime.InteropServices;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Windows Native Methods
    /// </summary>
    internal static class NativeMethods
    {
        #region Constants

        internal const int MEMORY_FLUSH_MODIFIED_LIST = 3;
        internal const int MEMORY_PURGE_STANDBY_LIST = 4;
        internal const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";
        internal const int SE_PRIVILEGE_ENABLED = 2;
        internal const string SE_PROFILE_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";
        internal const int SYSTEM_FILE_CACHE_INFORMATION = 21;
        internal const int SYSTEM_MEMORY_LIST_INFORMATION = 80;

        #endregion

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        internal struct Rect
        {
            internal int left;
            internal int top;
            internal int right;
            internal int bottom;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct SystemCacheInformation32
        {
            internal uint CurrentSize;
            internal uint PeakSize;
            internal uint PageFaultCount;
            internal uint MinimumWorkingSet;
            internal uint MaximumWorkingSet;
            internal uint Unused1;
            internal uint Unused2;
            internal uint Unused3;
            internal uint Unused4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct SystemCacheInformation64
        {
            internal long CurrentSize;
            internal long PeakSize;
            internal long PageFaultCount;
            internal long MinimumWorkingSet;
            internal long MaximumWorkingSet;
            internal long Unused1;
            internal long Unused2;
            internal long Unused3;
            internal long Unused4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            internal int Count;
            internal long Luid;
            internal int Attr;
        }

        #endregion

        #region Methods

        [DllImport("advapi32.dll")]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("psapi.dll")]
        internal static extern int EmptyWorkingSet(IntPtr hwProc);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        internal static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("ntdll.dll")]
        internal static extern UInt32 NtSetSystemInformation(int infoClass, IntPtr info, int length);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr handle, UInt32 message, IntPtr wParam, IntPtr lParam);

        #endregion
    }
}
