namespace WinMemoryCleaner
{
    /// <summary>
    /// Memory Size
    /// </summary>

    public class MemorySize
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemorySize" /> class.
        /// </summary>
        /// <param name="bytes">The amount of memory, in bytes</param>
        public MemorySize(ulong bytes)
        {
            Bytes = bytes;

            var memory = bytes.ToMemoryUnit();

            Unit = memory.Value;
            Value = memory.Key;
        }

        /// <summary>
        /// Gets or sets the value in bytes.
        /// </summary>
        /// <value>
        /// The value in bytes.
        /// </value>
        public ulong Bytes { get; private set; }

        /// <summary>
        /// Gets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public uint Percentage { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public Enums.Memory.Unit Unit { get; private set; }

        /// <summary>
        /// Gets or sets the unit value.
        /// </summary>
        /// <value>
        /// The unit value.
        /// </value>
        public double Value { get; private set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(Localizer.Culture, "{0:0.#} {1} ({2}%)", Value, Unit, Percentage);
        }
    }
}
