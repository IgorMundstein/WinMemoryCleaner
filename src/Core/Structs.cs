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
                internal ulong Flags;
            }

            /// <summary>
            /// Memory Status EX structure contains information about the current state of both physical and virtual memory, including extended memory
            /// </summary>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            internal class MemoryStatusEx
            {
                internal readonly uint dwLength;        // The size of the structure, in bytes.
                internal uint dwMemoryLoad;             // A number between 0 and 100 that specifies the approximate percentage of physical memory that is in use.
                internal ulong ullTotalPhys;            // The amount of actual physical memory, in bytes.
                internal ulong ullAvailPhys;            // The amount of physical memory currently available, in bytes.
                internal ulong ullTotalPageFile;        // The current committed memory limit for the system or the current process, whichever is smaller, in bytes.
                internal ulong ullAvailPageFile;        // The maximum amount of memory the current process can commit, in bytes.
                internal ulong ullTotalVirtual;         // The size of the user-mode portion of the virtual address space of the calling process, in bytes.
                internal ulong ullAvailVirtual;         // The amount of unreserved and uncommitted memory currently in the user-mode portion of the virtual address space of the calling process, in bytes.
                internal ulong ullAvailExtendedVirtual; // Reserved. This value is always 0.

                /// <summary>
                /// Initializes a new instance of the <see cref="MemoryStatusEx" /> class.
                /// </summary>
                internal MemoryStatusEx()
                {
                    dwLength = (uint)Marshal.SizeOf(typeof(MemoryStatusEx));
                    dwMemoryLoad = 0;
                    ullTotalPhys = 0;
                    ullAvailPhys = 0;
                    ullTotalPageFile = 0;
                    ullAvailPageFile = 0;
                    ullTotalVirtual = 0;
                    ullAvailVirtual = 0;
                    ullAvailExtendedVirtual = 0;
                }
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
