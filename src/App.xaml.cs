using Microsoft.Win32;
using System;
using System.Diagnostics;
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

        private static DateTimeOffset _lastAutoUpdate;
        private static Mutex _mutex;
        private static NotifyIcon _notifyIcon;
        private static ProcessStartInfo _updateProcess;
        private static Version _version;

        #endregion

        #region Constructors

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
                    {
                        _notifyIcon.Dispose();
                    }
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

            // Show/Hide
            if (mouseEventArgs != null && mouseEventArgs.Button == MouseButtons.Left && MainWindow != null)
            {
                switch (MainWindow.Visibility)
                {
                    case Visibility.Collapsed:
                    case Visibility.Hidden:
                        MainWindow.ShowInTaskbar = true;
                        MainWindow.WindowState = WindowState.Normal;

                        MainWindow.Show();
                        MainWindow.Activate();
                        break;

                    case Visibility.Visible:
                        MainWindow.ShowInTaskbar = false;
                        MainWindow.WindowState = WindowState.Normal;

                        MainWindow.Hide();
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

                        if (appHandle != IntPtr.Zero && NativeMethods.IsWindowVisible(appHandle))
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

                // App priority
                SetPriority(Enums.Priority.Low);

                // App Version
                _version = Assembly.GetExecutingAssembly().GetName().Version;

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
                    if (value.Equals(_version.ToString()))
                    {
                        updateNotification = string.Format(Localizer.String.UpdatedToVersion, string.Format("{0}.{1}", _version.Major, _version.Minor));

                        Logger.Information(updateNotification);
                    }
                }

                // GUI
                if (memoryAreas == Enums.Memory.Area.None)
                {
                    // Run On Startup
                    RunOnStartup(Settings.RunOnStartup);

                    // Notification Area
                    _notifyIcon = new NotifyIcon();
                    _notifyIcon.Click += OnNotifyIconClick;
                    _notifyIcon.DoubleClick += OnNotifyIconClick;

                    // DI/IOC
                    DependencyInjection.Container.Register(_notifyIcon);

                    // Update notification
                    if (!string.IsNullOrWhiteSpace(updateNotification))
                        DependencyInjection.Container.Resolve<NotificationService>().Notify(updateNotification);

                    MainWindow mainWindow = new MainWindow();

                    if (Settings.StartMinimized)
                    {
                        mainWindow.ShowInTaskbar = false;
                        mainWindow.Hide();
                    }
                    else
                        mainWindow.Show();

                    // Reduce app memory usage
                    NativeMethods.EmptyWorkingSet(Process.GetCurrentProcess().Handle);
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
        /// Runs the app on startup
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        internal static void RunOnStartup(bool enable)
        {
            try
            {
                var startupPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName);

                // Registry
                try
                {
                    using (var key = Registry.LocalMachine.CreateSubKey(Constants.App.Registry.Key.Startup))
                    {
                        if (key != null)
                        {
                            if (enable)
                                key.SetValue(Constants.App.Title, string.Format(@"""{0}""", startupPath));
                            else
                                key.DeleteValue(Constants.App.Title, false);
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Debug(e.GetBaseException().Message);
                }

                // Scheduled Task
                try
                {
                    var arguments = enable
                        ? string.Format(@"/CREATE /F /RL HIGHEST /SC ONLOGON /TN ""{0}"" /TR """"""{1}""""""", Constants.App.Title, startupPath)
                        : string.Format(@"/DELETE /F /TN ""{0}""", Constants.App.Title);

                    new Process
                    {
                        StartInfo =
                        {
                            Arguments = arguments,
                            CreateNoWindow = true,
                            FileName = "schtasks",
                            RedirectStandardError = false,
                            RedirectStandardInput = false,
                            RedirectStandardOutput = false,
                            UseShellExecute = false,
                            WindowStyle = ProcessWindowStyle.Hidden
                        }
                    }.Start();
                }
                catch (Exception e)
                {
                    Logger.Debug(e.GetBaseException().Message);
                }
            }
            catch (Exception e)
            {
                Logger.Debug(e.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Sets the app priority for the Windows
        /// </summary>
        public static void SetPriority(Enums.Priority priority)
        {
            bool priorityBoostEnabled;
            ProcessPriorityClass processPriorityClass;
            ThreadPriority threadPriority;
            ThreadPriorityLevel threadPriorityLevel;

            switch (priority)
            {
                case Enums.Priority.Low:
                    priorityBoostEnabled = false;
                    processPriorityClass = ProcessPriorityClass.Idle;
                    threadPriority = ThreadPriority.Lowest;
                    threadPriorityLevel = ThreadPriorityLevel.Idle;
                    break;

                case Enums.Priority.Normal:
                    priorityBoostEnabled = true;
                    processPriorityClass = ProcessPriorityClass.Normal;
                    threadPriority = ThreadPriority.Normal;
                    threadPriorityLevel = ThreadPriorityLevel.Normal;
                    break;

                case Enums.Priority.High:
                    priorityBoostEnabled = true;
                    processPriorityClass = ProcessPriorityClass.High;
                    threadPriority = ThreadPriority.Highest;
                    threadPriorityLevel = ThreadPriorityLevel.Highest;
                    break;

                default:
                    throw new NotImplementedException();
            }

            try
            {
                Thread.CurrentThread.Priority = threadPriority;
            }
            catch
            {
                // ignored
            }

            try
            {
                var process = Process.GetCurrentProcess();

                try
                {
                    process.PriorityBoostEnabled = priorityBoostEnabled;
                }
                catch
                {
                    // ignored
                }

                try
                {
                    process.PriorityClass = processPriorityClass;
                }
                catch
                {
                    // ignored
                }

                foreach (ProcessThread thread in process.Threads)
                {
                    try
                    {
                        thread.PriorityBoostEnabled = priorityBoostEnabled;
                    }
                    catch
                    {
                        // ignored
                    }

                    try
                    {
                        thread.PriorityLevel = threadPriorityLevel;
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            catch
            {
                // ignored
            }
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
        internal static void Update(params string[] args)
        {
            try
            {
                if (DateTimeOffset.Now.Subtract(_lastAutoUpdate).TotalHours < Constants.App.AutoUpdateInterval)
                    return;

                _lastAutoUpdate = DateTimeOffset.Now;

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

                    if (_version < newestVersion)
                    {
                        var exe = AppDomain.CurrentDomain.FriendlyName;
                        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exe);
                        var temp = Path.Combine(Path.GetTempPath(), exe);

                        if (File.Exists(temp))
                            File.Delete(temp);

                        client.DownloadFile(Constants.App.Repository.LatestExeUri, temp);

                        _updateProcess = new ProcessStartInfo
                        {
                            Arguments = string.Format(@"/c taskkill /f /im ""{0}"" & move /y ""{1}"" ""{2}"" & start """" ""{2}"" /{3} {4}", exe, temp, path, newestVersion, string.Join(" ", args)),
                            CreateNoWindow = true,
                            FileName = "cmd",
                            RedirectStandardError = false,
                            RedirectStandardInput = false,
                            RedirectStandardOutput = false,
                            UseShellExecute = false,
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
