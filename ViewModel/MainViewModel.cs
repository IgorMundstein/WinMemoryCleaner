using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using WinMemoryCleaner.Properties;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Main View Model
    /// </summary>
    internal class MainViewModel : ViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        internal MainViewModel()
        {
            try
            {
                CleanUpMemory = Resources.MainViewCleanUpMemory;
                Computer = new Computer
                {
                    MemoryAvailable = ComputerHelper.GetMemoryAvailable().ByteSizeToString(),
                    MemorySize = ComputerHelper.GetMemorySize().ByteSizeToString(),
                    MemoryUsage = ComputerHelper.GetMemoryUsage()
                };
                MemoryCleanCommand = new RelayCommand(MemoryClean);
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                Title = string.Format("{0} {1}.{2}", Resources.MainViewTitle, version.Major, version.Minor);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #endregion

        #region Fields

        private Computer _computer;
        private string _cleanUpMemory;
        private string _title;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the clean memory.
        /// </summary>
        /// <value>
        /// The clean memory.
        /// </value>
        public string CleanUpMemory
        {
            get
            {
                return _cleanUpMemory;
            }
            set
            {
                _cleanUpMemory = value;
                RaisePropertyChanged("CleanMemory");
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
            set
            {
                _computer = value;
                RaisePropertyChanged("Computer");
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
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
        internal void MemoryClean()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += MemoryClean;
            worker.RunWorkerAsync();
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
                // Loading
                Loading(true);

                // Memory clean
                ComputerHelper.MemoryClean();

                // Refresh memory information
                Computer.MemoryAvailable = ComputerHelper.GetMemoryAvailable().ByteSizeToString();
                Computer.MemorySize = ComputerHelper.GetMemorySize().ByteSizeToString();
                Computer.MemoryUsage = ComputerHelper.GetMemoryUsage();

                RaisePropertyChanged("Computer");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                // Loading
                Loading(false);
            }
        }

        #endregion
    }
}
