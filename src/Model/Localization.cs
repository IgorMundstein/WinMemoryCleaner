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
        private string _close, _closeAfterOptimization, _closeToTheNotificationArea, _collapse, _combinedPageList, _createStartMenuShortcut;
        private string _dangerLevel, _donate, _donationMessage, _donationTitle;
        private string _error, _errorAdminPrivilegeRequired, _errorCanNotSaveLog, _errorMemoryAreaOptimizationNotSupported, _errorResetCommand, _everyHour, _exit, _expand;
        private string _free;
        private string _garbageCollector;
        private string _help, _hotkeyIsInUseByOperatingSystem;
        private string _invalid;
        private string _lowMemory;
        private string _manual, _memoryAreas, _memoryOptimized, _memoryUsage, _minimize, _modifiedFileCache, _modifiedPageList;
        private string _no;
        private string _optimizationHotkey, _optimize, _optimizeOnMiddleMouseClick, _optimizing;
        private string _physicalMemory, _processExclusionList;
        private string _reason, _registryCache, _remove, _reset, _resetCommand, _resetConfirmation, _runOnLowPriority, _runOnStartup;
        private string _schedule, _seconds, _settings, _showMemoryUsage, _showOptimizationNotifications, _showVirtualMemory, _standbyList, _standbyListLowPriority, _startMinimized, _systemFileCache;
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
            set { _about = value; }
        }

        [DataMember]
        public string Add
        {
            get { return _add; }
            set { _add = value.Capitalize(); }
        }

        [DataMember]
        public string AlwaysOnTop
        {
            get { return _alwaysOnTop; }
            set { _alwaysOnTop = value.Capitalize(); }
        }

        [DataMember]
        public string AutoOptimization
        {
            get { return _autoOptimization; }
            set { _autoOptimization = value.Capitalize(); }
        }

        [DataMember]
        public string AutoOptimizationInterval
        {
            get { return _autoOptimizationInterval; }
            set { _autoOptimizationInterval = value.Capitalize(); }
        }

        [DataMember]
        public string AutoUpdate
        {
            get { return _autoUpdate; }
            set { _autoUpdate = value.Capitalize(); }
        }

        [DataMember]
        public string Background
        {
            get { return _background; }
            set { _background = value.Capitalize(); }
        }

        [DataMember]
        public string Close
        {
            get { return _close; }
            set { _close = value.Capitalize(); }
        }

        [DataMember]
        public string CloseAfterOptimization
        {
            get { return _closeAfterOptimization; }
            set { _closeAfterOptimization = value.Capitalize(); }
        }

        [DataMember]
        public string CloseToTheNotificationArea
        {
            get { return _closeToTheNotificationArea; }
            set { _closeToTheNotificationArea = value.Capitalize(); }
        }

        [DataMember]
        public string Collapse
        {
            get { return _collapse; }
            set { _collapse = value.Capitalize(); }
        }

        [DataMember]
        public string CombinedPageList
        {
            get { return _combinedPageList; }
            set { _combinedPageList = value.Capitalize(); }
        }

        [DataMember]
        public string CreateStartMenuShortcut
        {
            get { return _createStartMenuShortcut; }
            set { _createStartMenuShortcut = value.Capitalize(); }
        }

        [DataMember]
        public string DangerLevel
        {
            get { return _dangerLevel; }
            set { _dangerLevel = value.Capitalize(); }
        }

        [DataMember]
        public string Donate
        {
            get { return _donate; }
            set { _donate = value.Capitalize(); }
        }

        [DataMember]
        public string DonationMessage
        {
            get { return _donationMessage; }
            set { _donationMessage = value.Capitalize(); }
        }

        [DataMember]
        public string DonationTitle
        {
            get { return _donationTitle; }
            set { _donationTitle = value.Capitalize(); }
        }

        [DataMember]
        public string Error
        {
            get { return _error; }
            set { _error = value.Capitalize(); }
        }

        [DataMember]
        public string ErrorAdminPrivilegeRequired
        {
            get { return _errorAdminPrivilegeRequired; }
            set { _errorAdminPrivilegeRequired = value.Capitalize(); }
        }

        [DataMember]
        public string ErrorCanNotSaveLog
        {
            get { return _errorCanNotSaveLog; }
            set { _errorCanNotSaveLog = value.Capitalize(); }
        }

        [DataMember]
        public string ErrorMemoryAreaOptimizationNotSupported
        {
            get { return _errorMemoryAreaOptimizationNotSupported; }
            set { _errorMemoryAreaOptimizationNotSupported = value.Capitalize(); }
        }

        [DataMember]
        public string ErrorResetCommand
        {
            get { return _errorResetCommand; }
            set { _errorResetCommand = value.Capitalize(); }
        }

        [DataMember]
        public string EveryHour
        {
            get { return _everyHour; }
            set { _everyHour = value.Capitalize(); }
        }

        [DataMember]
        public string Exit
        {
            get { return _exit; }
            set { _exit = value.Capitalize(); }
        }

        [DataMember]
        public string Expand
        {
            get { return _expand; }
            set { _expand = value.Capitalize(); }
        }

        [DataMember]
        public string Free
        {
            get { return _free; }
            set { _free = value.Capitalize(); }
        }

        [DataMember]
        public string GarbageCollector
        {
            get { return _garbageCollector; }
            set { _garbageCollector = value.Capitalize(); }
        }

        [DataMember]
        public string Help
        {
            get { return _help; }
            set { _help = value.Capitalize(); }
        }

        [DataMember]
        public string HotkeyIsInUseByOperatingSystem
        {
            get { return _hotkeyIsInUseByOperatingSystem; }
            set { _hotkeyIsInUseByOperatingSystem = value.Capitalize(); }
        }

        [DataMember]
        public string Invalid
        {
            get { return _invalid; }
            set { _invalid = value.Capitalize(); }
        }

        [DataMember]
        public string LowMemory
        {
            get { return _lowMemory; }
            set { _lowMemory = value.Capitalize(); }
        }

        [DataMember]
        public string Manual
        {
            get { return _manual; }
            set { _manual = value.Capitalize(); }
        }

        [DataMember]
        public string MemoryAreas
        {
            get { return _memoryAreas; }
            set { _memoryAreas = value.Capitalize(); }
        }

        [DataMember]
        public string MemoryOptimized
        {
            get { return _memoryOptimized; }
            set { _memoryOptimized = value.Capitalize(); }
        }

        [DataMember]
        public string MemoryUsage
        {
            get { return _memoryUsage; }
            set { _memoryUsage = value.Capitalize(); }
        }

        [DataMember]
        public string Minimize
        {
            get { return _minimize; }
            set { _minimize = value.Capitalize(); }
        }

        [DataMember]
        public string ModifiedFileCache
        {
            get { return _modifiedFileCache; }
            set { _modifiedFileCache = value.Capitalize(); }
        }

        [DataMember]
        public string ModifiedPageList
        {
            get { return _modifiedPageList; }
            set { _modifiedPageList = value.Capitalize(); }
        }

        [DataMember]
        public string No
        {
            get { return _no; }
            set { _no = value.Capitalize(); }
        }

        [DataMember]
        public string OptimizationHotkey
        {
            get { return _optimizationHotkey; }
            set { _optimizationHotkey = value.Capitalize(); }
        }

        [DataMember]
        public string Optimize
        {
            get { return _optimize; }
            set { _optimize = value.Capitalize(); }
        }

        [DataMember]
        public string OptimizeOnMiddleMouseClick
        {
            get { return _optimizeOnMiddleMouseClick; }
            set { _optimizeOnMiddleMouseClick = value.Capitalize(); }
        }

        [DataMember]
        public string Optimizing
        {
            get { return _optimizing; }
            set { _optimizing = value.Capitalize(); }
        }

        [DataMember]
        public string PhysicalMemory
        {
            get { return _physicalMemory; }
            set { _physicalMemory = value.Capitalize(); }
        }

        [DataMember]
        public string ProcessExclusionList
        {
            get { return _processExclusionList; }
            set { _processExclusionList = value.Capitalize(); }
        }

        [DataMember]
        public string Reason
        {
            get { return _reason; }
            set { _reason = value.Capitalize(); }
        }

        [DataMember]
        public string RegistryCache
        {
            get { return _registryCache; }
            set { _registryCache = value.Capitalize(); }
        }

        [DataMember]
        public string Remove
        {
            get { return _remove; }
            set { _remove = value.Capitalize(); }
        }

        [DataMember]
        public string Reset
        {
            get { return _reset; }
            set { _reset = value.Capitalize(); }
        }

        [DataMember]
        public string ResetCommand
        {
            get { return _resetCommand; }
            set { _resetCommand = value.Capitalize(); }
        }

        [DataMember]
        public string ResetConfirmation
        {
            get { return _resetConfirmation; }
            set { _resetConfirmation = value.Capitalize(); }
        }

        [DataMember]
        public string RunOnLowPriority
        {
            get { return _runOnLowPriority; }
            set { _runOnLowPriority = value.Capitalize(); }
        }

        [DataMember]
        public string RunOnStartup
        {
            get { return _runOnStartup; }
            set { _runOnStartup = value.Capitalize(); }
        }

        [DataMember]
        public string Schedule
        {
            get { return _schedule; }
            set { _schedule = value.Capitalize(); }
        }

        [DataMember]
        public string Seconds
        {
            get { return _seconds; }
            set { _seconds = value.Capitalize(); }
        }

        [DataMember]
        public string Settings
        {
            get { return _settings; }
            set { _settings = value.Capitalize(); }
        }

        [DataMember]
        public string ShowMemoryUsage
        {
            get { return _showMemoryUsage; }
            set { _showMemoryUsage = value.Capitalize(); }
        }

        [DataMember]
        public string ShowOptimizationNotifications
        {
            get { return _showOptimizationNotifications; }
            set { _showOptimizationNotifications = value.Capitalize(); }
        }

        [DataMember]
        public string ShowVirtualMemory
        {
            get { return _showVirtualMemory; }
            set { _showVirtualMemory = value.Capitalize(); }
        }

        [DataMember]
        public string StandbyList
        {
            get { return _standbyList; }
            set { _standbyList = value.Capitalize(); }
        }

        [DataMember]
        public string StandbyListLowPriority
        {
            get { return _standbyListLowPriority; }
            set { _standbyListLowPriority = value.Capitalize(); }
        }

        [DataMember]
        public string StartMinimized
        {
            get { return _startMinimized; }
            set { _startMinimized = value.Capitalize(); }
        }

        [DataMember]
        public string SystemFileCache
        {
            get { return _systemFileCache; }
            set { _systemFileCache = value.Capitalize(); }
        }

        [DataMember]
        public string Text
        {
            get { return _text; }
            set { _text = value.Capitalize(); }
        }

        [DataMember]
        public string TrayIcon
        {
            get { return _trayIcon; }
            set { _trayIcon = value.Capitalize(); }
        }

        [DataMember]
        public string UpdatedToVersion
        {
            get { return _updatedToVersion; }
            set { _updatedToVersion = value.Capitalize(); }
        }

        [DataMember]
        public string UseTransparentBackground
        {
            get { return _useTransparentBackground; }
            set { _useTransparentBackground = value.Capitalize(); }
        }

        [DataMember]
        public string Used
        {
            get { return _used; }
            set { _used = value.Capitalize(); }
        }

        [DataMember]
        public string VirtualMemory
        {
            get { return _virtualMemory; }
            set { _virtualMemory = value.Capitalize(); }
        }

        [DataMember]
        public string WhenFreePhysicalMemoryIsBelow
        {
            get { return _whenFreePhysicalMemoryIsBelow; }
            set { _whenFreePhysicalMemoryIsBelow = value.Capitalize(); }
        }

        [DataMember]
        public string WarningLevel
        {
            get { return _warningLevel; }
            set { _warningLevel = value.Capitalize(); }
        }

        [DataMember]
        public string WorkingSet 
        {
            get { return _workingSet; }
            set { _workingSet = value.Capitalize(); }
        }

        [DataMember]
        public string Yes
        {
            get { return _yes; }
            set { _yes = value.Capitalize(); }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member