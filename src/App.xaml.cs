using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

[assembly: CLSCompliant(true)]
namespace WinMemoryCleaner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : IDisposable
    {
        #region Fields

        private static Mutex _mutex;
        private static NotifyIcon _notifyIcon;
        private static ToolStripItem _notifyMenuShowHide;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        internal App()
        {
            // Log
#if DEBUG
            Logger.Level = Enums.Log.Level.Debug;
#else
            Logger.Level = Enums.Log.Level.Information;
#endif

            // Events
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        #endregion

        #region Properties

        internal static IComputerService ComputerService { get; private set; }

        internal static INotificationService NotificationService { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks for updates.
        /// </summary>
        public void CheckForUpdates()
        {
            //TODO: Implement auto-update
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_mutex != null)
            {
                try
                {
                    _mutex.ReleaseMutex();
                }
                catch
                {
                    // ignored
                }

                try
                {
                    _mutex.Dispose();
                }
                catch
                {
                    // ignored
                }

                _mutex = null;
            }

            try
            {
                if (_notifyIcon != null)
                    _notifyIcon.Dispose();
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Called when [dispatcher unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            Logger.Error(e.Exception);
            ShowDialog(e.Exception);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Exit" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.ExitEventArgs" /> that contains the event data.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            Dispose();
        }

        /// <summary>
        /// Called when [notify icon click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnNotifyIconClick(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;

            if (mouseEventArgs != null && mouseEventArgs.Button == MouseButtons.Left)
                OnNotifyMenuShowHideClick(sender, e);
        }

        /// <summary>
        /// Called when [notify menu show hide click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnNotifyMenuShowHideClick(object sender, EventArgs e)
        {
            if (MainWindow != null)
            {
                switch (MainWindow.WindowState)
                {
                    case WindowState.Maximized:
                    case WindowState.Normal:
                        MainWindow.ShowInTaskbar = false;
                        MainWindow.WindowState = WindowState.Minimized;

                        if (_notifyMenuShowHide != null)
                            _notifyMenuShowHide.Text = Localization.NotifyMenuShow;
                        break;

                    case WindowState.Minimized:
                        MainWindow.ShowInTaskbar = true;
                        MainWindow.WindowState = WindowState.Normal;
                        MainWindow.Activate();

                        if (_notifyMenuShowHide != null)
                            _notifyMenuShowHide.Text = Localization.NotifyMenuHide;
                        break;
                }
            }
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

                _mutex = new Mutex(true, Constants.App.Guid, out createdNew);

                if (!createdNew)
                {
                    try
                    {
                        var appHandle = NativeMethods.FindWindow(null, Constants.App.Title);

                        if (appHandle != IntPtr.Zero)
                        {
                            NativeMethods.ShowWindowAsync(appHandle, Constants.Windows.ShowWindow.Restore);
                            NativeMethods.SetForegroundWindow(appHandle);
                        }

                        _mutex.Dispose();
                    }
                    finally
                    {
                        Environment.Exit(0);
                    }
                }

                GC.KeepAlive(_mutex);

                // Services
                ComputerService = new ComputerService();

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
                    // Notification Area
                    _notifyIcon = new NotifyIcon();
                    _notifyIcon.Click += OnNotifyIconClick;
                    _notifyIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
                    _notifyIcon.Text = string.Format("{0}{1}{2}", "Windows", Environment.NewLine, "Memory Cleaner");
                    _notifyIcon.Visible = true;

                    // Notification Area (Menu)
                    _notifyIcon.ContextMenuStrip = new ContextMenuStrip();

                    _notifyIcon.ContextMenuStrip.Items.Add(Localization.NotifyMenuHide, null, OnNotifyMenuShowHideClick);
                    _notifyMenuShowHide = _notifyIcon.ContextMenuStrip.Items[0];

                    _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());

                    _notifyIcon.ContextMenuStrip.Items.Add(Localization.NotifyMenuExit, null, (sender, args) => { Shutdown(); });

                    // Services
                    NotificationService = new NotificationService(_notifyIcon);

                    new MainWindow().Show();
                }
                else // NO GUI
                {
                    ComputerService.MemoryClean(memoryAreas);

                    Shutdown();
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
            Logger.Error((Exception)e.ExceptionObject);

            ShowDialog((Exception)e.ExceptionObject);
        }

        /// <summary>
        /// Shows a dialog
        /// </summary>
        /// <param name="exception">Exception</param>
        private void ShowDialog(Exception exception)
        {
            try
            {
                System.Windows.MessageBox.Show(exception.GetBaseException().Message, Constants.App.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                // ignored
            }
        }

        #endregion
    }
}
