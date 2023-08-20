using System;

namespace WinMemoryCleaner
{
    internal static class Constants
    {
        internal static class App
        {
            public const int AutoOptimizationMemoryUsageInterval = 5; // Minute
            internal const int AutoUpdateInterval = 24; // Hour
            internal const string Guid = "4B3E3081-D421-4AAC-B3DE-808B1A9CCD30";
            internal const string KeyFile = "WinMemoryCleaner.snk";
            internal const string Language = "English";
            public const string License = "GPL-3.0";
            internal const string LocalizationResourceExtension = ".json";
            internal const string LocalizationResourcePath = "WinMemoryCleaner.Resources.Localization.";
            internal const string Name = "WinMemoryCleaner";
            public const string Title = "Windows Memory Cleaner";

            internal static class Author
            {
                public const string Name = "Igor Mundstein";
            }

            internal static class Log
            {
                internal const string DatetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            }

            internal static class Registry
            {
                internal static class Key
                {
                    internal const string ProcessExclusionList = @"SOFTWARE\WinMemoryCleaner\ProcessExclusionList";
                    internal const string Settings = @"SOFTWARE\WinMemoryCleaner";
                    internal const string Startup = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run";
                }

                internal static class Name
                {
                    internal const string AlwaysOnTop = "AlwaysOnTop";
                    internal const string AutoOptimizationInterval = "AutoOptimizationInterval";
                    internal const string AutoOptimizationMemoryUsage = "AutoOptimizationMemoryUsage";
                    internal const string AutoUpdate = "AutoUpdate";
                    internal const string CloseAfterOptimization = "CloseAfterOptimization";
                    internal const string CloseToTheNotificationArea = "CloseToTheNotificationArea";
                    internal const string CompactMode = "CompactMode";
                    internal const string Language = "Language";
                    internal const string MemoryAreas = "MemoryAreas";
                    internal const string OptimizationKey = "OptimizationKey";
                    internal const string OptimizationModifiers = "OptimizationModifiers";
                    internal const string RunOnStartup = "RunOnStartup";
                    internal const string ShowOptimizationNotifications = "ShowOptimizationNotifications";
                    internal const string StartMinimized = "StartMinimized";
                }
            }

            internal static class Repository
            {
                public static readonly Uri AssemblyInfoUri = new Uri("https://raw.githubusercontent.com/IgorMundstein/WinMemoryCleaner/master/src/Properties/AssemblyInfo.cs");
                public static readonly Uri LatestExeUri = new Uri("https://github.com/IgorMundstein/WinMemoryCleaner/releases/latest/download/WinMemoryCleaner.exe");
                public static readonly Uri Uri = new Uri("https://github.com/IgorMundstein/WinMemoryCleaner/");
            }
        }

        internal static class Windows
        {
            internal static class DesktopWindowManager
            {
                internal class Attribute
                {
                    internal const int BorderColor = 34;
                    internal const int WindowCornerPreference = 33;
                }
                internal class Value
                {
                    internal const int WindowCornerPreferenceRound = 2;
                }
            }

            internal static class Keyboard
            {
                internal const int WmHotkey = 786; // 0x312
            }

            internal static class Privilege
            {
                internal const string SeDebugName = "SeDebugPrivilege"; // Required to debug and adjust the memory of a process owned by another account. User Right: Debug programs.
                internal const string SeIncreaseQuotaName = "SeIncreaseQuotaPrivilege"; // Required to increase the quota assigned to a process. User Right: Adjust memory quotas for a process.
                internal const string SeProfSingleProcessName = "SeProfileSingleProcessPrivilege"; // Required to gather profiling information for a single process. User Right: Profile single process.
            }

            internal static class PrivilegeAttribute
            {
                internal const int Enabled = 2;
            }

            internal static class ShowWindow
            {
                internal const int Restore = 9; // SW_RESTORE
            }

            internal static class SystemErrorCode
            {
                internal const int ErrorAccessDenied = 5; // (ERROR_ACCESS_DENIED) Access is denied
                internal const int ErrorSuccess = 0; // (ERROR_SUCCESS) The operation completed successfully
            }

            internal static class SystemInformationClass
            {
                internal const int SystemCombinePhysicalMemoryInformation = 130; // 0x82
                internal const int SystemFileCacheInformation = 21; // 0x15
                internal const int SystemMemoryListInformation = 80; // 0x50
            }

            internal static class SystemMemoryListCommand
            {
                internal const int MemoryFlushModifiedList = 3;
                internal const int MemoryPurgeLowPriorityStandbyList = 5;
                internal const int MemoryPurgeStandbyList = 4;
            }
        }
    }
}
