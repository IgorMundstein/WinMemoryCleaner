using System;
using System.Windows.Forms;
using System.Windows.Input;
using Application = System.Windows.Application;
using Cursors = System.Windows.Input.Cursors;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Notification Service
    /// </summary>
    internal class NotificationService : INotificationService
    {
        private readonly NotifyIcon _notifyIcon;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService" /> class.
        /// </summary>
        /// <param name="notifyIcon">Notify Icon</param>
        public NotificationService(NotifyIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;

            Initialize();
        }

        /// <summary>
        /// Initializes
        /// </summary>
        public void Initialize()
        {
            if (_notifyIcon == null)
                return;

            // Notification Area (Menu)
            _notifyIcon.ContextMenuStrip = new CustomContextMenuStrip();

            // Optimize
            _notifyIcon.ContextMenuStrip.Items.Add(Localizer.String.Optimize, null, (sender, args) =>
            {
                var mainViewModel = DependencyInjection.Container.Resolve<MainViewModel>();

                if (mainViewModel.OptimizeCommand.CanExecute(null))
                    mainViewModel.OptimizeCommand.Execute(null);
            });

            _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());

            // Exit
            _notifyIcon.ContextMenuStrip.Items.Add(Localizer.String.Exit, null, (sender, args) =>
            {
                Application.Current.Shutdown();
            });
        }

        /// <summary>
        /// Show/Hide Loading
        /// </summary>
        /// <param name="running">True (ON) / False (OFF)</param>
        public void Loading(bool running)
        {
            // Multi-threading trick
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                Mouse.OverrideCursor = running ? Cursors.Wait : null;

                if (_notifyIcon != null && _notifyIcon.ContextMenuStrip != null)
                    _notifyIcon.ContextMenuStrip.Enabled = !running;
            });
        }

        /// <summary>
        /// Displays a Notification
        /// </summary>
        /// <param name="message">The text</param>
        /// <param name="title">The title</param>
        /// <param name="timeout">The time period, in seconds</param>
        /// <param name="icon">The icon</param>
        public void Notify(string message, string title = null, int timeout = 5, Enums.NotificationIcon icon = Enums.NotificationIcon.None)
        {
            try
            {
                if (_notifyIcon != null)
                    _notifyIcon.ShowBalloonTip(timeout * 1000, title, message, (ToolTipIcon)icon);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Update Info
        /// </summary>
        /// <param name="memory">The memory.</param>
        public void UpdateInfo(Memory memory)
        {
            try
            {
                _notifyIcon.Text = string.Format("{0} {1}{2}%", Localizer.String.MemoryUsage, Environment.NewLine, memory.UsedPercentage);
            }
            catch
            {
                // ignored
            }
        }
    }
}