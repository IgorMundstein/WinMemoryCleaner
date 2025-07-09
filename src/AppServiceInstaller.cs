using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Windows Memory Cleaner Service Installer
    /// </summary>
    [RunInstaller(true)]
    public class AppServiceInstaller : Installer
    {
        private readonly ServiceInstaller _serviceInstaller;
        private readonly ServiceProcessInstaller _processInstaller;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceInstaller" /> class.
        /// </summary>
        public AppServiceInstaller()
        {
            _processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            _serviceInstaller = new ServiceInstaller
            {
                DelayedAutoStart = true,
                Description = Constants.App.Repository.Uri.ToString(),
                DisplayName = Constants.App.Title,
                ServiceName = Constants.App.Name,
                StartType = ServiceStartMode.Automatic
            };

            Installers.Add(_processInstaller);
            Installers.Add(_serviceInstaller);
        }

        /// <summary>
        /// Installs the service
        /// </summary>
        public static void Install()
        {
            Uninstall();

            ManagedInstallerClass.InstallHelper(new[]
            {
                "/LogFile=" + Constants.App.Name + ".log",
                "/LogToConsole=true",
                Assembly.GetExecutingAssembly().Location
            });
        }

        /// <summary>
        /// Raises the <see cref="E:System.Configuration.Install.Installer.AfterInstall" /> event.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after all the installers contained in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property have completed their installations.</param>
        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            // Service recommended settings
            Settings.AutoOptimizationInterval = Settings.AutoOptimizationInterval == 0 ? 24 : Settings.AutoOptimizationInterval;
            Settings.AutoOptimizationMemoryUsage = Settings.AutoOptimizationMemoryUsage == 0 ? 10 : Settings.AutoOptimizationMemoryUsage;
            Settings.MemoryAreas = Enums.Memory.Areas.CombinedPageList | Enums.Memory.Areas.ModifiedFileCache | Enums.Memory.Areas.ModifiedPageList | Enums.Memory.Areas.RegistryCache | Enums.Memory.Areas.StandbyList | Enums.Memory.Areas.SystemFileCache | Enums.Memory.Areas.WorkingSet;
            Settings.RunOnPriority = Enums.Priority.Low;
            Settings.RunOnStartup = false;
            Settings.ShowOptimizationNotifications = false;
            Settings.Save();

            // Remove run on startup
            App.RunOnStartup(false);

            // Start service after install
            using (var sc = new ServiceController(_serviceInstaller.ServiceName))
                sc.Start();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Configuration.Install.Installer.BeforeInstall" /> event.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property are installed. This <see cref="T:System.Collections.IDictionary" /> object should be empty at this point.</param>
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" /Service";

            base.OnBeforeInstall(savedState);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Configuration.Install.Installer.BeforeUninstall" /> event.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer before the installers in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property uninstall their installations.</param>
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" /Service";

            base.OnBeforeUninstall(savedState);
        }

        /// <summary>
        /// Uninstalls the service
        /// </summary>
        public static void Uninstall()
        {
            if (AppService.IsInstalled)
            {
                // Processess that blocks service refresh/uninstallation
                var processesToKill = new[] { "mmc", "procexp", "procexp64", "taskmgr" };
                var processes = Process.GetProcesses().Where(process => process != null && processesToKill.Contains(process.ProcessName, StringComparer.OrdinalIgnoreCase));

                foreach (var process in processes)
                {
                    try
                    {
                        process.Kill();
                    }
                    catch
                    {
                        // ignored
                    }
                }

                ManagedInstallerClass.InstallHelper(new[]
                {
                    "/Uninstall",
                    "/LogFile=" + Constants.App.Name + ".log",
                    "/LogToConsole=true",
                    Assembly.GetExecutingAssembly().Location
                });

                // Kill any remaining process
                processes = Process.GetProcessesByName(Constants.App.Name);

                foreach (var process in processes)
                {
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
        }
    }
}
