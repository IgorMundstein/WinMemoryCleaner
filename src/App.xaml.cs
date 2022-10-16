using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

[assembly: CLSCompliant(true)]
namespace WinMemoryCleaner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        internal App()
        {
            // Log
#if DEBUG
            DependencyInjection.Logger.Level = Enums.Log.Level.Debug;
#else
            DependencyInjection.Logger.Level = Enums.Log.Level.Information;
#endif

            // Events
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks for updates.
        /// </summary>
        public void CheckForUpdates()
        {
            //TODO: Implement auto-update

            //Version version = Assembly.GetExecutingAssembly().GetName().Version;
            //var appVersion = string.Format(CultureInfo.CurrentCulture, "{0}.{1}", version.Major, version.Minor);
        }

        /// <summary>
        /// Called when [dispatcher unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            DependencyInjection.Logger.Error(e.Exception);
            ShowDialog(e.Exception);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                // Check if app is already running
                bool createdNew;
                var unused = new Mutex(true, Constants.App.Title, out createdNew);

                if (!createdNew)
                    Environment.Exit(0);

                // Process command line arguments
                Enums.Memory.Area memoryAreas = Enums.Memory.Area.None;

                foreach (string arg in e.Args)
                {
                    string value = arg.Replace("/", string.Empty).Replace("-", string.Empty);

                    // Memory areas to clean
                    Enums.Memory.Area area;

                    if (Enum.TryParse(value, out area))
                        memoryAreas |= area;
                }

                // GUI
                if (memoryAreas == Enums.Memory.Area.None)
                {
                    new MainWindow().Show();
                }
                else // NO GUI
                {
                    DependencyInjection.ComputerService.MemoryClean(memoryAreas);
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                ShowDialog(ex);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Called when [unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            DependencyInjection.Logger.Error((Exception)e.ExceptionObject);
            ShowDialog((Exception)e.ExceptionObject);
        }

        /// <summary>
        /// Shows the dialog
        /// </summary>
        /// <param name="exception">Exception</param>
        private void ShowDialog(Exception exception)
        {
            try
            {
                if (exception.InnerException != null)
                    ShowDialog(exception.InnerException);

                MessageBox.Show(exception.Message, Constants.App.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                // ignored
            }
        }

        #endregion
    }
}
