namespace WinMemoryCleaner
{
    /// <summary>
    /// Dependency Injection Resolver
    /// </summary>
    internal static class DependencyInjection
    {
        /// <summary>
        /// Initializes the <see cref="DependencyInjection"/> class.
        /// </summary>
        static DependencyInjection()
        {
            // Logger
            Logger = new Logger();

            Configurator = new Configurator(Logger);

            // Services
            ComputerService = new ComputerService(Configurator, Logger);
            LoadingService = new LoadingService(Configurator, Logger);

            // View Models
            MainViewModel = new MainViewModel(ComputerService, Configurator, LoadingService, Logger);
        }

        /// <summary>
        /// Loading Service
        /// </summary>
        internal static readonly IComputerService ComputerService;

        /// <summary>
        /// Configurator
        /// </summary>
        internal static readonly IConfigurator Configurator;

        /// <summary>
        /// Loading Service
        /// </summary>
        internal static readonly ILoadingService LoadingService;

        /// <summary>
        /// Logger
        /// </summary>
        internal static readonly ILogger Logger;

        /// <summary>
        /// Main View Model
        /// </summary>
        internal static readonly MainViewModel MainViewModel;
    }
}
