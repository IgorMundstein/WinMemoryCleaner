using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Input;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Main View Model
    /// </summary>
    public class MainViewModel : ViewModel, IDisposable
    {
        #region Fields

        private Computer _computer;
        private readonly IComputerService _computerService;
        private readonly IHotKeyService _hotKeyService;
        private bool _isOptimizationKeyValid;
        private DateTimeOffset _lastAutoOptimizationByInterval = DateTimeOffset.Now;
        private DateTimeOffset _lastAutoOptimizationByMemoryUsage = DateTimeOffset.Now;
        private BackgroundWorker _monitorAppWorker;
        private BackgroundWorker _monitorComputerWorker;
        private byte _optimizationProgressPercentage;
        private string _optimizationProgressStep = Localizer.String.Optimize;
        private byte _optimizationProgressTotal = byte.MaxValue;
        private byte _optimizationProgressValue = byte.MinValue;
        private string _selectedProcess;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="computerService">Computer service</param>
        /// <param name="hotKeyService">Hotkey service.</param>
        /// <param name="notificationService">Notification service</param>
        public MainViewModel(IComputerService computerService, IHotKeyService hotKeyService, INotificationService notificationService)
            : base(notificationService)
        {
            _computerService = computerService;
            _hotKeyService = hotKeyService;

            // Commands
            AddProcessToExclusionListCommand = new RelayCommand<string>(AddProcessToExclusionList, () => CanAddProcessToExclusionList);
            OptimizeCommand = new RelayCommand(OptimizeAsync, () => CanOptimize);
            RemoveProcessFromExclusionListCommand = new RelayCommand<string>(RemoveProcessFromExclusionList);

            // Models
            Computer = new Computer();

            if (IsInDesignMode)
            {
                Computer.OperatingSystem.IsWindowsVistaOrGreater = true;
                Computer.OperatingSystem.IsWindowsXpOrGreater = true;
                IsOptimizationKeyValid = true;

                _hotKeyService = new HotKeyService();
            }
            else
            {
                _computerService.OnOptimizeProgressUpdate += OnOptimizeProgressUpdate;

                Computer.OperatingSystem = _computerService.OperatingSystem;

                RegisterOptimizationHotKey(Settings.OptimizationModifiers, Settings.OptimizationKey);
                MonitorAsync();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [always on top].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [always on top]; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysOnTop
        {
            get { return Settings.AlwaysOnTop; }
            set
            {
                try
                {
                    IsBusy = true;

                    Settings.AlwaysOnTop = value;
                    Settings.Save();

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
                }
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
            get { return Settings.AutoOptimizationInterval; }
            set
            {
                try
                {
                    IsBusy = true;

                    _lastAutoOptimizationByInterval = DateTimeOffset.Now;

                    Settings.AutoOptimizationInterval = value;
                    Settings.Save();

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
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
            get { return Settings.AutoOptimizationMemoryUsage; }
            set
            {
                try
                {
                    IsBusy = true;

                    _lastAutoOptimizationByMemoryUsage = DateTimeOffset.Now;

                    Settings.AutoOptimizationMemoryUsage = value;
                    Settings.Save();

                    RaisePropertyChanged();
                    RaisePropertyChanged(() => AutoOptimizationMemoryUsageDescription);
                }
                finally
                {
                    IsBusy = false;
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
            get { return string.Format(Localizer.Culture, Localizer.String.WhenFreeMemoryIsBelow, AutoOptimizationMemoryUsage); }
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
        /// Gets or sets a value indicating whether [automatic update].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic update]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoUpdate
        {
            get { return Settings.AutoUpdate; }
            set
            {
                try
                {
                    IsBusy = true;

                    Settings.AutoUpdate = value;
                    Settings.Save();

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can add process to exclusion list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can add process to exclusion list; otherwise, <c>false</c>.
        /// </value>
        public bool CanAddProcessToExclusionList
        {
            get { return SelectedProcess != null; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can optimize.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can optimize; otherwise, <c>false</c>.
        /// </value>
        public bool CanOptimize
        {
            get { return MemoryAreas != Enums.Memory.Areas.None; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [close after optimization].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [close after optimization]; otherwise, <c>false</c>.
        /// </value>
        public bool CloseAfterOptimization
        {
            get { return Settings.CloseAfterOptimization; }
            set
            {
                try
                {
                    IsBusy = true;

                    Settings.CloseAfterOptimization = value;
                    Settings.Save();

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
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
            get { return Settings.CloseToTheNotificationArea; }
            set
            {
                try
                {
                    IsBusy = true;

                    Settings.CloseToTheNotificationArea = value;
                    Settings.Save();

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [compact mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [compact mode]; otherwise, <c>false</c>.
        /// </value>
        public bool CompactMode
        {
            get { return Settings.CompactMode; }
            set
            {
                try
                {
                    IsBusy = true;

                    Settings.CompactMode = value;
                    Settings.Save();

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the computer.
        /// </summary>
        /// <value>
        /// The computer.
        /// </value>
        public Computer Computer
        {
            get { return _computer; }
            private set
            {
                _computer = value;
                RaisePropertyChanged();
            }
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
                _isOptimizationKeyValid = value;
                RaisePropertyChanged();
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
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public Language Language
        {
            get
            {
                return Localizer.Language;
            }
            set
            {
                try
                {
                    IsBusy = true;

                    if (Localizer.Language != null && Localizer.Language.Equals(value))
                        return;

                    Localizer.Language = value;

                    if (!IsInDesignMode)
                    {
                        Computer.Memory = _computerService.Memory;
                        RaisePropertyChanged(() => Computer);

                        NotificationService.Initialize();
                        NotificationService.Update(Computer.Memory);
                    }

                    RaisePropertyChanged(string.Empty);
                }
                catch (Exception e)
                {
                    NotificationService.Notify(e.Message);
                    Logger.Error(e);
                }
                finally
                {
                    IsBusy = false;
                }
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
                if (!Computer.OperatingSystem.HasCombinedPageList)
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.CombinedPageList;

                if (!Computer.OperatingSystem.HasModifiedPageList)
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.ModifiedPageList;

                if (!Computer.OperatingSystem.HasProcessesWorkingSet)
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.ProcessesWorkingSet;

                if (!Computer.OperatingSystem.HasStandbyList)
                {
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.StandbyList;
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.StandbyListLowPriority;
                }

                if (!Computer.OperatingSystem.HasSystemWorkingSet)
                    Settings.MemoryAreas &= ~Enums.Memory.Areas.SystemWorkingSet;

                return Settings.MemoryAreas;
            }
            set
            {
                try
                {
                    IsBusy = true;

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

                    RaisePropertyChanged();
                    RaisePropertyChanged(() => CanOptimize);
                }
                finally
                {
                    IsBusy = false;
                }
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
            set { RegisterOptimizationHotKey(Settings.OptimizationModifiers, value); }
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
        /// Gets or sets the optimization progress percentage.
        /// </summary>
        /// <value>
        /// The optimization progress percentage.
        /// </value>
        public byte OptimizationProgressPercentage
        {
            get { return _optimizationProgressPercentage; }
            set
            {
                _optimizationProgressPercentage = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the optimization progress step.
        /// </summary>
        /// <value>
        /// The optimization progress step.
        /// </value>
        public string OptimizationProgressStep
        {
            get { return _optimizationProgressStep; }
            set
            {
                _optimizationProgressStep = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the optimization progress total.
        /// </summary>
        /// <value>
        /// The optimization progress total.
        /// </value>
        public byte OptimizationProgressTotal
        {
            get { return _optimizationProgressTotal; }
            set
            {
                _optimizationProgressTotal = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the optimization progress value.
        /// </summary>
        /// <value>
        /// The optimization progress value.
        /// </value>
        public byte OptimizationProgressValue
        {
            get { return _optimizationProgressValue; }
            set
            {
                _optimizationProgressValue = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Physical Memory Header
        /// </summary>
        public string PhysicalMemoryHeader
        {
            get
            {
                return string.Format(Localizer.Culture, "{0} ({1:0.#} {2})", Localizer.String.Physical, Computer.Memory.Physical.Total.Value, Computer.Memory.Physical.Total.Unit);
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
                var processes = new ObservableCollection<string>(Process.GetProcesses()
                    .Where(process => process != null && !process.ProcessName.Equals(Constants.App.Name) && !Settings.ProcessExclusionList.Contains(process.ProcessName))
                    .Select(process => process.ProcessName.ToLower(Localizer.Culture).Replace(".exe", string.Empty))
                    .Distinct()
                    .OrderBy(name => name));

                if (!processes.Contains(SelectedProcess))
                    SelectedProcess = processes.FirstOrDefault();

                return processes;
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
            get { return new ObservableCollection<string>(Settings.ProcessExclusionList); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [run on low priority].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [run on low priority]; otherwise, <c>false</c>.
        /// </value>
        public bool RunOnLowPriority
        {
            get { return Settings.RunOnPriority == Enums.Priority.Low; }
            set
            {
                try
                {
                    IsBusy = true;

                    var priority = value ? Enums.Priority.Low : Enums.Priority.Normal;

                    App.SetPriority(priority);

                    Settings.RunOnPriority = priority;
                    Settings.Save();

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
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
            get { return Settings.RunOnStartup; }
            set
            {
                try
                {
                    IsBusy = true;

                    App.RunOnStartup(value);

                    Settings.RunOnStartup = value;
                    Settings.Save();

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
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
                _selectedProcess = value;
                RaisePropertyChanged();
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
            get { return Settings.ShowOptimizationNotifications; }
            set
            {
                try
                {
                    IsBusy = true;

                    Settings.ShowOptimizationNotifications = value;
                    Settings.Save();

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
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
            get { return Settings.ShowVirtualMemory; }
            set
            {
                try
                {
                    IsBusy = true;

                    Settings.ShowVirtualMemory = value;
                    Settings.Save();

                    NotificationService.Update(Computer.Memory);

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
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
            get { return Settings.StartMinimized; }
            set
            {
                try
                {
                    IsBusy = true;

                    Settings.StartMinimized = value;
                    Settings.Save();

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;

                return string.Format(Localizer.Culture, "{0} {1}.{2}", Constants.App.Title, version.Major, version.Minor);
            }
        }

        /// <summary>
        /// Gets or sets the tray icon.
        /// </summary>
        /// <value>
        /// The tray icon.
        /// </value>
        public Enums.Icon.Tray TrayIcon
        {
            get { return Settings.TrayIcon; }
            set
            {
                try
                {
                    IsBusy = true;

                    Settings.TrayIcon = value;
                    Settings.Save();

                    NotificationService.Update(Computer.Memory);

                    RaisePropertyChanged();
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        /// <summary>
        /// Virtual Memory Header
        /// </summary>
        public string VirtualMemoryHeader
        {
            get
            {
                return string.Format(Localizer.Culture, "{0} ({1:0.#} {2})", Localizer.String.Virtual, Computer.Memory.Virtual.Total.Value, Computer.Memory.Virtual.Total.Unit);
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_hotKeyService != null)
                    _hotKeyService.Dispose();

                if (_monitorAppWorker != null)
                {

                    try
                    {
                        _monitorAppWorker.CancelAsync();
                    }
                    catch
                    {
                        // ignored
                    }

                    try
                    {
                        _monitorAppWorker.Dispose();
                    }
                    catch
                    {
                        // ignored
                    }
                }

                if (_monitorComputerWorker != null)
                {

                    try
                    {
                        _monitorComputerWorker.CancelAsync();
                    }
                    catch
                    {
                        // ignored
                    }

                    try
                    {
                        _monitorComputerWorker.Dispose();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }

        #endregion

        #region Commands | Events

        /// <summary>
        /// Gets the add process to exclusion list.
        /// </summary>
        /// <value>
        /// The add process to exclusion list.
        /// </value>
        public ICommand AddProcessToExclusionListCommand { get; private set; }

        /// <summary>
        /// Gets the optimize command.
        /// </summary>
        /// <value>
        /// The optimize command.
        /// </value>
        public ICommand OptimizeCommand { get; private set; }

        /// <summary>
        /// Occurs when [optimize command is completed].
        /// </summary>
        public event Action OnOptimizeCommandCompleted;

        /// <summary>
        /// Gets the remove process from exclusion list command.
        /// </summary>
        /// <value>
        /// The remove process from exclusion list command.
        /// </value>
        public ICommand RemoveProcessFromExclusionListCommand { get; private set; }

        /// <summary>
        /// Occurs when [remove process from exclusion list command is completed].
        /// </summary>
        public event Action OnRemoveProcessFromExclusionListCommandCompleted;

        #endregion

        #region Methods

        /// <summary>
        /// Adds the process to exclusion list.
        /// </summary>
        /// <param name="process">The process.</param>
        private void AddProcessToExclusionList(string process)
        {
            try
            {
                IsBusy = true;

                if (!Settings.ProcessExclusionList.Contains(process) && !string.IsNullOrWhiteSpace(process))
                {
                    if (Settings.ProcessExclusionList.Add(process))
                    {
                        Settings.Save();

                        RaisePropertyChanged(() => Processes);
                        RaisePropertyChanged(() => ProcessExclusionList);
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Monitor App Resources
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        private void MonitorApp(object sender, DoWorkEventArgs e)
        {
            // App priority
            App.SetPriority(Settings.RunOnPriority);

            while (!_monitorAppWorker.CancellationPending)
            {
                try
                {
                    // Check if it's busy
                    if (IsBusy)
                        continue;

                    // Update app
                    if (Settings.AutoUpdate)
                        App.Update();

                    // Auto Optimization
                    if (CanOptimize)
                    {
                        // Interval
                        if (Settings.AutoOptimizationInterval > 0 &&
                            DateTimeOffset.Now.Subtract(_lastAutoOptimizationByInterval).TotalHours >= Settings.AutoOptimizationInterval)
                        {
                            OptimizeAsync();

                            _lastAutoOptimizationByInterval = DateTimeOffset.Now;
                        }
                        else
                        {
                            // Memory usage
                            if (Settings.AutoOptimizationMemoryUsage > 0 &&
                                Computer.Memory.Physical.Free.Percentage < Settings.AutoOptimizationMemoryUsage &&
                                DateTimeOffset.Now.Subtract(_lastAutoOptimizationByMemoryUsage).TotalMinutes >= Constants.App.AutoOptimizationMemoryUsageInterval)
                            {
                                OptimizeAsync();

                                _lastAutoOptimizationByMemoryUsage = DateTimeOffset.Now;
                            }
                        }
                    }

                    // Delay
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex.GetMessage());
                }
            }
        }

        /// <summary>
        /// Monitor Background Tasks
        /// </summary>
        private void MonitorAsync()
        {
            // Monitor App Resources
            try
            {
                using (_monitorAppWorker = new BackgroundWorker())
                {
                    _monitorAppWorker.DoWork += MonitorApp;
                    _monitorAppWorker.WorkerSupportsCancellation = true;
                    _monitorAppWorker.RunWorkerAsync();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            // Monitor Computer Resources
            try
            {
                using (_monitorComputerWorker = new BackgroundWorker())
                {
                    _monitorComputerWorker.DoWork += MonitorComputer;
                    _monitorComputerWorker.WorkerSupportsCancellation = true;
                    _monitorComputerWorker.RunWorkerAsync();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// Monitor Computer Resources
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        private void MonitorComputer(object sender, DoWorkEventArgs e)
        {
            // App priority
            App.SetPriority(Settings.RunOnPriority);

            while (!_monitorComputerWorker.CancellationPending)
            {
                try
                {
                    // Check if it's busy
                    if (IsBusy)
                        continue;

                    // Update memory info
                    Computer.Memory = _computerService.Memory;

                    RaisePropertyChanged(() => Computer);
                    RaisePropertyChanged(() => VirtualMemoryHeader);

                    NotificationService.Update(Computer.Memory);

                    // Delay
                    Thread.Sleep(5000);
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex.GetMessage());
                }
            }
        }

        /// <summary>
        /// Called when [optimize progress is update].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="step">The step.</param>
        private void OnOptimizeProgressUpdate(byte value, string step)
        {
            OptimizationProgressPercentage = (byte)(value * 100 / OptimizationProgressTotal);
            OptimizationProgressStep = step;
            OptimizationProgressValue = value;
        }

        /// <summary>
        /// Optimize
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        private void Optimize(object sender, DoWorkEventArgs e)
        {
            try
            {
                IsBusy = true;

                // App priority
                App.SetPriority(Settings.RunOnPriority);

                // Memory optimize
                var tempPhysicalAvailable = Computer.Memory.Physical.Free.Bytes;
                var tempVirtualAvailable = Computer.Memory.Virtual.Free.Bytes;

                _computerService.Optimize(Settings.MemoryAreas);

                // Update memory info
                Computer.Memory = _computerService.Memory;
                RaisePropertyChanged(() => Computer);

                // Notification
                if (Settings.ShowOptimizationNotifications)
                {
                    var physicalReleased = (Computer.Memory.Physical.Free.Bytes > tempPhysicalAvailable ? Computer.Memory.Physical.Free.Bytes - tempPhysicalAvailable : tempPhysicalAvailable - Computer.Memory.Physical.Free.Bytes).ToMemoryUnit();
                    var virtualReleased = (Computer.Memory.Virtual.Free.Bytes > tempVirtualAvailable ? Computer.Memory.Virtual.Free.Bytes - tempVirtualAvailable : tempVirtualAvailable - Computer.Memory.Virtual.Free.Bytes).ToMemoryUnit();

                    var message = Settings.ShowVirtualMemory
                        ? string.Format(Localizer.Culture, "{1}{0}{0}{2}: {3:0.#} {4} | {5}: {6:0.#} {7}", Environment.NewLine, Localizer.String.MemoryOptimized, Localizer.String.Physical, physicalReleased.Key, physicalReleased.Value, Localizer.String.Virtual, virtualReleased.Key, virtualReleased.Value)
                        : string.Format(Localizer.Culture, "{1}{0}{0}{2}: {3:0.#} {4}", Environment.NewLine, Localizer.String.MemoryOptimized, Localizer.String.Physical, physicalReleased.Key, physicalReleased.Value);

                    Notify(message);
                }

                if (OnOptimizeCommandCompleted != null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        OnOptimizeCommandCompleted();
                    });
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Optimize
        /// </summary>
        private void OptimizeAsync()
        {
            try
            {
                OptimizationProgressStep = Localizer.String.Optimize;
                OptimizationProgressValue = 0;
                OptimizationProgressTotal = (byte)(new BitArray(new[] { (int)Settings.MemoryAreas }).OfType<bool>().Count(x => x) + 1);

                using (var worker = new BackgroundWorker())
                {
                    worker.DoWork += Optimize;
                    worker.RunWorkerAsync();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
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
                IsBusy = true;

                _hotKeyService.Unregister(new HotKey(Settings.OptimizationModifiers, Settings.OptimizationKey));

                Settings.OptimizationKey = key;
                Settings.OptimizationModifiers = modifiers;

                var hotKey = new HotKey(Settings.OptimizationModifiers, Settings.OptimizationKey);
                IsOptimizationKeyValid = _hotKeyService.Register(hotKey, OptimizeAsync);

                if (!IsOptimizationKeyValid)
                {
                    var message = string.Format(Localizer.Culture, Localizer.String.HotkeyIsInUseByWindows, hotKey);

                    Logger.Warning(message);
                    NotificationService.Notify(message);

                    return;
                }

                Settings.Save();

                RaisePropertyChanged(() => OptimizationKey);
                RaisePropertyChanged(() => OptimizationModifiers);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Removes the process from exclusion list.
        /// </summary>
        /// <param name="process">The process.</param>
        private void RemoveProcessFromExclusionList(string process)
        {
            try
            {
                IsBusy = true;

                if (Settings.ProcessExclusionList.Remove(process))
                    Settings.Save();

                RaisePropertyChanged(() => Processes);
                RaisePropertyChanged(() => ProcessExclusionList);

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
                IsBusy = false;
            }
        }

        #endregion
    }
}
