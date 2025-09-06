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
            public const string EmbeddedResourcePath = "WinMemoryCleaner.Resources.";
            public const string EmbeddedResourcePathExtension = ".json";
            public const string Id = "4B3E3081-D421-4AAC-B3DE-808B1A9CCD30";
            public const string KeyFile = "WinMemoryCleaner.snk";
            public const string License = "GPL-3.0";
            public const string LocalizationResourcePath = EmbeddedResourcePath + "Localization.";
            public const string Name = "WinMemoryCleaner";
            public const string ThemesResourcePath = EmbeddedResourcePath + "Themes.";
            public const string Title = "Windows Memory Cleaner";
            public const string VersionFormat = "{0}.{1}.{2}";

            public static class Author
            {
                public const string Name = "Igor Mundstein";
            }

            public static class Certificate
            {
                public static class Release
                {
                    public const string Thumbprint = "9D201FB199626ABE7DA32FBE47013FC023670F9B";
                }

                public static class Test
                {
                    public const string Thumbprint = "2187092935C12F90727B29AD6913A7F89817B942";
                }
            }

            public static class CommandLineArgument
            {
                public const string Install = "Install";
                public const string Package = "Package";
                public const string Service = "Service";
                public const string Uninstall = "Uninstall";
            }

            public static class Defaults
            {
                public static readonly string Path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }

            public static class Donation
            {
                public static readonly Uri BitcoinUri = new Uri("https://www.blockchain.com/explorer/addresses/btc/bc1qu884q5r2uqugvdhyk8l6waakumeve7jykqp7ap");
                public static readonly Uri EthereumUri = new Uri("https://www.blockchain.com/explorer/addresses/eth/0xb71A94733B0578D155D9A765E0d2C4dA0f44156d");
                public static readonly Uri GitHubSponsorUri = new Uri("https://github.com/sponsors/IgorMundstein");
                public static readonly Uri KofiUri = new Uri("https://ko-fi.com/igormundstein");
            }

            public static class Registry
            {
                public static class Key
                {
                    public const string ProcessExclusionList = @"SOFTWARE\WinMemoryCleaner\ProcessExclusionList";
                    public const string Settings = @"SOFTWARE\WinMemoryCleaner";
                }
            }

            public static class Repository
            {
                private const string GitHub = "https://github.com/IgorMundstein/WinMemoryCleaner";
                private const string GitHubRaw = "https://raw.githubusercontent.com/IgorMundstein/WinMemoryCleaner/main";

                public static readonly Uri AboutUri = new Uri(GitHub + "?tab=readme-ov-file#windows-memory-cleaner");
                public static readonly Uri AssemblyInfoUri = new Uri(GitHubRaw + "/src/Properties/AssemblyInfo.cs");
                public static readonly Uri DownloadUri = new Uri(GitHub + "?tab=readme-ov-file#-download");
                public static readonly Uri LatestExeUri = new Uri(GitHub + "/releases/latest/download/WinMemoryCleaner.exe");
                public static readonly Uri Uri = new Uri(GitHub);
            }

            public static class Update
            {
                public const string BackupDirName = "Backups";
                public const int BackupRetentionDays = 3; // Day
                public const int CheckInterval = 24; // Hour
                public const string ChecksumsFileName = "checksums.txt";
                public const int ChecksumsMaxRetries = 3;
                public const int ChecksumsRetryDelay = 2; // Second
                public const int DownloadTimeout = 60; // Second
                public const int MaxConnectionLimit = 10;
                public const string MutexName = @"Local\WinMemoryCleaner_AutoUpdate_Mutex";
                public const bool RequireAuthenticode = true; // Require Authenticode trust and known thumbprint
                public const bool RequireChecksum = true; // Require checksum to be present and matching
                public const bool RequireKnownDownloadUri = true; // Require expected HTTPS GitHub URL shape
                public const int ReplaceMaxTries = 120; // Count
                public const int ReplaceRetryDelayMs = 1000; // Millisecond
                public const string TempRootDirName = "WinMemoryCleaner";
                public const string TokenEnvVar = "WMC_UPDATE_TOKEN";
                public const string TokenFilePrefix = "WMC.";
                public const string TokenFileSuffix = ".txt";
                public const string UserAgent = "WinMemoryCleaner-Updater/1.0";
                public const bool VerifyFileVersionInfo = true;
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
                    public const string SimplifiedChinese = "zh-Hans";
                    public const string TraditionalChinese = "zh-Hant";
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
