namespace WinMemoryCleaner
{
    /// <summary>
    /// Memory
    /// </summary>
    internal class Memory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Memory" /> class.
        /// </summary>
        public Memory()
        {
            Free = "0";
            Total = "0";
            Used = "0";
        }

        /// <summary>
        /// Gets or sets the free physical memory.
        /// </summary>
        /// <value>
        /// The free physical memory.
        /// </value>
        public string Free { get; set; }

        /// <summary>
        /// Gets or sets the free physical memory percentage.
        /// </summary>
        /// <value>
        /// The free physical memory percentage.
        /// </value>
        public uint FreePercentage { get; set; }

        /// <summary>
        /// Gets or sets the total physical memory.
        /// </summary>
        /// <value>
        /// The total physical memory.
        /// </value>
        public string Total { get; set; }

        /// <summary>
        /// Gets or sets the used physical memory.
        /// </summary>
        /// <value>
        /// The used physical memory.
        /// </value>
        public string Used { get; set; }

        /// <summary>
        /// Gets or sets the used physical memory percentage.
        /// </summary>
        /// <value>
        /// The used physical memory percentage.
        /// </value>
        public uint UsedPercentage { get; set; }
    }
}