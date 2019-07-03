namespace WinMemoryCleaner
{
    /// <summary>
    /// Computer
    /// </summary>
    internal class Computer
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Computer"/> class.
        /// </summary>
        internal Computer()
        {
            MemoryAvailable = "0";
            MemorySize = "0";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the memory available.
        /// </summary>
        /// <value>
        /// The memory available.
        /// </value>
        public string MemoryAvailable { get; set; }

        /// <summary>
        /// Gets or sets the size of the memory.
        /// </summary>
        /// <value>
        /// The size of the memory.
        /// </value>
        public string MemorySize { get; set; }

        /// <summary>
        /// Gets or sets the memory usage.
        /// </summary>
        /// <value>
        /// The memory usage.
        /// </value>
        public long MemoryUsage { get; set; }

        #endregion
    }
}
