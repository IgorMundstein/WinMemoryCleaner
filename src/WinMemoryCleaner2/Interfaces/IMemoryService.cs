namespace WinMemoryCleaner
{
    /// <summary>
    /// IMemoryService
    /// </summary>
    public interface IMemoryService
    {
        /// <summary>
        /// Gets the memory info (RAM)
        /// </summary>
        Memory Memory { get; }

        /// <summary>
        /// Optimize the computer
        /// </summary>
        /// <param name="areas">Memory areas</param>
        void Optimize(Enums.Memory.Areas areas);
    }
}
