namespace WinMemoryCleaner
{
    /// <summary>
    /// Memory
    /// </summary>
    internal class Memory
    {
        public Memory()
        {
            Available = "0";
            Total = "0";
        }

        /// <summary>
        /// Gets or sets the available physical memory.
        /// </summary>
        /// <value>
        /// The available memory.
        /// </value>
        public string Available { get; set; }

        /// <summary>
        /// Gets or sets the total physical memory.
        /// </summary>
        /// <value>
        /// The total memory.
        /// </value>
        public string Total { get; set; }

        /// <summary>
        /// Gets or sets the memory usage.
        /// </summary>
        /// <value>
        /// The memory usage.
        /// </value>
        public uint Usage { get; set; }
    }
}