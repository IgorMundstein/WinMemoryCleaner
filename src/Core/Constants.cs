using System;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner
{
    /// <summary>
    /// Constants
    /// </summary>
    public static class Constants
    {
        public static class App
        {
            public const int AutoOptimizationMemoryUsageInterval = 5; // Minute
            public const int AutoUpdateInterval = 24; // Hour
            public const string Id = "4B3E3081-D421-4AAC-B3DE-808B1A9CCD30";
            public const string KeyFile = "WinMemoryCleaner.snk";
            public const string License = "GPL-3.0";
            public const string LocalizationResourceExtension = ".json";
            public const string LocalizationResourcePath = "WinMemoryCleaner.Resources.Localization.";
            public const string Name = "WinMemoryCleaner";
            public const string Title = "Windows Memory Cleaner";
            public const string VersionFormat = "{0}.{1}";

            public static class Author
            {
                public const string Name = "Igor Mundstein";
            }

            public static class CommandLineArgument
            {
                public const string Install = "Install";
                public const string Package = "Package";
                public const string Service = "Service";
                public const string Uninstall = "Uninstall";
                public const string WinGet = "WinGet";
            }

            public static class Defaults
            {
                public static readonly string Path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }

            public static class Log
            {
                public const string DatetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            }

            public static class Registry
            {
                public static class Key
                {
                    public const string ProcessExclusionList = @"SOFTWARE\WinMemoryCleaner\ProcessExclusionList";
                    public const string Settings = @"SOFTWARE\WinMemoryCleaner";
                    public const string Startup = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run";
                }

                public static class Name
                {
                    public const string AlwaysOnTop = "AlwaysOnTop";
                    public const string AutoOptimizationInterval = "AutoOptimizationInterval";
                    public const string AutoOptimizationMemoryUsage = "AutoOptimizationMemoryUsage";
                    public const string AutoUpdate = "AutoUpdate";
                    public const string CloseAfterOptimization = "CloseAfterOptimization";
                    public const string CloseToTheNotificationArea = "CloseToTheNotificationArea";
                    public const string CompactMode = "CompactMode";
                    public const string Language = "Language";
                    public const string MemoryAreas = "MemoryAreas";
                    public const string OptimizationKey = "OptimizationKey";
                    public const string OptimizationModifiers = "OptimizationModifiers";
                    public const string Path = "Path";
                    public const string RunOnPriority = "RunOnPriority";
                    public const string RunOnStartup = "RunOnStartup";
                    public const string ShowOptimizationNotifications = "ShowOptimizationNotifications";
                    public const string ShowVirtualMemory = "ShowVirtualMemory";
                    public const string StartMinimized = "StartMinimized";
                    public const string TrayIcon = "TrayIcon";
                    public const string UseHotkey = "UseHotkey";
                }
            }

            public static class Repository
            {
                public static readonly Uri AssemblyInfoUri = new Uri("https://raw.githubusercontent.com/IgorMundstein/WinMemoryCleaner/master/src/Properties/AssemblyInfo.cs");
                public static readonly Uri LatestExeUri = new Uri("https://github.com/IgorMundstein/WinMemoryCleaner/releases/latest/download/WinMemoryCleaner.exe");
                public static readonly Uri Uri = new Uri("https://github.com/IgorMundstein/WinMemoryCleaner/");
            }
        }

        public static class Windows
        {
            public static class DesktopWindowManager
            {
                public static class Attribute
                {
                    public const int BorderColor = 34;
                    public const int WindowCornerPreference = 33;
                }
                public static class Value
                {
                    public const int WindowCornerPreferenceRound = 2;
                }
            }

            public static class Drive
            {
                public const int FsctlDiscardVolumeCache = 589828; // 0x00090054 - FSCTL_DISCARD_VOLUME_CACHE
                public const int IoControlResetWriteOrder = 589832; // 0x000900F8 - FSCTL_RESET_WRITE_ORDER
            }

            public static class File
            {
                public const int FlagsNoBuffering = 536870912; // 0x20000000 - FILE_FLAG_NO_BUFFERING
            }

            public static class Keyboard
            {
                public const int WmHotkey = 786; // 0x312
            }

            public static class Locale
            {
                public static class Name
                {
                    public const string English = "en";
                }
            }

            public static class Privilege
            {
                public const string SeDebugName = "SeDebugPrivilege"; // Required to debug and adjust the memory of a process owned by another account. User Right: Debug programs.
                public const string SeIncreaseQuotaName = "SeIncreaseQuotaPrivilege"; // Required to increase the quota assigned to a process. User Right: Adjust memory quotas for a process.
                public const string SeProfSingleProcessName = "SeProfileSingleProcessPrivilege"; // Required to gather profiling information for a single process. User Right: Profile single process.
            }

            public static class PrivilegeAttribute
            {
                public const int Enabled = 2;
            }

            public static class ShowWindow
            {
                public const int Restore = 9; // SW_RESTORE
            }

            public static class SystemErrorCode
            {
                public const int ErrorAccessDenied = 5; // (ERROR_ACCESS_DENIED) Access is denied
                public const int ErrorSuccess = 0; // (ERROR_SUCCESS) The operation completed successfully
            }

            public static class SystemInformationClass
            {
                public const int SystemCombinePhysicalMemoryInformation = 130; // 0x82
                public const int SystemFileCacheInformation = 21; // 0x15
                public const int SystemMemoryListInformation = 80; // 0x50
                public const int SystemRegistryReconciliationInformation = 155; // 0x9B
            }

            public static class SystemMemoryListCommand
            {
                public const int MemoryEmptyWorkingSets = 2;
                public const int MemoryFlushModifiedList = 3;
                public const int MemoryPurgeLowPriorityStandbyList = 5;
                public const int MemoryPurgeStandbyList = 4;
            }
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member