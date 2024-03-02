using System;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Memory (RAM)
    /// </summary>
    public class Memory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Memory" /> class.
        /// </summary>
        public Memory()
        {
            Physical = new MemoryStats(0, 0, 0);
            Virtual = new MemoryStats(0, 0, 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Memory" /> class.
        /// </summary>
        /// <param name="memoryStatusEx">The memory status ex.</param>
        public Memory(Structs.Windows.MemoryStatusEx memoryStatusEx)
        {
            if (memoryStatusEx == null)
                throw new ArgumentNullException("memoryStatusEx");

            Physical = new MemoryStats(memoryStatusEx.AvailPhys, memoryStatusEx.TotalPhys, memoryStatusEx.MemoryLoad);
            Virtual = new MemoryStats(memoryStatusEx.AvailPageFile, memoryStatusEx.TotalPageFile);
        }

        /// <summary>
        /// Physical
        /// </summary>
        public MemoryStats Physical { get; private set; }

        /// <summary>
        /// Virtual
        /// </summary>
        public MemoryStats Virtual { get; private set; }
    }
}