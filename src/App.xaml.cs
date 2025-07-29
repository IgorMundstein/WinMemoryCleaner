using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Windows Memory Cleaner
    /// </summary>
    public partial class App : IDisposable
    {
        #region Fields

        private static bool _isRunning;
        private static DateTimeOffset _lastAutoUpdate;
        private static Mutex _mutex;
        private static NotifyIcon _notifyIcon;
        private static readonly List<ProcessStartInfo> _processes = new List<ProcessStartInfo>();
        private static readonly List<string> _notifications = new List<string>();
        private static readonly object _showHidelock = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="App" /> class.
        /// </summary>
        public App()
        {
            // Log
            Logger.Level = Debugger.IsAttached ? Enums.Log.Levels.Debug : Enums.Log.Levels.Information;

            // Events
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            // DI/IOC
            DependencyInjection.Container.Register<IComputerService, ComputerService>();
            DependencyInjection.Container.Register<IHotkeyService, HotkeyService>();
            DependencyInjection.Container.Register<INotificationService, NotificationService>();

            // Check if app is already running
            bool createdNew;

            _mutex = new Mutex(true, Constants.App.Id, out createdNew);
            _isRunning = !createdNew;

            // App properties
            Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName);
            Version = Assembly.GetExecutingAssembly().GetName().Version;

            // App Migration
            if (!_isRunning)
                Migrator.Run();

            // App priority
            SetPriority(Settings.RunOnPriority);

            // Brushes
            Brushes = typeof(System.Drawing.Color)
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(property => property.PropertyType == typeof(System.Drawing.Color))
                .Select(property => (System.Drawing.Color)property.GetValue(null, null))
                .Where(color => color.A == 255)
                .OrderBy(color => color.GetHue())
                .ThenBy(color => color.GetSaturation())
                .ThenBy(color => color.GetBrightness())
                .Select(color => new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B)))
                .ToList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the brushes.
        /// </summary>
        /// <value>
        /// The brushes.
        /// </value>
        public static List<SolidColorBrush> Brushes { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is in design mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in design mode; otherwise, <c>false</c>.
        /// </value>
        public static bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }

        /// <summary>
        /// App path
        /// </summary>
        public static string Path { get; private set; }

        /// <summary>
        /// App version
        /// </summary>
        public static Version Version { get; private set; }

        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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

            foreach (var process in _processes)
            {
                try
                {
                    Process.Start(process);
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Called when [notify icon click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnNotifyIconClick(object sender, EventArgs e)
        {
            lock (_showHidelock)
            {
                var mouseEventArgs = e as MouseEventArgs;

                // Show/Hide
                if (mouseEventArgs != null && mouseEventArgs.Button == MouseButtons.Left && MainWindow != null)
                {
                    if (MainWindow.OwnedWindows.Cast<View>().Where(window => window != null && window.IsDialog).Any())
                    {
                        MainWindow.Activate();
                        MainWindow.Focus();

                        return;
                    }

                    switch (MainWindow.Visibility)
                    {
                        case Visibility.Collapsed:
                        case Visibility.Hidden:
                            MainWindow.Show();

                            MainWindow.WindowState = WindowState.Normal;

                            MainWindow.Activate();
                            MainWindow.Focus();

                            MainWindow.Topmost = true;
                            MainWindow.Topmost = Settings.AlwaysOnTop;
                            MainWindow.ShowInTaskbar = true;
                            break;

                        case Visibility.Visible:
                            MainWindow.Hide();

                            MainWindow.ShowInTaskbar = false;
                            break;
                    }

                    ReleaseMemory();
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:Startup" /> event.
        /// </summary>
        /// <param name="startupEvent">The <see cref="StartupEventArgs" /> instance containing the event data.</param>
        protected override void OnStartup(StartupEventArgs startupEvent)
        {
            try
            {
                var commandLineArguments = startupEvent != null ? new List<string>(startupEvent.Args.Select(arg => arg.Replace("-", "/").Trim())) : null;
                var memoryAreas = Enums.Memory.Areas.None;
                var startupType = Enums.StartupType.App;

                if (commandLineArguments != null)
                {
                    // Update to the latest version
                    if (Settings.AutoUpdate)
                        Update(commandLineArguments);

                    // Process command‑line arguments
                    foreach (var argument in commandLineArguments.Select(arg => arg.Replace("/", string.Empty)))
                    {
                        // Memory areas to optimize
                        Enums.Memory.Areas area;

                        if (Enum.TryParse(argument, out area))
                            memoryAreas |= area;

                        // Version (Update)
                        if (argument.Equals(Version.ToString()))
                            _notifications.Add(string.Format(Localizer.Culture, Localizer.String.UpdatedToVersion, string.Format(Localizer.Culture, Constants.App.VersionFormat, Version.Major, Version.Minor, Version.Build)));

                        // Startup Type
                        if (memoryAreas != Enums.Memory.Areas.None)
                            startupType = Enums.StartupType.Silent;

                        if (argument.Equals(Constants.App.CommandLineArgument.Install, StringComparison.OrdinalIgnoreCase))
                            startupType = Enums.StartupType.Installation;

                        if (argument.Equals(Constants.App.CommandLineArgument.Package, StringComparison.OrdinalIgnoreCase))
                            startupType = Enums.StartupType.Package;

                        if (argument.Equals(Constants.App.CommandLineArgument.Service, StringComparison.OrdinalIgnoreCase))
                            startupType = Enums.StartupType.Service;

                        if (argument.Equals(Constants.App.CommandLineArgument.Uninstall, StringComparison.OrdinalIgnoreCase))
                            startupType = Enums.StartupType.Uninstallation;
                    }

                    // Update app path setting
                    if (startupType != Enums.StartupType.Package)
                        Settings.Path = Path;
                }

                switch (startupType)
                {
                    case Enums.StartupType.App:
                        if (_isRunning)
                        {
                            try
                            {
                                var appHandle = NativeMethods.FindWindow(null, Constants.App.Title);

                                if (appHandle != IntPtr.Zero && NativeMethods.IsWindowVisible(appHandle))
                                {
                                    int appId;

                                    if (NativeMethods.GetWindowThreadProcessId(appHandle, out appId) != Constants.Windows.SystemErrorCode.ErrorSuccess)
                                        NativeMethods.AllowSetForegroundWindow(appId);

                                    NativeMethods.ShowWindowAsync(appHandle, Constants.Windows.ShowWindow.Restore);
                                    NativeMethods.SetForegroundWindow(appHandle);
                                }
                            }
                            finally
                            {
                                Shutdown(true);
                            }
                        }

                        NativeMethods.AllowSetForegroundWindow(Process.GetCurrentProcess().Id);

                        // Run On Startup
                        RunOnStartup(Settings.RunOnStartup);

                        // Notification Areas
                        _notifyIcon = new NotifyIcon();
                        _notifyIcon.Click += OnNotifyIconClick;

                        // DI/IOC
                        DependencyInjection.Container.Register(_notifyIcon);

                        var mainWindow = new MainWindow();

                        if (!Settings.StartMinimized)
                            mainWindow.Show();

                        // Process notifications
                        foreach (var notification in _notifications)
                        {
                            Logger.Information(notification);
                            DependencyInjection.Container.Resolve<INotificationService>().Notify(notification);
                        }

                        ReleaseMemory();
                        break;

                    case Enums.StartupType.Installation:
                        AppServiceInstaller.Install();

                        Shutdown();
                        break;

                    case Enums.StartupType.Package:
                        var exe = AppDomain.CurrentDomain.FriendlyName;
                        var sourcePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exe);
                        var targetPath = File.Exists(Settings.Path) ? Settings.Path : System.IO.Path.Combine(Constants.App.Defaults.Path, exe);                        

                        try
                        {
                            foreach (var process in Process.GetProcessesByName(Constants.App.Name).Where(p => p != null && p.Id != Process.GetCurrentProcess().Id))
                            {
                                try
                                {
                                    targetPath = process.MainModule.FileName;
                                }
                                catch
                                {
                                    // ignored
                                }

                                try
                                {
                                    process.Kill();
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

                        commandLineArguments.Remove(string.Format(Localizer.Culture, "/{0}", Constants.App.CommandLineArgument.Package));

                        _processes.Add(new ProcessStartInfo
                        {
                            Arguments = string.Format(Localizer.Culture, @"/c taskkill /f /im ""{0}"" & move /y ""{1}"" ""{2}"" & start """" ""{2}"" /{3} {4}", exe, sourcePath, targetPath, Version, string.Join(" ", commandLineArguments)).Trim(),
                            CreateNoWindow = true,
                            FileName = "cmd",
                            RedirectStandardError = false,
                            RedirectStandardInput = false,
                            RedirectStandardOutput = false,
                            UseShellExecute = false,
                            WindowStyle = ProcessWindowStyle.Hidden
                        });

                        Shutdown();
                        break;

                    case Enums.StartupType.Service:
                        using (var service = new AppService())
                        {
                            if (Debugger.IsAttached)
                                service.OnDebug(null);
                            else
                                ServiceBase.Run(service);
                        }
                        break;

                    case Enums.StartupType.Silent:
                        DependencyInjection.Container.Resolve<IComputerService>().Optimize(Enums.Memory.Optimization.Reason.Manual, memoryAreas);

                        Shutdown();
                        break;

                    case Enums.StartupType.Uninstallation:
                        AppServiceInstaller.Uninstall();

                        Shutdown();
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                ShowDialog(e);

                Shutdown(true);
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
        /// Releases the app memory
        /// </summary>
        public static void ReleaseMemory()
        {
            // Garbage Collector
            try
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            }
            catch
            {
                // ignored
            }

            // Optimize App Working Set
            try
            {
                NativeMethods.EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Runs the app on startup
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        public static void RunOnStartup(bool enable)
        {
            try
            {
                using (Process.Start(new ProcessStartInfo
                {
                    Arguments = string.Format(Localizer.Culture, @"/DELETE /F /TN ""{0}""", Constants.App.Title),
                    CreateNoWindow = true,
                    FileName = "schtasks",
                    RedirectStandardError = false,
                    RedirectStandardInput = false,
                    RedirectStandardOutput = false,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                })) { }

                if (enable)
                {
                    using (Process.Start(new ProcessStartInfo
                    {
                        Arguments = string.Format(Localizer.Culture, @"/CREATE /F /IT /RL HIGHEST /RU ADMINISTRATORS /SC ONLOGON /TN ""{0}"" /TR """"""{1}""""""", Constants.App.Title, Path),
                        CreateNoWindow = true,
                        FileName = "schtasks",
                        RedirectStandardError = false,
                        RedirectStandardInput = false,
                        RedirectStandardOutput = false,
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden
                    })) { }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
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
                System.Windows.MessageBox.Show(exception.GetMessage(), Constants.App.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Shuts down the app
        /// </summary>
        /// <param name="force">if set to <c>true</c> [force].</param>
        public static void Shutdown(bool force = false)
        {
            try
            {
                if (force)
                    Environment.Exit(Constants.Windows.SystemErrorCode.ErrorSuccess);
                else
                    Current.Shutdown();
            }
            catch
            {
                Environment.Exit(Constants.Windows.SystemErrorCode.ErrorSuccess);
            }
        }

        /// <summary>
        /// Update to the latest version
        /// </summary>
        public static void Update(List<string> commandLineArguments = null)
        {
            try
            {
                if (commandLineArguments != null && commandLineArguments.Any(arg => arg.Replace("/", string.Empty).Equals(Constants.App.CommandLineArgument.Package, StringComparison.OrdinalIgnoreCase)))
                    return;

                if (DateTimeOffset.Now.Subtract(_lastAutoUpdate).TotalHours < Constants.App.AutoUpdateInterval)
                    return;

                _lastAutoUpdate = DateTimeOffset.Now;

                using (var client = new WebClient())
                {
                    ServicePointManager.DefaultConnectionLimit = 10;
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol |= (SecurityProtocolType)3072 | (SecurityProtocolType)12288; // TLS 1.2 | TLS 1.3

                    var assemblyInfo = client.DownloadString(Constants.App.Repository.AssemblyInfoUri);

                    var assemblyVersionMatch = Regex.Match(assemblyInfo, @"AssemblyVersion\(""(.*)""\)\]");

                    if (!assemblyVersionMatch.Success)
                        return;

                    var newestVersion = Version.Parse(assemblyVersionMatch.Groups[1].Value);

                    if (Version < newestVersion)
                    {
                        var exe = AppDomain.CurrentDomain.FriendlyName;
                        var sourcePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), exe);
                        var targetPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exe);

                        if (File.Exists(sourcePath))
                            File.Delete(sourcePath);

                        client.DownloadFile(Constants.App.Repository.LatestExeUri, sourcePath);

                        if (File.Exists(sourcePath) && AssemblyName.GetAssemblyName(sourcePath).Version.Equals(newestVersion))
                        {
                            _processes.Add(new ProcessStartInfo
                            {
                                Arguments = string.Format(Localizer.Culture, @"/c taskkill /f /im ""{0}"" & move /y ""{1}"" ""{2}"" & start """" ""{2}"" /{3} {4}", exe, sourcePath, targetPath, newestVersion, string.Join(" ", commandLineArguments)).Trim(),
                                CreateNoWindow = true,
                                FileName = "cmd",
                                RedirectStandardError = false,
                                RedirectStandardInput = false,
                                RedirectStandardOutput = false,
                                UseShellExecute = false,
                                WindowStyle = ProcessWindowStyle.Hidden
                            });

                            Shutdown();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Warning(string.Format(Localizer.Culture, "({0}) {1}", Localizer.String.AutoUpdate.ToUpper(Localizer.Culture), e.GetMessage()));
            }
        }

        #endregion
    }
}
