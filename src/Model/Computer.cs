namespace WinMemoryCleaner
{
    /// <summary>
    /// Computer
    /// </summary>
    public class Computer : Model
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Computer"/> class.
        /// </summary>
        public Computer()
        {
            MemoryAvailable = "0";
            MemorySize = "0";
        }

        #endregion

        #region Fields

        private string _memoryAvailable;
        private string _memorySize;
        private long _memoryUsage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the memory available.
        /// </summary>
        /// <value>
        /// The memory available.
        /// </value>
        public string MemoryAvailable
        {
            get
            {
                return _memoryAvailable;
            }
            set
            {
                _memoryAvailable = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the size of the memory.
        /// </summary>
        /// <value>
        /// The size of the memory.
        /// </value>
        public string MemorySize
        {
            get
            {
                return _memorySize;
            }
            set
            {
                _memorySize = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the memory usage.
        /// </summary>
        /// <value>
        /// The memory usage.
        /// </value>
        public long MemoryUsage
        {
            get
            {
                return _memoryUsage;
            }
            set
            {
                _memoryUsage = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
