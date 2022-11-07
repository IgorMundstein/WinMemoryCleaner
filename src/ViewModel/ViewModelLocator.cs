namespace WinMemoryCleaner
{
    /// <summary>
    /// View Model Locator
    /// </summary>
    internal class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
        /// </summary>
        public ViewModelLocator()
        {
            MainViewModel = new MainViewModel(App.ComputerService, App.NotificationService);
        }

        /// <summary>
        /// Main View Model
        /// </summary>
        public MainViewModel MainViewModel { get; private set; }
    }
}
