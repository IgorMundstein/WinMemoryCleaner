using System;
using System.Runtime.InteropServices;

namespace WinMemoryCleaner
{
    internal static class Structs
    {
        internal static class Windows
        {
            /// <summary>
            /// Memory Combine Information Ex
            /// </summary>
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            internal struct MemoryCombineInformationEx
            {
                internal IntPtr Handle;
                internal IntPtr PagesCombined;
                internal IntPtr Flags;
            }

            /// <summary>
            /// System Cache Information structure for x86 working set
            /// </summary>
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

            /// <summary>
            /// System Cache Information structure for x64 working set
            /// </summary>
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

            /// <summary>
            /// Token Privileges structure, used for adjusting token privileges
            /// </summary>
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            internal struct TokenPrivileges
            {
                internal int Count;
                internal long Luid;
                internal int Attr;
            }
        }
    }
}
