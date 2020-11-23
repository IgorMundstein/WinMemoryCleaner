using System;
using System.ComponentModel;
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
            MemoryCleanCommand = new RelayCommand(MemoryClean);

            // Refresh
            Refresh();
        }

        #endregion

        #region Fields

        private Computer _computer;

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
            set
            {
                _computer = value;
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
        /// Memory clean
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Logged")]
        internal void MemoryClean()
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
        /// Verify Command Signaling
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void MemoryClean(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Loading ON
                Loading(true);

                // Memory clean
                MemoryHelper.Clean();

                // Refresh
                Refresh();
            }
            finally
            {
                // Loading OFF
                Loading(false);
            }
        }

        /// <summary>
        /// Refresh
        /// </summary>
        private void Refresh()
        {
            // Refresh memory information
            Computer.MemoryAvailable = ComputerHelper.GetMemoryAvailable().ByteSizeToString();
            Computer.MemorySize = ComputerHelper.GetMemorySize().ByteSizeToString();
            Computer.MemoryUsage = ComputerHelper.GetMemoryUsage();
        }

        #endregion
    }
}
