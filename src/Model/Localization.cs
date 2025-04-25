using System.Runtime.Serialization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner
{
    /// <summary>
    /// Localization (L10N)
    /// </summary>
    [DataContract]
    public class Localization
    {
        [DataMember]
        public string Add { get; private set; }

        [DataMember]
        public string AlwaysOnTop { get; private set; }

        [DataMember]
        public string AutoOptimization { get; private set; }

        [DataMember]
        public string AutoOptimizationInterval { get; private set; }

        [DataMember]
        public string AutoUpdate { get; private set; }

        [DataMember]
        public string Close { get; private set; }

        [DataMember]
        public string CloseAfterOptimization { get; private set; }

        [DataMember]
        public string CloseToTheNotificationArea { get; private set; }

        [DataMember]
        public string Collapse { get; private set; }

        [DataMember]
        public string DevelopedBy { get; private set; }

        [DataMember]
        public string Error { get; private set; }

        [DataMember]
        public string ErrorAdminPrivilegeRequired { get; private set; }

        [DataMember]
        public string ErrorCanNotSaveLog { get; private set; }

        [DataMember]
        public string ErrorMemoryAreaOptimizationNotSupported { get; private set; }

        [DataMember]
        public string EveryHour { get; private set; }

        [DataMember]
        public string Exit { get; private set; }

        [DataMember]
        public string Expand { get; private set; }

        [DataMember]
        public string Free { get; private set; }

        [DataMember]
        public string HotkeyIsInUseByWindows { get; private set; }

        [DataMember]
        public string Image { get; private set; }

        [DataMember]
        public string Invalid { get; private set; }

        [DataMember]
        public string MemoryAreas { get; private set; }

        [DataMember]
        public string MemoryCombinedPageList { get; private set; }

        [DataMember]
        public string MemoryModifiedPageList { get; private set; }

        [DataMember]
        public string MemoryOptimized { get; private set; }

        [DataMember]
        public string MemoryProcessesWorkingSet { get; private set; }

        [DataMember]
        public string MemoryStandbyList { get; private set; }

        [DataMember]
        public string MemoryStandbyListLowPriority { get; private set; }

        [DataMember]
        public string MemorySystemWorkingSet { get; private set; }

        [DataMember]
        public string MemoryUsage { get; private set; }

        [DataMember]
        public string Minimize { get; private set; }

        [DataMember]
        public string OptimizationHotkey { get; private set; }

        [DataMember]
        public string Optimize { get; private set; }

        [DataMember]
        public string Optimized { get; private set; }

        [DataMember]
        public string Physical { get; private set; }

        [DataMember]
        public string ProcessExclusionList { get; private set; }

        [DataMember]
        public string Remove { get; private set; }

        [DataMember]
        public string RepositoryInfo { get; private set; }

        [DataMember]
        public string RunOnLowPriority { get; private set; }

        [DataMember]
        public string RunOnStartup { get; private set; }

        [DataMember]
        public string Seconds { get; private set; }

        [DataMember]
        public string Settings { get; private set; }

        [DataMember]
        public string ShowOptimizationNotifications { get; private set; }

        [DataMember]
        public string ShowVirtualMemory { get; private set; }

        [DataMember]
        public string StartMinimized { get; private set; }

        [DataMember]
        public string TrayIcon { get; private set; }

        [DataMember]
        public string UpdatedToVersion { get; private set; }

        [DataMember]
        public string Used { get; private set; }

        [DataMember]
        public string Virtual { get; private set; }

        [DataMember]
        public string WhenFreeMemoryIsBelow { get; private set; }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member