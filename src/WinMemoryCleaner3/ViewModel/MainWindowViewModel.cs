using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WinMemoryCleaner;

namespace WinMemoryCleaner3.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IComputerService _computerService;
        private readonly IHotKeyService _hotKeyService;
        private readonly INotificationManager notificationManager;

        private DateTimeOffset _lastAutoOptimizationByInterval = DateTimeOffset.Now;
        private DateTimeOffset _lastAutoOptimizationByMemoryUsage = DateTimeOffset.Now;

        private WinMemoryCleaner.OperatingSystem _os;
        private Memory _memory;

        private bool _canOptimize = false;
        private Language _language = Localizer.Language;
        private Enums.Icon.Tray _trayIcon = Settings.TrayIcon;

        private bool _alwaysOnTop = Settings.AlwaysOnTop;
        private bool _autoUpdate = Settings.AutoUpdate;
        private bool _closeAfterOptimization = Settings.CloseAfterOptimization;
        private bool _runOnStartup = Settings.RunOnStartup;
        private bool _closeToTheNotificationArea = Settings.CloseToTheNotificationArea;
        private bool _showOptimizationNotifications = Settings.ShowOptimizationNotifications;
        private bool _showVirtualMemory = Settings.ShowVirtualMemory;
        private bool _startMinimized = Settings.StartMinimized;

        private ObservableCollection<string> _processes = new ObservableCollection<string>(Process.GetProcesses()
            .Where(process => process != null && !process.ProcessName.Equals(Constants.App.Name) && !Settings.ProcessExclusionList.Contains(process.ProcessName))
            .Select(process => process.ProcessName.ToLower(Localizer.Culture).Replace(".exe", string.Empty))
            .Distinct()
            .OrderBy(name => name));

        private string _selectedProcess;
        private bool _canAddProcessToExclusionList = false;
        private ObservableCollection<string> _processExclusionList = new ObservableCollection<string>(Settings.ProcessExclusionList);

        private int _autoOptimizationInterval = Settings.AutoOptimizationInterval;
        private int _autoOptimizationMemoryUsage = Settings.AutoOptimizationMemoryUsage;
        private string _autoOptimizationMemoryUsageDescription = string.Format(Localizer.Culture, Localizer.String.WhenFreeMemoryIsBelow, Settings.AutoOptimizationMemoryUsage);

        private bool _isOptimizationKeyValid;

        private string _physicalMemoryHeader;
        private string _virtualMemoryHeader;

        public WinMemoryCleaner.OperatingSystem OS
        {
            get { return _os; }
            set
            {
                SetProperty(ref _os, value);
            }
        }

        public Memory Memory
        {
            get { return _memory; }
            set
            {
                SetProperty(ref _memory, value);
                PhysicalMemoryHeader = string.Format(Localizer.Culture, "{0} ({1:0.#} {2})", Localizer.String.Physical, Memory.Physical.Total.Value, Memory.Physical.Total.Unit);
                VirtualMemoryHeader = string.Format(Localizer.Culture, "{0} ({1:0.#} {2})", Localizer.String.Virtual, Memory.Virtual.Total.Value, Memory.Virtual.Total.Unit);
            }
        }

        /// <summary>
        /// Gets or sets the memory areas.
        /// </summary>
        /// <value>
        /// The memory areas.
        /// </value>
        public Enums.Memory.Areas MemoryAreas
        {
            get
            {
                if (!_computerService.OperatingSystem.HasCombinedPageList)
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.CombinedPageList;

                if (!_computerService.OperatingSystem.HasModifiedPageList)
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.ModifiedPageList;

                if (!_computerService.OperatingSystem.HasProcessesWorkingSet)
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.ProcessesWorkingSet;

                if (!_computerService.OperatingSystem.HasStandbyList)
                {
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.StandbyList;
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.StandbyListLowPriority;
                }

                if (!_computerService.OperatingSystem.HasSystemWorkingSet)
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.SystemWorkingSet;

                return Settings.MemoryAreas;
            }
            set
            {
                try
                {
                    if ((Settings.MemoryAreas & value) != 0)
                        Settings.MemoryAreas &= ~value;
                    else
                        Settings.MemoryAreas |= value;

                    switch (value)
                    {
                        case Enums.Memory.Areas.StandbyList:
                            if ((Settings.MemoryAreas & Enums.Memory.Areas.StandbyListLowPriority) != 0)
                                Settings.MemoryAreas &= ~Enums.Memory.Areas.StandbyListLowPriority;
                            break;

                        case Enums.Memory.Areas.StandbyListLowPriority:
                            if ((Settings.MemoryAreas & Enums.Memory.Areas.StandbyList) != 0)
                                Settings.MemoryAreas &= ~Enums.Memory.Areas.StandbyList;
                            break;
                    }

                    Settings.Save();
                    CanOptimize = (MemoryAreas != Enums.Memory.Areas.None);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can optimize.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can optimize; otherwise, <c>false</c>.
        /// </value>
        public bool CanOptimize
        {
            get
            {
                return _canOptimize;
                //return MemoryAreas != Enums.Memory.Areas.None;
            }
            set
            {
                SetProperty(ref _canOptimize, value);
                OptimizeCommand.CanExecute(true);
            }
        }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>

        public Language Language
        {
            get => _language;
            set
            {
                try
                {
                    if (Localizer.Language != null && Localizer.Language.Equals(value))
                        return;
                    Localizer.Language = value;
                    SetProperty(ref _language, value);
                    Memory = _computerService.Memory;
                    AutoOptimizationMemoryUsageDescription = string.Format(Localizer.Culture, Localizer.String.WhenFreeMemoryIsBelow, Settings.AutoOptimizationMemoryUsage);
                }
                catch (Exception e)
                {
                    notificationManager.Show(e.Message, NotificationType.Warning);
                    Logger.Error(e);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets the keyboard keys.
        /// </summary>
        /// <value>
        /// The keyboard keys.
        /// </value>
        public List<Key> KeyboardKeys
        {
            get
            {
                return _hotKeyService.Keys;
            }
        }

        /// <summary>
        /// Gets the keyboard modifiers.
        /// </summary>
        /// <value>
        /// The keyboard modifiers.
        /// </value>
        public Dictionary<ModifierKeys, string> KeyboardModifiers
        {
            get
            {
                return _hotKeyService.Modifiers;
            }
        }

        /// <summary>
        /// Gets or sets the optimization key.
        /// </summary>
        /// <value>
        /// The optimization key.
        /// </value>
        public Key OptimizationKey
        {
            get { return Settings.OptimizationKey; }
            set
            {
                RegisterOptimizationHotKey(Settings.OptimizationModifiers, value);
            }
        }

        /// <summary>
        /// Gets or sets the optimization modifiers.
        /// </summary>
        /// <value>
        /// The optimization modifiers.
        /// </value>
        public ModifierKeys OptimizationModifiers
        {
            get { return Settings.OptimizationModifiers; }
            set { RegisterOptimizationHotKey(value, Settings.OptimizationKey); }
        }

        /// <summary>
        /// Gets or sets the tray icon.
        /// </summary>
        /// <value>
        /// The tray icon.
        /// </value>

        public Enums.Icon.Tray TrayIcon
        {
            get { return _trayIcon; }
            set
            {
                try
                {
                    Settings.TrayIcon = value;
                    Settings.Save();
                    SetProperty(ref _trayIcon, value);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [always on top].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [always on top]; otherwise, <c>false</c>.
        /// </value>

        public bool AlwaysOnTop
        {
            get => _alwaysOnTop;
            set
            {
                try
                {
                    Settings.AlwaysOnTop = value;
                    Settings.Save();
                    SetProperty(ref _alwaysOnTop, value);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic update].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic update]; otherwise, <c>false</c>.
        /// </value>

        public bool AutoUpdate
        {
            get => _autoUpdate;
            set
            {
                try
                {
                    Settings.AutoUpdate = value;
                    Settings.Save();
                    SetProperty(ref _autoUpdate, value);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [close after optimization].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [close after optimization]; otherwise, <c>false</c>.
        /// </value>
        public bool CloseAfterOptimization
        {
            get => _closeAfterOptimization;
            set
            {
                try
                {
                    Settings.CloseAfterOptimization = value;
                    Settings.Save();
                    SetProperty(ref _closeAfterOptimization, value);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [close to the notification area].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [close to the notification area]; otherwise, <c>false</c>.
        /// </value>

        public bool CloseToTheNotificationArea
        {
            get => _closeToTheNotificationArea;
            set
            {
                try
                {
                    Settings.CloseToTheNotificationArea = value;
                    Settings.Save();
                    SetProperty(ref _closeToTheNotificationArea, value);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [run on startup].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [run on startup]; otherwise, <c>false</c>.
        /// </value>

        public bool RunOnStartup
        {
            get => _runOnStartup;
            set
            {
                try
                {
                    App.RunOnStartup(value);
                    Settings.RunOnStartup = value;
                    Settings.Save();
                    SetProperty(ref _runOnStartup, value);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show optimization notifications].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show optimization notifications]; otherwise, <c>false</c>.
        /// </value>

        public bool ShowOptimizationNotifications
        {
            get => _showOptimizationNotifications;
            set
            {
                try
                {
                    Settings.ShowOptimizationNotifications = value;
                    Settings.Save();
                    SetProperty(ref _showOptimizationNotifications, value);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show virtual memory].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show virtual memory]; otherwise, <c>false</c>.
        /// </value>

        public bool ShowVirtualMemory
        {
            get { return _showVirtualMemory; }
            set
            {
                try
                {
                    Settings.ShowVirtualMemory = value;
                    Settings.Save();
                    SetProperty(ref _showVirtualMemory, value);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [start minimized].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [start minimized]; otherwise, <c>false</c>.
        /// </value>

        public bool StartMinimized
        {
            get => _startMinimized;
            set
            {
                try
                {
                    Settings.StartMinimized = value;
                    Settings.Save();
                    SetProperty(ref _startMinimized, value);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected process.
        /// </summary>
        /// <value>
        /// The selected process.
        /// </value>

        public string SelectedProcess
        {
            get { return _selectedProcess; }
            set
            {
                SetProperty(ref _selectedProcess, value);
                CanAddProcessToExclusionList = value != null;
            }
        }

        /// <summary>
        /// Gets or sets the automatic optimization interval.
        /// </summary>
        /// <value>
        /// The automatic optimization interval.
        /// </value>

        public int AutoOptimizationInterval
        {
            get { return _autoOptimizationInterval; }
            set
            {
                try
                {
                    _lastAutoOptimizationByInterval = DateTimeOffset.Now;
                    Settings.AutoOptimizationInterval = value;
                    Settings.Save();
                    SetProperty(ref _autoOptimizationInterval, value);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets the automatic optimization memory usage.
        /// </summary>
        /// <value>
        /// The automatic optimization memory usage.
        /// </value>

        public int AutoOptimizationMemoryUsage
        {
            get { return _autoOptimizationMemoryUsage; }
            set
            {
                try
                {
                    _lastAutoOptimizationByMemoryUsage = DateTimeOffset.Now;

                    Settings.AutoOptimizationMemoryUsage = value;
                    Settings.Save();
                    SetProperty(ref _autoOptimizationMemoryUsage, value);
                    AutoOptimizationMemoryUsageDescription = string.Format(Localizer.Culture, Localizer.String.WhenFreeMemoryIsBelow, AutoOptimizationMemoryUsage);
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Gets the automatic optimization memory usage description.
        /// </summary>
        /// <value>
        /// The automatic optimization memory usage description.
        /// </value>
        public string AutoOptimizationMemoryUsageDescription
        {
            get { return _autoOptimizationMemoryUsageDescription; }
            set
            {
                SetProperty(ref _autoOptimizationMemoryUsageDescription, value);
            }
        }

        /// <summary>
        /// Gets the automatic optimization memory usage warning.
        /// </summary>
        /// <value>
        /// The automatic optimization memory usage warning.
        /// </value>
        public string AutoOptimizationMemoryUsageWarning
        {
            get { return string.Format(Localizer.Culture, Localizer.String.AutoOptimizationInterval, Constants.App.AutoOptimizationMemoryUsageInterval); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether optimization key is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if optimization key is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsOptimizationKeyValid
        {
            get { return _isOptimizationKeyValid; }
            set
            {
                SetProperty(ref _isOptimizationKeyValid, value);
            }
        }

        /// <summary>
        /// Gets the processes.
        /// </summary>
        /// <value>
        /// The processes.
        /// </value>
        public ObservableCollection<string> Processes
        {
            get
            {
                if (!_processes.Contains(SelectedProcess))
                    SelectedProcess = _processes.FirstOrDefault();
                return _processes;
            }
            set
            {
                SetProperty(ref _processes, value);
            }
        }

        public bool CanAddProcessToExclusionList
        {
            get { return _canAddProcessToExclusionList; }
            set
            {
                SetProperty(ref _canAddProcessToExclusionList, value);
            }
        }

        /// <summary>
        /// Gets or sets the process exclusion list.
        /// </summary>
        /// <value>
        /// The process exclusion list.
        /// </value>
        public ObservableCollection<string> ProcessExclusionList
        {
            get { return _processExclusionList; }
            set { SetProperty(ref _processExclusionList, value); }
        }


        /// <summary>
        /// Physical Memory Header
        /// </summary>
        public string PhysicalMemoryHeader
        {
            get
            {
                return _physicalMemoryHeader;
            }
            set
            {
                SetProperty(ref _physicalMemoryHeader, value);
            }
        }


        /// <summary>
        /// Virtual Memory Header
        /// </summary>
        public string VirtualMemoryHeader
        {
            get
            {
                return _virtualMemoryHeader;
            }
            set
            {
                SetProperty(ref _virtualMemoryHeader, value);
            }
        }

        /// <summary>
        /// Gets the optimize command.
        /// </summary>
        /// <value>
        /// The optimize command.
        /// </value>
        public IAsyncRelayCommand OptimizeCommand { get; private set; }

        public ICommand RemoveProcessFromExclusionListCommand { get; set; }
        public ICommand AddProcessToExclusionListCommand { get; set; }

        /// <summary>
        /// Occurs when [remove process from exclusion list command is completed].
        /// </summary>
        public event Action OnRemoveProcessFromExclusionListCommandCompleted;

        public MainWindowViewModel(IComputerService computerService, IHotKeyService hotKeyService, INotificationManager manager)
        {
            _computerService = computerService;
            _hotKeyService = hotKeyService;
            notificationManager = manager;
            Memory = _computerService.Memory;
            OS = _computerService.OperatingSystem;
            RemoveProcessFromExclusionListCommand = new RelayCommand<string>(RemoveProcessFromExclusionList);
            AddProcessToExclusionListCommand = new RelayCommand<string>(AddProcessToExclusionList, (x) => CanAddProcessToExclusionList);
            OptimizeCommand = new AsyncRelayCommand(Optimize);
            RegisterOptimizationHotKey(Settings.OptimizationModifiers, Settings.OptimizationKey);
            CanOptimize = (MemoryAreas != Enums.Memory.Areas.None);
        }

        /// <summary>
        /// Removes the process from exclusion list.
        /// </summary>
        /// <param name="process">The process.</param>
        private void RemoveProcessFromExclusionList(string process)
        {
            try
            {
                if (Settings.ProcessExclusionList.Remove(process))
                {
                    ProcessExclusionList = new ObservableCollection<string>(Settings.ProcessExclusionList);
                    Settings.Save();
                }
                if (OnRemoveProcessFromExclusionListCommandCompleted != null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        OnRemoveProcessFromExclusionListCommandCompleted();
                    });
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// Adds the process to exclusion list.
        /// </summary>
        /// <param name="process">The process.</param>
        private void AddProcessToExclusionList(string process)
        {
            try
            {
                if (!Settings.ProcessExclusionList.Contains(process) && !string.IsNullOrWhiteSpace(process))
                {
                    if (Settings.ProcessExclusionList.Add(process))
                    {
                        ProcessExclusionList = new ObservableCollection<string>(Settings.ProcessExclusionList);
                        Settings.Save();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// Registers the optimization hotkey.
        /// </summary>
        /// <param name="modifiers">The modifiers.</param>
        /// <param name="key">The key.</param>
        private void RegisterOptimizationHotKey(ModifierKeys modifiers, Key key)
        {
            try
            {
                _hotKeyService.Unregister(new HotKey(Settings.OptimizationModifiers, Settings.OptimizationKey));

                Settings.OptimizationKey = key;
                Settings.OptimizationModifiers = modifiers;

                var hotKey = new HotKey(Settings.OptimizationModifiers, Settings.OptimizationKey);
                IsOptimizationKeyValid = _hotKeyService.Register(hotKey, HotKeyPressed);

                if (!IsOptimizationKeyValid)
                {
                    var message = string.Format(Localizer.Culture, Localizer.String.HotkeyIsInUseByWindows, hotKey);

                    Logger.Warning(message);
                    //taskbarIcon?.ShowBalloonTip("", message, BalloonIcon.Info);
                    return;
                }

                Settings.Save();
            }
            finally
            {
            }
        }

        private void HotKeyPressed()
        {
            Optimize();
        }

        /// <summary>
        /// Optimize
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        private async Task Optimize()
        {
            try
            {
                CanOptimize = false;
                // App priority
                App.SetPriority(Settings.RunOnPriority);

                // Memory optimize
                var tempPhysicalAvailable = Memory.Physical.Free.Bytes;
                var tempVirtualAvailable = Memory.Virtual.Free.Bytes;

                await _computerService.Optimize(Settings.MemoryAreas);

                // Update memory info
                Memory = _computerService.Memory;

                // Notification
                if (Settings.ShowOptimizationNotifications)
                {
                    var physicalReleased = (Memory.Physical.Free.Bytes > tempPhysicalAvailable ? Memory.Physical.Free.Bytes - tempPhysicalAvailable : tempPhysicalAvailable - Memory.Physical.Free.Bytes).ToMemoryUnit();
                    var virtualReleased = (Memory.Virtual.Free.Bytes > tempVirtualAvailable ? Memory.Virtual.Free.Bytes - tempVirtualAvailable : tempVirtualAvailable - Memory.Virtual.Free.Bytes).ToMemoryUnit();

                    var message = Settings.ShowVirtualMemory
                        ? string.Format(Localizer.Culture, "{1}{0}{0}{2}: {3:0.#} {4} | {5}: {6:0.#} {7}", Environment.NewLine, Localizer.String.MemoryOptimized, Localizer.String.Physical, physicalReleased.Key, physicalReleased.Value, Localizer.String.Virtual, virtualReleased.Key, virtualReleased.Value)
                        : string.Format(Localizer.Culture, "{1}{0}{0}{2}: {3:0.#} {4}", Environment.NewLine, Localizer.String.MemoryOptimized, Localizer.String.Physical, physicalReleased.Key, physicalReleased.Value);

                    notificationManager.Show(message, NotificationType.Notification);
                }
                CanOptimize = (MemoryAreas != Enums.Memory.Areas.None);

                if (Settings.CloseAfterOptimization)
                {
                    App.Current.Shutdown();
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// Monitor Computer Resources
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        public void MonitorComputer()
        {
            // App priority
            App.SetPriority(Settings.RunOnPriority);
            try
            {
                // Update memory info
                Memory = _computerService.Memory;
                CanOptimize = (MemoryAreas != Enums.Memory.Areas.None);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.GetMessage());
            }
        }

        /// <summary>
        /// Monitor App Resources
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        public async void MonitorApp()
        {
            // App priority
            App.SetPriority(Settings.RunOnPriority);
            try
            {
                //// Update app
                //if (Settings.AutoUpdate)
                //    App.Update();
                // Auto Optimization
                if (CanOptimize)
                {
                    // Interval
                    if (Settings.AutoOptimizationInterval > 0 &&
                        DateTimeOffset.Now.Subtract(_lastAutoOptimizationByInterval).TotalSeconds >= Settings.AutoOptimizationInterval)
                    {
                        await Optimize();

                        _lastAutoOptimizationByInterval = DateTimeOffset.Now;
                    }
                    else
                    {
                        // Memory usage
                        if (Settings.AutoOptimizationMemoryUsage > 0 &&
                            Memory.Physical.Free.Percentage < Settings.AutoOptimizationMemoryUsage &&
                            DateTimeOffset.Now.Subtract(_lastAutoOptimizationByMemoryUsage).TotalSeconds >= Constants.App.AutoOptimizationMemoryUsageInterval)
                        {
                            await Optimize();
                            _lastAutoOptimizationByMemoryUsage = DateTimeOffset.Now;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.GetMessage());
            }
        }
    }
}