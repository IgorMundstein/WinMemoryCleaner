﻿using System;
using System.Runtime.InteropServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner
{
    /// <summary>
    /// Structs
    /// </summary>
    public static class Structs
    {
        public static class Windows
        {
            /// <summary>
            /// Memory Combine Information Ex
            /// </summary>
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct MemoryCombineInformationEx
            {
                public IntPtr Handle;
                public IntPtr PagesCombined;
                public ulong Flags;
            }

            /// <summary>
            /// Memory Status EX structure contains information about the current state of both physical and virtual memory, including extended memory
            /// </summary>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public class MemoryStatusEx
            {
                public readonly uint Length;          // The size of the structure, in bytes.
                public uint MemoryLoad;               // A number between 0 and 100 that specifies the approximate percentage of physical memory that is in use.
                public ulong TotalPhys;               // The amount of actual physical memory, in bytes.
                public ulong AvailPhys;               // The amount of physical memory currently available, in bytes.
                public ulong TotalPageFile;           // The current committed memory limit for the system or the current process, whichever is smaller, in bytes.
                public ulong AvailPageFile;           // The maximum amount of memory the current process can commit, in bytes.
                public ulong TotalVirtual;            // The size of the user-mode portion of the virtual address space of the calling process, in bytes.
                public ulong AvailVirtual;            // The amount of unreserved and uncommitted memory currently in the user-mode portion of the virtual address space of the calling process, in bytes.
                public ulong AvailExtendedVirtual;    // Reserved. This value is always 0.

                /// <summary>
                /// Initializes a new instance of the <see cref="MemoryStatusEx" /> class.
                /// </summary>
                public MemoryStatusEx()
                {
                    Length = (uint)Marshal.SizeOf(typeof(MemoryStatusEx));
                    MemoryLoad = 0;
                    TotalPhys = 0;
                    AvailPhys = 0;
                    TotalPageFile = 0;
                    AvailPageFile = 0;
                    TotalVirtual = 0;
                    AvailVirtual = 0;
                    AvailExtendedVirtual = 0;
                }
            }

            /// <summary>
            /// System Cache Information structure for x86 working set
            /// </summary>
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct SystemCacheInformation32
            {
                public uint CurrentSize;
                public uint PeakSize;
                public uint PageFaultCount;
                public uint MinimumWorkingSet;
                public uint MaximumWorkingSet;
                public uint Unused1;
                public uint Unused2;
                public uint Unused3;
                public uint Unused4;
            }

            /// <summary>
            /// System Cache Information structure for x64 working set
            /// </summary>
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct SystemCacheInformation64
            {
                public long CurrentSize;
                public long PeakSize;
                public long PageFaultCount;
                public long MinimumWorkingSet;
                public long MaximumWorkingSet;
                public long Unused1;
                public long Unused2;
                public long Unused3;
                public long Unused4;
            }

            /// <summary>
            /// Token Privileges structure, used for adjusting token privileges
            /// </summary>
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct TokenPrivileges
            {
                public int Count;
                public long Luid;
                public int Attr;
            }
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member