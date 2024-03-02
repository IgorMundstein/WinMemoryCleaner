namespace WinMemoryCleaner
{
    /// <summary>
    /// Memory Stats
    /// </summary>
    public class MemoryStats
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryStats" /> class.
        /// </summary>
        /// <param name="free">The amount of memory currently available</param>
        /// <param name="total">The amount of actual memory</param>
        /// <param name="used">A number between 0 and 100 that specifies the approximate amount of memory that is in use</param>
        public MemoryStats(ulong free, ulong total, uint? used = null)
        {
            Free = new MemorySize(free);
            Total = new MemorySize(total);
            Used = new MemorySize(total >= free ? total - free : free - total);

            if (used == null)
                used = Used.Value > 0 && Total.Value > 0 ? (uint)(Used.Value * 100 / Total.Value) : 0;

            Free.Percentage = (uint)(100 - used);
            Used.Percentage = (uint)used;
        }

        /// <summary>
        /// Gets or sets the free memory.
        /// </summary>
        /// <value>
        /// The free memory.
        /// </value>
        public MemorySize Free { get; private set; }

        /// <summary>
        /// Gets or sets the total memory.
        /// </summary>
        /// <value>
        /// The total memory.
        /// </value>
        public MemorySize Total { get; private set; }

        /// <summary>
        /// Gets or sets the used memory.
        /// </summary>
        /// <value>
        /// The used memory.
        /// </value>
        public MemorySize Used { get; private set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Total.Value:0.#} {Total.Unit} | {Localizer.String.Used} - {Used} | {Localizer.String.Free} - {Free}";
        }
    }
}