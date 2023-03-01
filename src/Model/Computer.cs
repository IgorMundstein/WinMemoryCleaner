namespace WinMemoryCleaner
{
    /// <summary>
    /// Computer
    /// </summary>
    internal class Computer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Computer" /> class.
        /// </summary>
        internal Computer()
        {
            Memory = new Memory();
            OperatingSystem = new OperatingSystem();
        }

        /// <summary>
        /// Memory
        /// </summary>
        public Memory Memory { get; set; }

        /// <summary>
        /// Operating System
        /// </summary>
        internal OperatingSystem OperatingSystem { get; set; }
    }
}
