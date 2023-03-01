using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Main View Model
    /// </summary>
    internal class MainViewModel : ViewModel, IDisposable
    {
        #region Fields

        private Computer _computer;
        private readonly IComputerService _computerService;
        private BackgroundWorker _monitorWorker;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="computerService">Computer service</param>
        /// <param name="notificationService">Notification service</param>
        public MainViewModel(IComputerService computerService, INotificationService notificationService)
            : base(notificationService)
        {
            _computerService = computerService;

            Computer = new Computer();
            MemoryCleanCommand = new RelayCommand(MemoryClean, CanExecuteMemoryClean);

            Monitor();
        }

        #endregion

        #region Properties

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
        /// Gets the logs.
        /// </summary>
        /// <value>
        /// The logs.
        /// </value>
        public ReadOnlyObservableCollection<Log> Logs
        {
            get { return Logger.Logs; }
        }

        /// <summary>
        /// Gets or sets the memory areas.
        /// </summary>
        /// <value>
        /// The memory areas.
        /// </value>
        public Enums.Memory.Area MemoryAreas
        {
            get
            {
                return Settings.MemoryAreas;
            }
            set
            {
                if ((Settings.MemoryAreas & value) != 0)
                    Settings.MemoryAreas &= ~value;
                else
                    Settings.MemoryAreas |= value;

                switch (value)
                {
                    case Enums.Memory.Area.StandbyList:
                        if ((Settings.MemoryAreas & Enums.Memory.Area.StandbyListLowPriority) != 0)
                            Settings.MemoryAreas &= ~Enums.Memory.Area.StandbyListLowPriority;
                        break;

                    case Enums.Memory.Area.StandbyListLowPriority:
                        if ((Settings.MemoryAreas & Enums.Memory.Area.StandbyList) != 0)
                            Settings.MemoryAreas &= ~Enums.Memory.Area.StandbyList;
                        break;
                }

                Settings.Save();
                RaisePropertyChanged();
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_monitorWorker != null)
                {

                    try
                    {
                        _monitorWorker.CancelAsync();
                    }
                    catch
                    {
                        // ignored
                    }

                    try
                    {
                        _monitorWorker.Dispose();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the memory clean command.
        /// </summary>
        /// <value>
        /// The memory clean command.
        /// </value>
        public ICommand MemoryCleanCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can execute memory clean].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can execute memory clean]; otherwise, <c>false</c>.
        /// </returns>
        private bool CanExecuteMemoryClean()
        {
            return MemoryAreas != Enums.Memory.Area.None;
        }

        /// <summary>
        /// Monitor Computer Resources
        /// </summary>
        private void Monitor()
        {
            try
            {
                if (IsInDesignMode)
                    return;

                using (_monitorWorker = new BackgroundWorker())
                {
                    _monitorWorker.DoWork += Monitor;
                    _monitorWorker.WorkerSupportsCancellation = true;
                    _monitorWorker.RunWorkerAsync();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// Monitor
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        private void Monitor(object sender, DoWorkEventArgs e)
        {
            Computer.OperatingSystem = _computerService.GetOperatingSystem();

            while (!_monitorWorker.CancellationPending)
            {
                // Update memory info
                Computer.Memory = _computerService.GetMemory();

                RaisePropertyChanged(() => Computer);

                // Update app
                if (!IsBusy)
                    App.Update();

                // Delay
                Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// Memory clean
        /// </summary>
        private void MemoryClean()
        {
            try
            {
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.DoWork += MemoryClean;
                    worker.RunWorkerAsync();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// Memory clean
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        private void MemoryClean(object sender, DoWorkEventArgs e)
        {
            try
            {
                IsBusy = true;

                // Clear logs
                Logger.Flush();

                // Memory clean
                _computerService.CleanMemory(Settings.MemoryAreas);

                // Notification
                Notify(Localization.MemoryCleaned);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
