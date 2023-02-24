namespace WinMemoryCleaner
{
    internal interface IMemoryService
    {
        void CleanMemory(Enums.Memory.Area areas);

        Memory GetMemory();
    }
}
