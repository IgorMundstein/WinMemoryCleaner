using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
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

        private static DateTimeOffset _lastUpdateCheck;
        private static Mutex _mutex;
        private static NotifyIcon _notifyIcon;
        private static ProcessStartInfo _updateProcess;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="App" /> class.
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
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            // DI/IOC
            DependencyInjection.Container.Register<IComputerService, ComputerService>();
            DependencyInjection.Container.Register<INotificationService, NotificationService>();
        }

        #endregion

        #region Properties

        internal static Version Version { get { return Assembly.GetExecutingAssembly().GetName().Version; } }

        #endregion

        #region IDisposable

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
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
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when [dispatcher unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs" /> instance containing the event data.</param>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            Logger.Error(e.Exception);
            ShowDialog(e.Exception);
        }

        /// <summary>
        /// Called when [process exit].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnProcessExit(object sender, EventArgs e)
        {
            Dispose();

            try
            {
                if (_updateProcess != null)
                    Process.Start(_updateProcess);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Called when [notify icon click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnNotifyIconClick(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = e as MouseEventArgs;

            if (mouseEventArgs != null && mouseEventArgs.Button == MouseButtons.Left)
                OnNotifyMenuShowHideClick();
        }

        /// <summary>
        /// Called when [notify menu show hide click].
        /// </summary>
        private void OnNotifyMenuShowHideClick()
        {
            if (MainWindow != null)
            {
                switch (MainWindow.WindowState)
                {
                    case WindowState.Maximized:
                    case WindowState.Normal:
                        MainWindow.ShowInTaskbar = false;
                        MainWindow.WindowState = WindowState.Minimized;
                        break;

                    case WindowState.Minimized:
                        MainWindow.ShowInTaskbar = true;
                        MainWindow.WindowState = WindowState.Normal;
                        MainWindow.Activate();
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

                // Update to the latest version
                if (Settings.AutoUpdate)
                    Update(e.Args);

                // Process command line arguments
                Enums.Memory.Area memoryAreas = Enums.Memory.Area.None;
                string updateNotification = null;

                foreach (string arg in e.Args)
                {
                    string value = arg.Replace("/", string.Empty).Replace("-", string.Empty);

                    // Memory areas to clean
                    Enums.Memory.Area area;

                    if (Enum.TryParse(value, out area))
                        memoryAreas |= area;

                    // Version (Update)
                    if (value.Equals(Version.ToString()))
                    {
                        updateNotification = string.Format(CultureInfo.CurrentCulture, Localization.UpdatedToVersion, string.Format("{0}.{1}", Version.Major, Version.Minor));

                        Logger.Information(updateNotification);
                    }
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
                    
                    // Optimize
                    _notifyIcon.ContextMenuStrip.Items.Add(Localization.Optimize, null, (sender, args) =>
                    {
                        var mainViewModel = DependencyInjection.Container.Resolve<MainViewModel>();

                        if (mainViewModel.MemoryCleanCommand.CanExecute(null))
                            mainViewModel.MemoryCleanCommand.Execute(null);
                    });

                    _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());

                    // Exit
                    _notifyIcon.ContextMenuStrip.Items.Add(Localization.Exit, null, (sender, args) =>
                    {
                        Shutdown();
                    });

                    // DI/IOC
                    DependencyInjection.Container.Register(_notifyIcon);

                    // Update notification
                    if (!string.IsNullOrWhiteSpace(updateNotification))
                        DependencyInjection.Container.Resolve<NotificationService>().Notify(updateNotification);

                    new MainWindow().Show();
                }
                else // NO GUI
                {
                    DependencyInjection.Container.Resolve<ComputerService>().CleanMemory(memoryAreas);

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
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs" /> instance containing the event data.</param>
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

        /// <summary>
        /// Update to the latest version
        /// </summary>
        public static void Update(params string[] args)
        {
            try
            {
                if (DateTimeOffset.Now.Subtract(_lastUpdateCheck).TotalHours < Constants.App.UpdateInterval)
                    return;

                _lastUpdateCheck = DateTimeOffset.Now;

                using (WebClient client = new WebClient())
                {
                    ServicePointManager.DefaultConnectionLimit = 10;
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)3072 | (SecurityProtocolType)768 | SecurityProtocolType.Tls;

                    var assemblyInfo = client.DownloadString(Constants.App.Repository.AssemblyInfoUri);

                    var assemblyVersionMatch = Regex.Match(assemblyInfo, @"AssemblyVersion\(""(.*)""\)\]");

                    if (!assemblyVersionMatch.Success)
                        return;

                    var newestVersion = Version.Parse(assemblyVersionMatch.Groups[1].Value);

                    if (Version < newestVersion)
                    {
                        var exe = AppDomain.CurrentDomain.FriendlyName;
                        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exe);
                        var temp = Path.Combine(Path.GetTempPath(), exe);

                        if (File.Exists(temp))
                            File.Delete(temp);

                        client.DownloadFile(Constants.App.Repository.LatestExeUri, temp);

                        _updateProcess = new ProcessStartInfo
                        {
                            FileName = "cmd",
                            Arguments = string.Format(@"/c taskkill /f /im ""{0}"" & move /y ""{1}"" ""{2}"" & start """" ""{2}"" /{3} {4}", exe, temp, path, newestVersion, string.Join(" ", args)),
                            WindowStyle = ProcessWindowStyle.Hidden
                        };

                        Environment.Exit(0);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        #endregion
    }
}
