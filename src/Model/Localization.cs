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
        #region Fields

        private string _about, _add, _alwaysOnTop, _autoOptimization, _autoOptimizationInterval, _autoUpdate;
        private string _background;
        private string _close, _closeAfterOptimization, _closeToTheNotificationArea, _collapse, _combinedPageList;
        private string _dangerLevel, _donate, _donationMessage, _donationTitle;
        private string _error, _errorAdminPrivilegeRequired, _errorCanNotSaveLog, _errorMemoryAreaOptimizationNotSupported, _everyHour, _exit, _expand;
        private string _free;
        private string _help, _hotkeyIsInUseByOperatingSystem;
        private string _invalid;
        private string _lowMemory;
        private string _manual, _memoryAreas, _memoryOptimized, _memoryUsage, _minimize, _modifiedFileCache, _modifiedPageList;
        private string _no;
        private string _optimizationHotkey, _optimize, _optimized;
        private string _physicalMemory, _processExclusionList;
        private string _reason, _registryCache, _remove, _reset, _resetConfirmation, _runOnLowPriority, _runOnStartup;
        private string _schedule, _seconds, _securityWarning, _settings, _showMemoryUsage, _showOptimizationNotifications, _showVirtualMemory, _standbyList, _standbyListLowPriority, _startMinimized, _systemFileCache;
        private string _text, _trayIcon;
        private string _updatedToVersion, _used, _useTransparentBackground;
        private string _virtualMemory;
        private string _warningLevel, _whenFreePhysicalMemoryIsBelow, _workingSet;
        private string _yes;

        #endregion

        #region Properties

        [DataMember]
        public string About
        {
            get { return _about; }
            private set { _about = value.Capitalize(); }
        }

        [DataMember]
        public string Add
        {
            get { return _add; }
            private set { _add = value.Capitalize(); }
        }

        [DataMember]
        public string AlwaysOnTop
        {
            get { return _alwaysOnTop; }
            private set { _alwaysOnTop = value.Capitalize(); }
        }

        [DataMember]
        public string AutoOptimization
        {
            get { return _autoOptimization; }
            private set { _autoOptimization = value.Capitalize(); }
        }

        [DataMember]
        public string AutoOptimizationInterval
        {
            get { return _autoOptimizationInterval; }
            private set { _autoOptimizationInterval = value.Capitalize(); }
        }

        [DataMember]
        public string AutoUpdate
        {
            get { return _autoUpdate; }
            private set { _autoUpdate = value.Capitalize(); }
        }

        [DataMember]
        public string Background
        {
            get { return _background; }
            private set { _background = value.Capitalize(); }
        }

        [DataMember]
        public string Close
        {
            get { return _close; }
            private set { _close = value.Capitalize(); }
        }

        [DataMember]
        public string CloseAfterOptimization
        {
            get { return _closeAfterOptimization; }
            private set { _closeAfterOptimization = value.Capitalize(); }
        }

        [DataMember]
        public string CloseToTheNotificationArea
        {
            get { return _closeToTheNotificationArea; }
            private set { _closeToTheNotificationArea = value.Capitalize(); }
        }

        [DataMember]
        public string Collapse
        {
            get { return _collapse; }
            private set { _collapse = value.Capitalize(); }
        }

        [DataMember]
        public string CombinedPageList
        {
            get { return _combinedPageList; }
            private set { _combinedPageList = value.Capitalize(); }
        }

        [DataMember]
        public string DangerLevel
        {
            get { return _dangerLevel; }
            private set { _dangerLevel = value.Capitalize(); }
        }

        [DataMember]
        public string Donate
        {
            get { return _donate; }
            private set { _donate = value.Capitalize(); }
        }

        [DataMember]
        public string DonationMessage
        {
            get { return _donationMessage; }
            private set { _donationMessage = value.Capitalize(); }
        }

        [DataMember]
        public string DonationTitle
        {
            get { return _donationTitle; }
            private set { _donationTitle = value.Capitalize(); }
        }

        [DataMember]
        public string Error
        {
            get { return _error; }
            private set { _error = value.Capitalize(); }
        }

        [DataMember]
        public string ErrorAdminPrivilegeRequired
        {
            get { return _errorAdminPrivilegeRequired; }
            private set { _errorAdminPrivilegeRequired = value.Capitalize(); }
        }

        [DataMember]
        public string ErrorCanNotSaveLog
        {
            get { return _errorCanNotSaveLog; }
            private set { _errorCanNotSaveLog = value.Capitalize(); }
        }

        [DataMember]
        public string ErrorMemoryAreaOptimizationNotSupported
        {
            get { return _errorMemoryAreaOptimizationNotSupported; }
            private set { _errorMemoryAreaOptimizationNotSupported = value.Capitalize(); }
        }

        [DataMember]
        public string EveryHour
        {
            get { return _everyHour; }
            private set { _everyHour = value.Capitalize(); }
        }

        [DataMember]
        public string Exit
        {
            get { return _exit; }
            private set { _exit = value.Capitalize(); }
        }

        [DataMember]
        public string Expand
        {
            get { return _expand; }
            private set { _expand = value.Capitalize(); }
        }

        [DataMember]
        public string Free
        {
            get { return _free; }
            private set { _free = value.Capitalize(); }
        }

        [DataMember]
        public string Help
        {
            get { return _help; }
            private set { _help = value.Capitalize(); }
        }

        [DataMember]
        public string HotkeyIsInUseByOperatingSystem
        {
            get { return _hotkeyIsInUseByOperatingSystem; }
            private set { _hotkeyIsInUseByOperatingSystem = value.Capitalize(); }
        }

        [DataMember]
        public string Invalid
        {
            get { return _invalid; }
            private set { _invalid = value.Capitalize(); }
        }

        [DataMember]
        public string LowMemory
        {
            get { return _lowMemory; }
            private set { _lowMemory = value.Capitalize(); }
        }

        [DataMember]
        public string Manual
        {
            get { return _manual; }
            private set { _manual = value.Capitalize(); }
        }

        [DataMember]
        public string MemoryAreas
        {
            get { return _memoryAreas; }
            private set { _memoryAreas = value.Capitalize(); }
        }

        [DataMember]
        public string MemoryOptimized
        {
            get { return _memoryOptimized; }
            private set { _memoryOptimized = value.Capitalize(); }
        }

        [DataMember]
        public string MemoryUsage
        {
            get { return _memoryUsage; }
            private set { _memoryUsage = value.Capitalize(); }
        }

        [DataMember]
        public string Minimize
        {
            get { return _minimize; }
            private set { _minimize = value.Capitalize(); }
        }

        [DataMember]
        public string ModifiedFileCache
        {
            get { return _modifiedFileCache; }
            private set { _modifiedFileCache = value.Capitalize(); }
        }

        [DataMember]
        public string ModifiedPageList
        {
            get { return _modifiedPageList; }
            private set { _modifiedPageList = value.Capitalize(); }
        }

        [DataMember]
        public string No
        {
            get { return _no; }
            private set { _no = value.Capitalize(); }
        }

        [DataMember]
        public string OptimizationHotkey
        {
            get { return _optimizationHotkey; }
            private set { _optimizationHotkey = value.Capitalize(); }
        }

        [DataMember]
        public string Optimize
        {
            get { return _optimize; }
            private set { _optimize = value.Capitalize(); }
        }

        [DataMember]
        public string Optimized
        {
            get { return _optimized; }
            private set { _optimized = value.Capitalize(); }
        }

        [DataMember]
        public string PhysicalMemory
        {
            get { return _physicalMemory; }
            private set { _physicalMemory = value.Capitalize(); }
        }

        [DataMember]
        public string ProcessExclusionList
        {
            get { return _processExclusionList; }
            private set { _processExclusionList = value.Capitalize(); }
        }

        [DataMember]
        public string Reason
        {
            get { return _reason; }
            private set { _reason = value.Capitalize(); }
        }

        [DataMember]
        public string RegistryCache
        {
            get { return _registryCache; }
            private set { _registryCache = value.Capitalize(); }
        }

        [DataMember]
        public string Remove
        {
            get { return _remove; }
            private set { _remove = value.Capitalize(); }
        }

        [DataMember]
        public string Reset
        {
            get { return _reset; }
            private set { _reset = value.Capitalize(); }
        }

        [DataMember]
        public string ResetConfirmation
        {
            get { return _resetConfirmation; }
            private set { _resetConfirmation = value.Capitalize(); }
        }

        [DataMember]
        public string RunOnLowPriority
        {
            get { return _runOnLowPriority; }
            private set { _runOnLowPriority = value.Capitalize(); }
        }

        [DataMember]
        public string RunOnStartup
        {
            get { return _runOnStartup; }
            private set { _runOnStartup = value.Capitalize(); }
        }

        [DataMember]
        public string Schedule
        {
            get { return _schedule; }
            private set { _schedule = value.Capitalize(); }
        }

        [DataMember]
        public string Seconds
        {
            get { return _seconds; }
            private set { _seconds = value.Capitalize(); }
        }

        [DataMember]
        public string SecurityWarning
        {
            get { return _securityWarning; }
            private set { _securityWarning = value.Capitalize(); }
        }

        [DataMember]
        public string Settings
        {
            get { return _settings; }
            private set { _settings = value.Capitalize(); }
        }

        [DataMember]
        public string ShowMemoryUsage
        {
            get { return _showMemoryUsage; }
            private set { _showMemoryUsage = value.Capitalize(); }
        }

        [DataMember]
        public string ShowOptimizationNotifications
        {
            get { return _showOptimizationNotifications; }
            private set { _showOptimizationNotifications = value.Capitalize(); }
        }

        [DataMember]
        public string ShowVirtualMemory
        {
            get { return _showVirtualMemory; }
            private set { _showVirtualMemory = value.Capitalize(); }
        }

        [DataMember]
        public string StandbyList
        {
            get { return _standbyList; }
            private set { _standbyList = value.Capitalize(); }
        }

        [DataMember]
        public string StandbyListLowPriority
        {
            get { return _standbyListLowPriority; }
            private set { _standbyListLowPriority = value.Capitalize(); }
        }

        [DataMember]
        public string StartMinimized
        {
            get { return _startMinimized; }
            private set { _startMinimized = value.Capitalize(); }
        }

        [DataMember]
        public string SystemFileCache
        {
            get { return _systemFileCache; }
            private set { _systemFileCache = value.Capitalize(); }
        }

        [DataMember]
        public string Text
        {
            get { return _text; }
            private set { _text = value.Capitalize(); }
        }

        [DataMember]
        public string TrayIcon
        {
            get { return _trayIcon; }
            private set { _trayIcon = value.Capitalize(); }
        }

        [DataMember]
        public string UpdatedToVersion
        {
            get { return _updatedToVersion; }
            private set { _updatedToVersion = value.Capitalize(); }
        }

        [DataMember]
        public string UseTransparentBackground
        {
            get { return _useTransparentBackground; }
            private set { _useTransparentBackground = value.Capitalize(); }
        }

        [DataMember]
        public string Used
        {
            get { return _used; }
            private set { _used = value.Capitalize(); }
        }

        [DataMember]
        public string VirtualMemory
        {
            get { return _virtualMemory; }
            private set { _virtualMemory = value.Capitalize(); }
        }

        [DataMember]
        public string WhenFreePhysicalMemoryIsBelow
        {
            get { return _whenFreePhysicalMemoryIsBelow; }
            private set { _whenFreePhysicalMemoryIsBelow = value.Capitalize(); }
        }

        [DataMember]
        public string WarningLevel
        {
            get { return _warningLevel; }
            private set { _warningLevel = value.Capitalize(); }
        }

        [DataMember]
        public string WorkingSet 
        {
            get { return _workingSet; }
            private set { _workingSet = value.Capitalize(); }
        }

        [DataMember]
        public string Yes
        {
            get { return _yes; }
            private set { _yes = value.Capitalize(); }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member