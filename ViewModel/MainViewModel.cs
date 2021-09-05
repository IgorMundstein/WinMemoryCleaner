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
    public class MainViewModel : ViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
            : base(App.LoadingService)
        {
            // Initialize
            Computer = new Computer();
            MemoryCleanCommand = new RelayCommand(MemoryClean, CanExecuteMemoryClean);

            Monitor();
        }

        #endregion

        #region Fields

        private Computer _computer;
        private BackgroundWorker _monitorWorker;

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
        /// Gets the git hub URI.
        /// </summary>
        /// <value>
        /// The git hub URI.
        /// </value>
        public static string GitHub
        {
            get
            {
                return Constants.App.GitHub;
            }
        }

        /// <summary>
        /// Gets the git hub URI.
        /// </summary>
        /// <value>
        /// The git hub URI.
        /// </value>
        public static Uri GitHubUri
        {
            get
            {
                return new Uri(Constants.App.GitHubUri);
            }
        }

        /// <summary>
        /// Gets the license.
        /// </summary>
        /// <value>
        /// The license.
        /// </value>
        public static string License
        {
            get
            {
                return Constants.App.License;
            }
        }

        /// <summary>
        /// Gets the logs.
        /// </summary>
        /// <value>
        /// The logs.
        /// </value>
        public ObservableCollection<Log> Logs
        {
            get { return new ObservableCollection<Log>(LogHelper.Logs); }
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
                if (Settings.MemoryAreas.HasFlag(value))
                    Settings.MemoryAreas &= ~value;
                else
                    Settings.MemoryAreas |= value;

                switch (value)
                {
                    case Enums.Memory.Area.StandbyList:
                        if (Settings.MemoryAreas.HasFlag(Enums.Memory.Area.StandbyListLowPriority))
                            Settings.MemoryAreas &= ~Enums.Memory.Area.StandbyListLowPriority;
                        break;

                    case Enums.Memory.Area.StandbyListLowPriority:
                        if (Settings.MemoryAreas.HasFlag(Enums.Memory.Area.StandbyList))
                            Settings.MemoryAreas &= ~Enums.Memory.Area.StandbyList;
                        break;
                }

                Settings.Save();
                RaisePropertyChanged();
            }
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
                using (_monitorWorker = new BackgroundWorker())
                {
                    _monitorWorker.DoWork += Monitor;
                    _monitorWorker.RunWorkerAsync();
                }
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
            }
        }

        /// <summary>
        /// Monitor
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void Monitor(object sender, DoWorkEventArgs e)
        {
            while (!_monitorWorker.CancellationPending)
            {
                // Refresh memory information
                Computer.MemoryAvailable = ComputerHelper.GetMemoryAvailable().ByteSizeToString();
                Computer.MemorySize = ComputerHelper.GetMemorySize().ByteSizeToString();
                Computer.MemoryUsage = ComputerHelper.GetMemoryUsage();

                if (IsInDesignMode)
                    break;

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
                LogHelper.Error(e);
            }
        }

        /// <summary>
        /// Memory clean
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void MemoryClean(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Loading ON
                Loading(true);

                // Logs
                LogHelper.Logs.Clear();
                RaisePropertyChanged(() => Logs);

                // Memory clean
                MemoryHelper.Clean();

                // Logs
                RaisePropertyChanged(() => Logs);
            }
            finally
            {
                // Loading OFF
                Loading(false);
            }
        }

        #endregion
    }
}
