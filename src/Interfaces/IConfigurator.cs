namespace WinMemoryCleaner
{
    internal interface IConfigurator
    {
        Config Config { get; }
        
        void Save();
    }
}
