using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Windows Memory Cleaner Service Installer
    /// Rewritten for .NET 8 using sc.exe instead of System.Configuration.Install
    /// </summary>
    public static class WinServiceInstaller
    {
        private const string ServiceName = Constants.App.Name;
        private const string DisplayName = Constants.App.Title;
        private const string Description = "https://github.com/IgorMundstein/WinMemoryCleaner";

        /// <summary>
        /// Installs the service
        /// </summary>
        public static void Install()
        {
            Uninstall();

            // Recommended settings for service mode
            Settings.AutoOptimizationInterval = Settings.AutoOptimizationInterval == 0 ? 24 : Settings.AutoOptimizationInterval;
            Settings.AutoOptimizationMemoryUsage = Settings.AutoOptimizationMemoryUsage == 0 ? 10 : Settings.AutoOptimizationMemoryUsage;
            Settings.MemoryAreas = Enums.Memory.Areas.CombinedPageList | Enums.Memory.Areas.ModifiedFileCache | Enums.Memory.Areas.ModifiedPageList | Enums.Memory.Areas.RegistryCache | Enums.Memory.Areas.StandbyList | Enums.Memory.Areas.SystemFileCache | Enums.Memory.Areas.WorkingSet;
            Settings.RunOnPriority = Enums.Priority.Low;
            Settings.RunOnStartup = false;
            Settings.ShowOptimizationNotifications = false;
            Settings.Save();

            // Remove run on startup
            App.RunOnStartup(false);

            // Create service using sc.exe
            var assemblyPath = "\"" + Assembly.GetExecutingAssembly().Location + "\" /Service";
            var createArgs = $"create \"{ServiceName}\" binPath= {assemblyPath} DisplayName= \"{DisplayName}\" Description= \"{Description}\" start= auto type= own error= normal obj= LocalSystem";

            RunScCommand(createArgs);

            // Set delayed auto-start
            RunScCommand($"config \"{ServiceName}\" start= delayed-auto");

            // Start service after install
            using (var sc = new ServiceController(ServiceName))
                sc.Start();

            Logger.Information("Service installed and started: " + ServiceName);
        }

        /// <summary>
        /// Uninstalls the service
        /// </summary>
        public static void Uninstall()
        {
            if (IsInstalled())
            {
                // Stop service first
                try
                {
                    using (var sc = new ServiceController(ServiceName))
                    {
                        if (sc.Status != ServiceControllerStatus.Stopped && sc.Status != ServiceControllerStatus.StopPending)
                        {
                            sc.Stop();
                            sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warning("Failed to stop service before uninstall: " + ex.Message);
                }

                // Kill processes that block service uninstallation
                var processesToKill = new[] { "mmc", "procexp", "procexp64", "taskmgr" };
                var processes = Process.GetProcesses().Where(p => p != null && processesToKill.Contains(p.ProcessName, StringComparer.OrdinalIgnoreCase));

                foreach (var process in processes)
                {
                    try { process.Kill(); } catch (Exception ex) { Logger.Debug("Failed to kill process " + process.ProcessName + ": " + ex.Message); }
                }

                // Delete service using sc.exe
                RunScCommand($"delete \"{ServiceName}\"");

                // Kill any remaining app process
                processes = Process.GetProcessesByName(Constants.App.Name);
                foreach (var process in processes)
                {
                    try { process.Kill(); } catch (Exception ex) { Logger.Debug("Failed to kill process " + process.ProcessName + ": " + ex.Message); }
                }

                Logger.Information("Service uninstalled: " + ServiceName);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the service is installed.
        /// </summary>
        public static bool IsInstalled()
        {
            try
            {
                return ServiceController.GetServices().Any(sc => string.Equals(sc.ServiceName, ServiceName, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return false;
            }
        }

        private static void RunScCommand(string arguments)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "sc.exe",
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using (var process = Process.Start(startInfo))
            {
                if (process == null)
                    throw new InvalidOperationException("Failed to start sc.exe");

                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    var message = string.Format(Localizer.Culture, "sc.exe failed (exit code {0}). Args: {1}. Output: {2}. Error: {3}", process.ExitCode, arguments, output, error);
                    Logger.Error(message);
                    throw new InvalidOperationException(message);
                }
            }
        }
    }
}