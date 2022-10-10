namespace WinMemoryCleaner
{
    /// <summary>
    /// Service
    /// </summary>
    internal abstract class Service
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        /// <param name="configurator">Configurator</param>
        /// <param name="logger">Logger</param>
        protected Service(IConfigurator configurator, ILogger logger)
        {
            Configurator = configurator;
            Logger = logger;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Logger
        /// </summary>
        protected readonly IConfigurator Configurator;

        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger Logger;

        #endregion
    }
}
