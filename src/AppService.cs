using System;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Windows Memory Cleaner Service
    /// </summary>
    public class AppService : ServiceBase
    {
        private readonly Computer _computer;
        private readonly IComputerService _computerService;
        private DateTimeOffset _lastAutoOptimizationByInterval = DateTimeOffset.Now;
        private DateTimeOffset _lastAutoOptimizationByMemoryUsage = DateTimeOffset.Now;
        private readonly Timer _timer = new Timer(60000);

        /// <summary>
        /// Initializes a new instance of the <see cref="AppService"/> class.
        /// </summary>
        public AppService()
        {
            // App resources
            _computer = new Computer();
            _computerService = DependencyInjection.Container.Resolve<IComputerService>();

            // Service settings
            CanPauseAndContinue = false;
            ServiceName = Constants.App.Name;

            // Timer settings
            _timer.AutoReset = true;
            _timer.Elapsed += OnTimer;
        }

        /// <summary>
        /// Gets a value indicating whether the service is installed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the service is installed; otherwise, <c>false</c>.
        /// </value>
        internal static bool IsInstalled
        {
            get
            {
                return ServiceController.GetServices().Any(sc => string.Equals(sc.ServiceName, Constants.App.Name, StringComparison.OrdinalIgnoreCase));
            }
        }

        /// <summary>
        /// Called when debugging the service
        /// </summary>
        /// <param name="args"></param>
        internal void OnDebug(string[] args)
        {
            OnStart(args);
            OnTimer(null, null);

            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// Method specifies what code to execute each time a service is started. Here it configures parameters for the timer
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            _timer.Start();
        }

        /// <summary>
        /// Method specifies what code to execute each time a service is stopped. Here it configures parameters for the timer
        /// </summary>
        protected override void OnStop()
        {
            _timer.Stop();
        }

        /// <summary>
        /// Method specifies what code to execute each time a timer's interval is up
        /// /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            // App priority
            App.SetPriority(Settings.RunOnPriority);

            // Update memory info
            _computer.Memory = _computerService.Memory;

            // Interval
            if (Settings.AutoOptimizationInterval > 0 &&
                DateTimeOffset.Now.Subtract(_lastAutoOptimizationByInterval).TotalHours >= Settings.AutoOptimizationInterval)
            {
                DependencyInjection.Container.Resolve<IComputerService>().Optimize(Enums.Memory.Optimization.Reason.Schedule, Settings.MemoryAreas);

                _lastAutoOptimizationByInterval = DateTimeOffset.Now;
                return;
            }

            // Memory usage
            if (Settings.AutoOptimizationMemoryUsage > 0 &&
                _computer.Memory.Physical.Free.Percentage < Settings.AutoOptimizationMemoryUsage &&
                DateTimeOffset.Now.Subtract(_lastAutoOptimizationByMemoryUsage).TotalMinutes >= Constants.App.AutoOptimizationMemoryUsageInterval)
            {
                DependencyInjection.Container.Resolve<IComputerService>().Optimize(Enums.Memory.Optimization.Reason.LowMemory, Settings.MemoryAreas);

                _lastAutoOptimizationByMemoryUsage = DateTimeOffset.Now;
                return;
            }
        }
    }
}
