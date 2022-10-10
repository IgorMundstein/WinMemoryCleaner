using System;
using System.Windows;
using System.Windows.Input;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Loading Service
    /// </summary>
    internal class LoadingService : Service, ILoadingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingService"/> class.
        /// </summary>
        /// <param name="configurator">Configurator</param>
        /// <param name="logger">Logger</param>
        public LoadingService(IConfigurator configurator, ILogger logger)
            : base(configurator, logger)
        {
        }

        /// <summary>
        /// Show/Hide Loading
        /// </summary>
        /// <param name="running">True (ON) / False (OFF)</param>
        public void Loading(bool running)
        {
            // Multi-threading trick
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                Mouse.OverrideCursor = running ? Cursors.Wait : null;
            });
        }
    }
}
