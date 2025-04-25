using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Input;
using Application = System.Windows.Application;
using Cursors = System.Windows.Input.Cursors;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Notification Service
    /// </summary>
    public class NotificationService : INotificationService
    {
        #region Fields

        private readonly Icon _imageIcon;
        private readonly NotifyIcon _notifyIcon;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService" /> class.
        /// </summary>
        /// <param name="notifyIcon">Notify Icon</param>
        public NotificationService(NotifyIcon notifyIcon)
        {
            _imageIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            _notifyIcon = notifyIcon;

            Initialize();
        }

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
                try
                {
                    if (_imageIcon != null)
                    {
                        _imageIcon.Dispose();
                    }
                }
                catch
                {
                    // ignored
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
        /// Initializes
        /// </summary>
        public void Initialize()
        {
            if (_notifyIcon == null)
                return;

            // Notification Areas (Menu)
            _notifyIcon.ContextMenuStrip = new ContextMenuStripControl();

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

            Update(new Memory());

            _notifyIcon.Visible = true;
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
        public void Notify(string message, string title = null, int timeout = 5, Enums.Icon.Notification icon = Enums.Icon.Notification.None)
        {
            try
            {
                if (_notifyIcon == null)
                    return;

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
        public void Update(Memory memory)
        {
            if (_notifyIcon == null)
                return;

            // String
            try
            {
                if (memory == null)
                    throw new ArgumentNullException("memory");

                _notifyIcon.Text = Settings.ShowVirtualMemory
                    ? string.Format(Localizer.Culture, "{1}{0}{2}: {3}%{0}{4}: {5}%", Environment.NewLine, Localizer.String.MemoryUsage.ToUpper(Localizer.Culture), Localizer.String.Physical, memory.Physical.Used.Percentage, Localizer.String.Virtual, memory.Virtual.Used.Percentage)
                    : string.Format(Localizer.Culture, "{1}{0}{2}: {3}%", Environment.NewLine, Localizer.String.MemoryUsage.ToUpper(Localizer.Culture), Localizer.String.Physical, memory.Physical.Used.Percentage);
            }
            catch
            {
                if (_notifyIcon != null)
                    _notifyIcon.Text = string.Empty;
            }

            // Icon
            try
            {
                switch (Settings.TrayIcon)
                {
                    case Enums.Icon.Tray.Image:
                        _notifyIcon.Icon = _imageIcon;
                        break;

                    case Enums.Icon.Tray.MemoryUsage:
                        if (memory == null)
                            throw new ArgumentNullException("memory");

                        if (memory.Physical.Used.Percentage == 0)
                            return;

                        using (var image = new Bitmap(16, 16))
                        using (var graphics = Graphics.FromImage(image))
                        using (var font = new Font("Arial", 14F, GraphicsUnit.Pixel))
                        using (var format = new StringFormat())
                        {
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;

                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                            graphics.FillRectangle(memory.Physical.Used.Percentage >= 90 ? Brushes.DarkRed : memory.Physical.Used.Percentage >= 80 ? Brushes.DarkGoldenrod : Brushes.Black, 0, 0, 16, 16);
                            graphics.DrawString(string.Format(Localizer.Culture, "{0:00}", memory.Physical.Used.Percentage == 100 ? 0 : memory.Physical.Used.Percentage), font, Brushes.WhiteSmoke, 9, 9, format);

                            var handle = image.GetHicon();

                            using (var icon = Icon.FromHandle(handle))
                                _notifyIcon.Icon = (Icon)icon.Clone();

                            NativeMethods.DestroyIcon(handle);
                        }
                        break;
                }
            }
            catch
            {
                if (_notifyIcon != null)
                    _notifyIcon.Icon = _imageIcon;
            }
        }

        #endregion
    }
}