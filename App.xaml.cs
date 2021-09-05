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
        public App()
        {
            // Events
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            // Log
#if DEBUG
            LogHelper.Level = Enums.Log.Level.Debug;
#else
            LogHelper.Level = Enums.Log.Level.Info;
#endif

            // Culture
            CultureInfo englishUnitedStates = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentCulture = englishUnitedStates;
            Thread.CurrentThread.CurrentUICulture = englishUnitedStates;

            // It could be replaced by a dependency injection library
            LoadingService = new LoadingService();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Loading Service
        /// </summary>
        public static ILoadingService LoadingService { get; private set; }

        /// <summary>
        /// App Title
        /// </summary>
        public static string Title
        {
            get
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;

                return string.Format(CultureInfo.CurrentCulture, "{0} {1}.{2}", Constants.App.Title, version.Major, version.Minor);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when [dispatcher unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            LogHelper.Error(e.Exception);
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
            }
            catch
            {
                // ignored
            }
            finally
            {
                base.OnStartup(e);
            }
        }

        /// <summary>
        /// Called when [unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogHelper.Error((Exception)e.ExceptionObject);
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

                MessageBox.Show(exception.Message, App.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                // ignored
            }
        }

        #endregion
    }
}
