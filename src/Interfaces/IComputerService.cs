namespace WinMemoryCleaner
{
    internal interface IComputerService
    {
        Memory GetMemory();

        OperatingSystem GetOperatingSystem();

        void MemoryClean(Enums.Memory.Area areas);
    }
}
