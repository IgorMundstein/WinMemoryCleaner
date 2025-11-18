using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
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

        private int _currentRotationAngle;
        private readonly Icon _imageIcon;
        private readonly NotifyIcon _notifyIcon;
        private DispatcherTimer _rotationTimer;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService" /> class.
        /// </summary>
        /// <param name="notifyIcon">Notify Icon</param>
        public NotificationService(NotifyIcon notifyIcon)
        {
            _currentRotationAngle = 0;
            _imageIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            _notifyIcon = notifyIcon;

            Initialize();
        }

        /// <summary>
        /// Initializes the notification service
        /// </summary>
        public void Initialize()
        {
            if (_notifyIcon == null)
                return;

            // Notification Areas (Menu)
            _notifyIcon.ContextMenuStrip = new TrayIconContextMenuControl();

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
                App.Shutdown();
            });

            Update(new Memory());

            _notifyIcon.Visible = true;
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
                    if (_rotationTimer != null)
                        _rotationTimer = null;
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex);
                }

                try
                {
                    if (_imageIcon != null)
                        _imageIcon.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex);
                }

                try
                {
                    if (_notifyIcon != null)
                        _notifyIcon.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Cleans up the rotation timer resources and resets the rotation angle
        /// </summary>
        private void CleanupRotationTimer()
        {
            try
            {
                _currentRotationAngle = 0;

                if (_rotationTimer != null)
                {
                    _rotationTimer.Stop();
                    _rotationTimer.Tick -= OnRotationTimerTick;
                    _rotationTimer = null;
                }
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
            }
        }

        /// <summary>
        /// Gets the background brush color based on memory usage and optimization state
        /// </summary>
        /// <param name="memory">The memory information</param>
        /// <param name="isOptimizing">if set to <c>true</c> the system is optimizing</param>
        /// <returns>A solid brush with the appropriate background color</returns>
        private Brush GetBackgroundBrush(Memory memory, bool isOptimizing)
        {
            try
            {
                if (Settings.TrayIconUseTransparentBackground)
                    return new SolidBrush(Color.Transparent);

                if (isOptimizing)
                {
                    var solidBrush = Settings.TrayIconOptimizingColor as SolidBrush;
                    return new SolidBrush(solidBrush.Color);
                }

                if (memory.Physical.Used.Percentage >= Settings.TrayIconDangerLevel)
                {
                    var solidBrush = Settings.TrayIconDangerColor as SolidBrush;
                    return new SolidBrush(solidBrush.Color);
                }

                if (memory.Physical.Used.Percentage >= Settings.TrayIconWarningLevel)
                {
                    var solidBrush = Settings.TrayIconWarningColor as SolidBrush;
                    return new SolidBrush(solidBrush.Color);
                }

                var backgroundBrush = Settings.TrayIconBackgroundColor as SolidBrush;
                return new SolidBrush(backgroundBrush.Color);
            }
            catch (Exception)
            {
                return new SolidBrush(Color.Black);
            }
        }

        /// <summary>
        /// Gets the appropriate tray icon based on settings and current state
        /// </summary>
        /// <param name="memory">The memory information</param>
        /// <param name="isOptimizing">if set to <c>true</c> the system is optimizing</param>
        /// <returns>The tray icon to display</returns>
        private Icon GetIcon(Memory memory, bool isOptimizing)
        {
            try
            {
                return Settings.TrayIconShowMemoryUsage ? GetMemoryUsageIcon(memory, isOptimizing) : GetImageIcon(isOptimizing);
            }
            catch
            {
                return _imageIcon;
            }
        }

        /// <summary>
        /// Gets the static application icon with optional rotation animation during optimization
        /// </summary>
        /// <param name="isOptimizing">if set to <c>true</c> the system is optimizing</param>
        /// <returns>The application icon, optionally rotated</returns>
        private Icon GetImageIcon(bool isOptimizing)
        {
            try
            {
                if (isOptimizing)
                {
                    StartRotationAnimation();

                    if (_currentRotationAngle > 0)
                        return GetRotatedIcon(_imageIcon, _currentRotationAngle);
                }
                else
                {
                    StopRotationAnimation();
                }

                return _imageIcon;
            }
            catch
            {
                return _imageIcon;
            }
        }

        /// <summary>
        /// Gets a custom icon displaying the current memory usage percentage
        /// </summary>
        /// <param name="memory">The memory information</param>
        /// <param name="isOptimizing">if set to <c>true</c> the system is optimizing</param>
        /// <returns>An icon with rendered memory percentage text</returns>
        private Icon GetMemoryUsageIcon(Memory memory, bool isOptimizing)
        {
            try
            {
                using (var image = new Bitmap(16, 16))
                using (var graphics = Graphics.FromImage(image))
                using (var font = new Font("Consolas", 15F, FontStyle.Regular, GraphicsUnit.Pixel))
                using (var format = new StringFormat())
                using (var backgroundBrush = GetBackgroundBrush(memory, isOptimizing))
                using (var textBrush = GetTextBrush(memory, isOptimizing))
                {
                    // Configure format
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    // Configure graphics quality
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

                    // Draw background
                    if (!Settings.TrayIconUseTransparentBackground)
                    {
                        using (var path = new GraphicsPath())
                        {
                            var radius = 10;

                            path.AddArc(0, 0, radius, radius, 180, 90);
                            path.AddArc(16 - radius, 0, radius, radius, 270, 90);
                            path.AddArc(16 - radius, 16 - radius, radius, radius, 0, 90);
                            path.AddArc(0, 16 - radius, radius, radius, 90, 90);
                            path.CloseFigure();

                            graphics.FillPath(backgroundBrush, path);
                        }
                    }

                    // Draw text
                    graphics.DrawString(string.Format(CultureInfo.InvariantCulture, "{0:00}", memory.Physical.Used.Percentage == 100 ? 99 : memory.Physical.Used.Percentage), font, textBrush, 8F, 9F, format);

                    var handle = image.GetHicon();

                    using (var icon = Icon.FromHandle(handle))
                    {
                        var clonedIcon = (Icon)icon.Clone();

                        NativeMethods.DestroyIcon(handle);

                        return clonedIcon;
                    }
                }
            }
            catch
            {
                return _imageIcon;
            }
        }

        /// <summary>
        /// Gets a rotated version of the specified icon
        /// </summary>
        /// <param name="icon">The icon to rotate</param>
        /// <param name="angle">The rotation angle in degrees</param>
        /// <returns>A new icon rotated by the specified angle</returns>
        private Icon GetRotatedIcon(Icon icon, float angle)
        {
            if (icon == null || angle == 0)
                return icon;

            try
            {
                using (var image = icon.ToBitmap())
                using (var rotatedImage = new Bitmap(image.Width, image.Height))
                using (var graphics = Graphics.FromImage(rotatedImage))
                {
                    // Configure graphics quality
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;

                    // Rotate around center point
                    var centerX = image.Width / 2f;
                    var centerY = image.Height / 2f;

                    graphics.TranslateTransform(centerX, centerY);
                    graphics.RotateTransform(angle);
                    graphics.TranslateTransform(-centerX, -centerY);

                    graphics.DrawImage(image, new Point(0, 0));

                    var handle = rotatedImage.GetHicon();

                    using (var tempIcon = Icon.FromHandle(handle))
                    {
                        var clonedIcon = (Icon)tempIcon.Clone();

                        NativeMethods.DestroyIcon(handle);

                        return clonedIcon;
                    }
                }
            }
            catch
            {
                return icon;
            }
        }

        /// <summary>
        /// Gets the tray icon tooltip text based on memory usage and optimization state
        /// </summary>
        /// <param name="memory">The memory information</param>
        /// <param name="isOptimizing">if set to <c>true</c> the system is optimizing</param>
        /// <returns>The formatted tooltip text</returns>
        private string GetText(Memory memory, bool isOptimizing)
        {
            try
            {
                string text;

                if (isOptimizing)
                    text = Localizer.String.Optimizing.ToUpper(Localizer.Culture);
                else
                {
                    text = Settings.ShowVirtualMemory
                        ? string.Format(Localizer.Culture, "{0}{1}: {2}%{0}{3}: {4}%", Environment.NewLine, Localizer.String.PhysicalMemory, memory.Physical.Used.Percentage, Localizer.String.VirtualMemory, memory.Virtual.Used.Percentage)
                        : string.Format(Localizer.Culture, "{0}{1}: {2}%", Environment.NewLine, Localizer.String.PhysicalMemory, memory.Physical.Used.Percentage);
                }
                
                // Truncate to 63 characters
                if (text.Length > 63)
                    text = text.Substring(0, 63);

                return text;
            }
            catch
            {
                return Constants.App.Title;
            }
        }

        /// <summary>
        /// Gets the text brush color based on memory usage and optimization state
        /// </summary>
        /// <param name="memory">The memory information</param>
        /// <param name="isOptimizing">if set to <c>true</c> the system is optimizing</param>
        /// <returns>A solid brush with the appropriate text color</returns>
        private Brush GetTextBrush(Memory memory, bool isOptimizing)
        {
            try
            {
                if (!Settings.TrayIconUseTransparentBackground)
                {
                    var solidBrush = Settings.TrayIconTextColor as SolidBrush;
                    return new SolidBrush(solidBrush.Color);
                }

                if (isOptimizing)
                {
                    var solidBrush = Settings.TrayIconOptimizingColor as SolidBrush;
                    return new SolidBrush(solidBrush.Color);
                }

                if (memory.Physical.Used.Percentage >= Settings.TrayIconDangerLevel)
                {
                    var solidBrush = Settings.TrayIconDangerColor as SolidBrush;
                    return new SolidBrush(solidBrush.Color);
                }

                if (memory.Physical.Used.Percentage >= Settings.TrayIconWarningLevel)
                {
                    var solidBrush = Settings.TrayIconWarningColor as SolidBrush;
                    return new SolidBrush(solidBrush.Color);
                }

                var textBrush = Settings.TrayIconTextColor as SolidBrush;
                return new SolidBrush(textBrush.Color);
            }
            catch (Exception)
            {
                return new SolidBrush(Color.White);
            }
        }

        /// <summary>
        /// Shows or hides the loading cursor and enables/disables the context menu
        /// </summary>
        /// <param name="running">if set to <c>true</c> shows loading cursor and disables menu</param>
        public void Loading(bool running)
        {
            if (Application.Current == null || Application.Current.Dispatcher == null)
                return;

            // Multi-threading trick
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                Mouse.OverrideCursor = running ? Cursors.Wait : null;

                if (_notifyIcon.ContextMenuStrip != null)
                    _notifyIcon.ContextMenuStrip.Enabled = !running;
            });
        }

        /// <summary>
        /// Displays a balloon tip notification from the system tray
        /// /// </summary>
        /// <param name="message">The notification message text</param>
        /// <param name="title">The notification title</param>
        /// <param name="timeout">The time period in seconds to display the notification</param>
        /// <param name="icon">The notification icon type</param>
        public void Notify(string message, string title = null, int timeout = 5, Enums.Icon.Notification icon = Enums.Icon.Notification.None)
        {
            if (_notifyIcon == null)
                return;

            try
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Visible = true;

                _notifyIcon.ShowBalloonTip(timeout * 1000, title, message, (ToolTipIcon)icon);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
            }
        }

        /// <summary>
        /// Handles the rotation timer tick event to animate the icon rotation
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void OnRotationTimerTick(object sender, EventArgs e)
        {
            try
            {
                _currentRotationAngle = (_currentRotationAngle + 90) % 360;

                _notifyIcon.Icon = GetRotatedIcon(_imageIcon, _currentRotationAngle);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
            }
        }

        /// <summary>
        /// Starts the icon rotation animation for the optimization state
        /// </summary>
        private void StartRotationAnimation()
        {
            if (_rotationTimer != null)
                return;

            if (Application.Current == null || Application.Current.Dispatcher == null)
                return;

            try
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    try
                    {
                        _currentRotationAngle = 0;

                        _rotationTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
                        _rotationTimer.Tick += OnRotationTimerTick;
                        _rotationTimer.Start();
                    }
                    catch (Exception ex)
                    {
                        Logger.Debug(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
            }
        }

        /// <summary>
        /// Stops the icon rotation animation
        /// </summary>
        private void StopRotationAnimation()
        {
            if (_rotationTimer == null)
                return;

            try
            {
                if (Application.Current == null || Application.Current.Dispatcher == null)
                {
                    CleanupRotationTimer();
                    return;
                }

                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    CleanupRotationTimer();
                });
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
            }
        }

        /// <summary>
        /// Updates the tray icon and tooltip text based on current memory usage and optimization state
        /// </summary>
        /// <param name="memory">The memory information</param>
        /// <param name="isOptimizing">if set to <c>true</c> the system is optimizing</param>
        /// <exception cref="ArgumentNullException">memory</exception>
        public void Update(Memory memory, bool isOptimizing = false)
        {
            if (memory == null)
                throw new ArgumentNullException("memory");

            if (_notifyIcon == null)
                return;

            _notifyIcon.Text = GetText(memory, isOptimizing);
            _notifyIcon.Icon = GetIcon(memory, isOptimizing);
        }

        #endregion
    }
}