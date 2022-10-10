namespace WinMemoryCleaner
{
    /// <summary>
    /// View Model Locator
    /// </summary>
    internal class ViewModelLocator
    {
        /// <summary>
        /// Main View Model
        /// </summary>
        public MainViewModel MainViewModel { get { return DependencyInjection.MainViewModel; } }
    }
}
