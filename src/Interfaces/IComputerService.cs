namespace WinMemoryCleaner
{
    /// <summary>
    /// IComputerService
    /// </summary>
    /// <seealso cref="IMemoryService" />
    /// <seealso cref="IOperatingSystem" />
    public interface IComputerService : IMemoryService, IOperatingSystem
    {
    }
}
