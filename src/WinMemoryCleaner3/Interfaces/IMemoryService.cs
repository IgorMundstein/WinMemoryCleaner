using System.Threading.Tasks;

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
        Task Optimize(Enums.Memory.Areas areas);
    }
}