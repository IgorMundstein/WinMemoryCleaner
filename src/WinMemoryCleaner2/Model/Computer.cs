namespace WinMemoryCleaner
{
    /// <summary>
    /// Computer
    /// </summary>
    public class Computer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Computer" /> class.
        /// </summary>
        public Computer()
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
        public OperatingSystem OperatingSystem { get; set; }

    }
}
