using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace WinMemoryCleaner.Engine
{
    /// <summary>
    /// Memory areas (bit flags). Values match the original Enums.Memory.Areas exactly,
    /// so persisted settings remain compatible.
    /// </summary>
    [Flags]
    internal enum MemoryAreas
    {
        None = 0,
        CombinedPageList = 1,
        ModifiedFileCache = 2,
        ModifiedPageList = 4,
        RegistryCache = 8,
        StandbyList = 16,
        StandbyListLowPriority = 32,
        SystemFileCache = 64,
        WorkingSet = 128
    }

    internal enum OptimizationReason
    {
        LowMemory,
        Manual,
        Schedule
    }

    /// <summary>
    /// Operating-system capability detection, ported from the original ComputerService/OperatingSystem.
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal sealed class OsInfo
    {
        public bool Is64Bit { get; }
        public bool IsWindowsXpOrGreater { get; }
        public bool IsWindowsVistaOrGreater { get; }
        public bool IsWindows7OrGreater { get; }
        public bool IsWindows8OrGreater { get; }
        public bool IsWindows81OrGreater { get; }

        public bool HasWorkingSet => IsWindowsXpOrGreater;
        public bool HasSystemFileCache => IsWindowsXpOrGreater;
        public bool HasModifiedFileCache => IsWindowsXpOrGreater;
        public bool HasModifiedPageList => IsWindowsVistaOrGreater;
        public bool HasStandbyList => IsWindowsVistaOrGreater;
        public bool HasCombinedPageList => IsWindows8OrGreater;
        public bool HasRegistryHive => IsWindows81OrGreater;

        public OsInfo()
        {
            var v = Environment.OSVersion.Version;
            Is64Bit = Environment.Is64BitOperatingSystem;
            IsWindowsXpOrGreater = v.Major >= 5.1;
            IsWindowsVistaOrGreater = v.Major >= 6;
            IsWindows7OrGreater = (v.Major > 6) || (v.Major == 6 && v.Minor >= 1);
            IsWindows8OrGreater = v.Major >= 6.2;
            IsWindows81OrGreater = v.Major >= 6.3;
        }

        /// <summary>Which areas the OS supports, as a bit mask.</summary>
        public MemoryAreas SupportedAreas
        {
            get
            {
                var a = MemoryAreas.None;
                if (HasWorkingSet) a |= MemoryAreas.WorkingSet;
                if (HasSystemFileCache) a |= MemoryAreas.SystemFileCache;
                if (HasModifiedFileCache) a |= MemoryAreas.ModifiedFileCache;
                if (HasModifiedPageList) a |= MemoryAreas.ModifiedPageList;
                if (HasStandbyList) a |= MemoryAreas.StandbyList | MemoryAreas.StandbyListLowPriority;
                if (HasCombinedPageList) a |= MemoryAreas.CombinedPageList;
                if (HasRegistryHive) a |= MemoryAreas.RegistryCache;
                return a;
            }
        }
    }

    /// <summary>Point-in-time RAM snapshot from GlobalMemoryStatusEx.</summary>
    [SupportedOSPlatform("windows")]
    internal readonly struct MemorySnapshot
    {
        public long PhysicalTotal { get; }
        public long PhysicalFree { get; }
        public long PhysicalUsed { get; }
        public int PhysicalUsedPercent { get; }

        public long VirtualTotal { get; }
        public long VirtualFree { get; }
        public long VirtualUsed { get; }
        public int VirtualUsedPercent { get; }

        private MemorySnapshot(Structs.Windows.MemoryStatusEx m)
        {
            PhysicalTotal = m.TotalPhys;
            PhysicalFree = m.AvailPhys;
            PhysicalUsed = m.TotalPhys >= m.AvailPhys ? m.TotalPhys - m.AvailPhys : 0;
            PhysicalUsedPercent = m.MemoryLoad;

            VirtualTotal = m.TotalPageFile;
            VirtualFree = m.AvailPageFile;
            VirtualUsed = m.TotalPageFile >= m.AvailPageFile ? m.TotalPageFile - m.AvailPageFile : 0;
            VirtualUsedPercent = m.TotalPageFile > 0 ? (int)(VirtualUsed * 100 / m.TotalPageFile) : 0;
        }

        public static MemorySnapshot Read()
        {
            var m = new Structs.Windows.MemoryStatusEx();
            if (!NativeMethods.GlobalMemoryStatusEx(m))
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return new MemorySnapshot(m);
        }
    }
}
