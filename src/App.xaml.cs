using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
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
            Logger.Level = IsInDebugMode ? Enums.Log.Levels.Debug : Enums.Log.Levels.Information;

            // Events
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is in debug mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in debug mode; otherwise, <c>false</c>.
        /// </value>
        public static bool IsInDebugMode
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

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

        private void Initialize()
        {
            // Check if the app is secure to run
            SecurityCheck();

            // DI/IOC
            DependencyInjection.Container.Register<IComputerService, ComputerService>();
            DependencyInjection.Container.Register<IHotkeyService, HotkeyService>();
            DependencyInjection.Container.Register<INotificationService, NotificationService>();

            // App properties
            Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName);
            Version = Assembly.GetExecutingAssembly().GetName().Version;

            // Check if app is already running
            bool createdNew;

            _mutex = new Mutex(true, Constants.App.Id, out createdNew);
            _isRunning = !createdNew;

            // App Migration
            if (!_isRunning)
                Migrator.Run();

            // App priority
            SetPriority(Settings.RunOnPriority);
        }

        /// <summary>
        /// Navigates the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public static void Navigate(Uri uri)
        {
            if (uri == null)
                return;

            using (Process.Start(new ProcessStartInfo
            {
                FileName = uri.AbsoluteUri,
                UseShellExecute = true
            })) { }
        }

        /// <summary>
        /// Called when [dispatcher unhandled exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs" /> instance containing the event data.</param>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            Logger.Error(e.Exception);
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
                Initialize();

                var commandLineArguments = startupEvent != null ? new List<string>(startupEvent.Args.Select(arg => arg.Replace("-", "/").Trim())) : null;
                var memoryAreas = Enums.Memory.Areas.None;
                var startupType = Enums.StartupType.App;

                if (commandLineArguments != null)
                {
                    // Update to the latest version
                    Updater.Update(commandLineArguments);

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
                }

                // Update the last executable path when neither installing nor updating via a package manager
                if (startupType != Enums.StartupType.Package)
                {
                    Settings.Path = Path;
                    Settings.Save();
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

                        // Theme
                        ThemeManager.Theme = Enums.Theme.Dark;

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

                        RunOnStartup(Settings.RunOnStartup);
                        ReleaseMemory();
                        break;

                    case Enums.StartupType.Installation:
                        WinServiceInstaller.Install();

                        Shutdown();
                        break;

                    case Enums.StartupType.Package:
                        commandLineArguments.Remove(string.Format(Localizer.Culture, "/{0}", Constants.App.CommandLineArgument.Package));

                        var exe = AppDomain.CurrentDomain.FriendlyName;
                        var isUpdate = false;
                        var sourcePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exe);
                        var targetPath = sourcePath;
                        var wasRunning = false;

                        try
                        {
                            if (Directory.Exists(System.IO.Path.GetDirectoryName(Settings.Path)))
                                targetPath = Settings.Path;
                        }
                        catch
                        {
                            // Ignored
                        }

                        try
                        {
                            var runningProcesses = Process.GetProcessesByName(Constants.App.Name)
                                .Where(p => p != null && p.Id != Process.GetCurrentProcess().Id)
                                .ToList();

                            wasRunning = runningProcesses.Any();

                            foreach (var process in runningProcesses)
                            {
                                try
                                {
                                    var processPath = process.MainModule.FileName;

                                    if (!string.IsNullOrEmpty(processPath) && File.Exists(processPath))
                                        targetPath = processPath;
                                }
                                catch
                                {
                                    // Ignored
                                }

                                try
                                {
                                    process.Kill();
                                    process.WaitForExit();
                                }
                                catch
                                {
                                    // Ignored
                                }
                                finally
                                {
                                    process.Dispose();
                                }
                            }
                        }
                        catch
                        {
                            // Ignored
                        }

                        try
                        {
                            if (File.Exists(targetPath))
                            {
                                var currentVersion = FileVersionInfo.GetVersionInfo(sourcePath);
                                var targetVersion = FileVersionInfo.GetVersionInfo(targetPath);

                                isUpdate = currentVersion.FileVersion != targetVersion.FileVersion;
                            }
                        }
                        catch
                        {
                            // Ignored
                        }

                        try
                        {
                            var targetDirectory = System.IO.Path.GetDirectoryName(targetPath);

                            if (!string.IsNullOrEmpty(targetDirectory) && !Directory.Exists(targetDirectory))
                                Directory.CreateDirectory(targetDirectory);
                        }
                        catch
                        {
                            // Ignored
                        }

                        var restart = !isUpdate || wasRunning;

                        _processes.Add(new ProcessStartInfo
                        {
                            Arguments = string.Format
                            (
                                CultureInfo.InvariantCulture,
                                @"/c move ""{0}"" ""{1}"" >nul 2>&1 & if {2} equ 1 if exist ""{1}"" start """" ""{1}"" {3}",
                                sourcePath,
                                targetPath,
                                restart ? "1" : "0",
                                string.Join(" ", commandLineArguments.Concat(new[] { isUpdate ? string.Format(CultureInfo.InvariantCulture, "/{0}", Version) : string.Empty }).Where(arg => !string.IsNullOrEmpty(arg)))
                            ).Trim(),
                            CreateNoWindow = true,
                            FileName = "cmd",
                            UseShellExecute = false,
                            WindowStyle = ProcessWindowStyle.Hidden
                        });

                        Shutdown();
                        break;

                    case Enums.StartupType.Service:
                        using (var service = new WinService())
                        {
                            if (IsInDebugMode)
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
                        WinServiceInstaller.Uninstall();

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
                if (enable)
                {
                    var runLevelArgument = Environment.OSVersion.Version.Major >= 6 ? " /RL HIGHEST" : string.Empty;

                    var createStartInfo = new ProcessStartInfo("schtasks")
                    {
                        Arguments = string.Format(Localizer.Culture, @"/CREATE /F /IT{0} /SC ONLOGON /TN ""{1}"" /TR ""{2}"" /RU ""{3}""", runLevelArgument, Constants.App.Title, Path, Environment.UserName),
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardError = true
                    };

                    using (var createProcess = Process.Start(createStartInfo))
                    {
                        var errorMessage = createProcess.StandardError.ReadToEnd();
                        createProcess.WaitForExit();

                        if (createProcess.ExitCode != Constants.Windows.SystemErrorCode.ErrorSuccess)
                            Logger.Error(string.Format(Localizer.Culture, "Failed to create startup task for '{0}'. Error: {1}", Constants.App.Title, errorMessage));
                    }
                }
                else
                {
                    var queryStartInfo = new ProcessStartInfo("schtasks")
                    {
                        Arguments = string.Format(Localizer.Culture, @"/QUERY /TN ""{0}""", Constants.App.Title),
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true
                    };

                    using (var queryProcess = Process.Start(queryStartInfo))
                    {
                        queryProcess.WaitForExit();

                        if (queryProcess.ExitCode == Constants.Windows.SystemErrorCode.ErrorSuccess)
                        {
                            var deleteStartInfo = new ProcessStartInfo("schtasks")
                            {
                                Arguments = string.Format(Localizer.Culture, @"/DELETE /F /TN ""{0}""", Constants.App.Title),
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                WindowStyle = ProcessWindowStyle.Hidden
                            };

                            using (var deleteProcess = Process.Start(deleteStartInfo))
                            {
                                deleteProcess.WaitForExit();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format(Localizer.Culture, "An error occurred while managing the scheduled task for app startup. Error: {0}", e.GetMessage()));
            }
        }

        /// <summary>
        /// Verify if the app is secure to run
        /// </summary>
        private void SecurityCheck()
        {
            if (IsInDebugMode)
                return;

            if (!Validator.IsCertificateValid())
            {
                try
                {
                    ShowDialog(Localizer.String.SecurityWarning, MessageBoxButton.OK, MessageBoxImage.Warning);
                    Navigate(Constants.App.Repository.DownloadUri);
                }
                finally
                {
                    Shutdown(true);
                }
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
            ShowDialog(exception.GetMessage(), MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="button">The button.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultResult">The default result.</param>
        /// <param name="options">The options.</param>
        private void ShowDialog(string message, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.OK, System.Windows.MessageBoxOptions options = System.Windows.MessageBoxOptions.None)
        {
            try
            {
                System.Windows.MessageBox.Show(message, Constants.App.Title, button, icon, defaultResult, options);
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

        #endregion
    }
}
