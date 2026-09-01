using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using Cursors = System.Windows.Input.Cursors;
using WpfApplication = System.Windows.Application;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Notification Service
    /// </summary>
    public class NotificationService : INotificationService
    {
        #region Fields

        private volatile int _currentRotationAngle;
        private Icon _currentIcon;
        private volatile bool _disposed;
        private readonly Icon _imageIcon;
        private readonly NotifyIcon _notifyIcon;
        private readonly object _disposeLock = new object();

        /// <summary>
        /// Serializes icon rendering. Held by both <see cref="GetRotatedIcon" /> and
        /// <see cref="GetMemoryUsageIcon" />, which the UI thread (rotation tick) and the
        /// background monitor threads can reach at the same time.
        /// </summary>
        /// <remarks>
        /// Rendering used to be serialized incidentally, because <see cref="Update" /> held
        /// <see cref="_disposeLock" /> for its whole duration. That lock had to go to break the
        /// deadlock described on <see cref="InvokeOnUi" />, so this one restores the guarantee
        /// explicitly. It covers both render paths on purpose: GDI+ types are not thread safe,
        /// and a partially covered invariant invites a future change to cache a Font, Brush or
        /// StringFormat in a field and reintroduce a data race. Never held across a dispatcher
        /// call.
        /// </remarks>
        private readonly object _iconRenderLock = new object();

        /// <summary>
        /// Guards the <see cref="_currentIcon" /> swap. Normally that swap is serialized by
        /// running on the UI thread, but when no dispatcher exists the work runs inline on the
        /// calling thread, so concurrent callers could otherwise both dispose the same icon.
        /// Only ever held for a field assignment; never across a dispatcher call.
        /// </summary>
        private readonly object _currentIconLock = new object();

        /// <summary>
        /// Owned by the UI thread. Only ever read or written inside a dispatcher callback,
        /// which makes the UI thread the single point of truth and removes the need to hold
        /// <see cref="_disposeLock" /> across a dispatcher call to keep it consistent.
        /// </summary>
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
                lock (_disposeLock)
                {
                    if (_disposed)
                        return;

                    _disposed = true;
                }

                // DispatcherTimer has thread affinity: Stop() from another thread throws, which
                // previously left the timer running and kept firing ticks during shutdown.
                InvokeOnUi(CleanupRotationTimer);

                try
                {
                    if (_notifyIcon != null)
                    {
                        _notifyIcon.Visible = false;
                        _notifyIcon.Icon = null;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex);
                }

                try
                {
                    lock (_currentIconLock)
                    {
                        if (_currentIcon != null && _currentIcon != _imageIcon)
                        {
                            _currentIcon.Dispose();
                            _currentIcon = null;
                        }
                    }
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
        /// Runs an action on the UI thread without ever blocking the calling thread.
        /// </summary>
        /// <remarks>
        /// Must stay non-blocking. This used to be a blocking <c>Dispatcher.Invoke</c> reached
        /// from <see cref="Update" /> on a background thread that held <see cref="_disposeLock" />,
        /// while the UI thread took that same lock in <see cref="OnRotationTimerTick" />. Each
        /// side then waited on the other (AB-BA), which froze the window and left a process that
        /// only Task Manager could end. Both halves of that cycle are now gone, and using
        /// <c>BeginInvoke</c> here is what keeps it from being reintroduced by a caller that
        /// holds a lock.
        /// </remarks>
        /// <param name="action">The action to run on the UI thread.</param>
        private static void InvokeOnUi(Action action)
        {
            if (action == null)
                return;

            try
            {
                var application = WpfApplication.Current;
                var dispatcher = application == null ? null : application.Dispatcher;

                // No dispatcher means there is no UI thread to marshal to, so running inline is
                // both correct and necessary: returning here would silently drop the update.
                if (dispatcher == null || dispatcher.CheckAccess())
                    action();
                else
                    dispatcher.BeginInvoke(action);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
            }
        }

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
                lock (_iconRenderLock)
                {
                    using (var image = new Bitmap(16, 16))
                    using (var graphics = Graphics.FromImage(image))
                    using (var font = new Font("Consolas", 14F, FontStyle.Regular, GraphicsUnit.Pixel))
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
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

                        // Draw background
                        if (!Settings.TrayIconUseTransparentBackground)
                        {
                            using (var path = new GraphicsPath())
                            {
                                path.AddArc(0, 0, 10, 10, 180, 90);
                                path.AddArc(5, 0, 10, 10, 270, 90);
                                path.AddArc(5, 5, 10, 10, 0, 90);
                                path.AddArc(0, 5, 10, 10, 90, 90);
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
                // icon is the shared _imageIcon; ToBitmap is a GDI read that must not run
                // concurrently from the UI thread and the monitor thread.
                lock (_iconRenderLock)
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
                        ? string.Format(Localizer.Culture, "{0}: {1}%{2}{3}: {4}%", Localizer.String.PhysicalMemory, memory.Physical.Used.Percentage, Environment.NewLine, Localizer.String.VirtualMemory, memory.Virtual.Used.Percentage)
                        : string.Format(Localizer.Culture, "{0}: {1}%", Localizer.String.PhysicalMemory, memory.Physical.Used.Percentage);
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
            // Non-blocking: this is called from background threads that hold locks (the
            // optimization path sets IsBusy while holding the view model lock).
            InvokeOnUi(() =>
            {
                try
                {
                    if (_disposed)
                        return;

                    Mouse.OverrideCursor = running ? Cursors.Wait : null;

                    if (_notifyIcon != null && _notifyIcon.ContextMenuStrip != null)
                        _notifyIcon.ContextMenuStrip.Enabled = !running;
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex);
                }
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

            // Marshalled: the optimization path calls this from a background thread, and
            // NotifyIcon.Visible / ShowBalloonTip must run on the thread that owns the icon.
            InvokeOnUi(() =>
            {
                try
                {
                    if (_disposed || _notifyIcon == null)
                        return;

                    _notifyIcon.Visible = false;
                    _notifyIcon.Visible = true;

                    _notifyIcon.ShowBalloonTip(timeout * 1000, title, message, (ToolTipIcon)icon);
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex);
                }
            });
        }

        /// <summary>
        /// Handles the rotation timer tick event to animate the icon rotation
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void OnRotationTimerTick(object sender, EventArgs e)
        {
            // Already on the UI thread. Taking _disposeLock here is what let a background
            // thread inside Update block the UI thread, so the lock is deliberately absent.
            if (_disposed)
                return;

            try
            {
                _currentRotationAngle = (_currentRotationAngle + 90) % 360;

                ApplyIcon(_notifyIcon.Text, GetRotatedIcon(_imageIcon, _currentRotationAngle));
            }
            catch (ObjectDisposedException)
            {
                // Already disposed, ignore
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
            InvokeOnUi(() =>
            {
                try
                {
                    // Checked on the UI thread so two concurrent starts cannot both create a timer
                    if (_rotationTimer != null || _disposed)
                        return;

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

        /// <summary>
        /// Stops the icon rotation animation
        /// </summary>
        private void StopRotationAnimation()
        {
            // No _rotationTimer check here on purpose. The field is owned by the UI thread and
            // is not volatile, so a caller on another thread can read a stale null and skip the
            // cleanup, leaving the animation running. CleanupRotationTimer does the null check
            // on the UI thread, where the read is valid.
            InvokeOnUi(CleanupRotationTimer);
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

            if (_disposed || _notifyIcon == null)
                return;

            string text;
            Icon newIcon;

            // Rendering happens on the calling thread, outside any lock, so a slow GDI draw
            // never stalls the UI thread and never blocks a lock the UI thread needs.
            try
            {
                text = GetText(memory, isOptimizing);
                newIcon = GetIcon(memory, isOptimizing);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return;
            }

            // NotifyIcon has thread affinity: its window was created on the UI thread, so
            // assigning Text/Icon from a pool thread can hang on the shell notification call.
            InvokeOnUi(() => ApplyIcon(text, newIcon));
        }

        /// <summary>
        /// Assigns the tray icon text and image, then releases the icon it replaced.
        /// </summary>
        /// <remarks>
        /// Normally invoked on the UI thread. When no dispatcher exists it runs inline on the
        /// calling thread instead, so the icon swap is guarded by <see cref="_currentIconLock" />
        /// rather than relying on UI-thread serialization.
        /// </remarks>
        /// <param name="text">The tooltip text.</param>
        /// <param name="newIcon">The icon to display.</param>
        private void ApplyIcon(string text, Icon newIcon)
        {
            try
            {
                if (_disposed || _notifyIcon == null)
                {
                    if (newIcon != null && newIcon != _imageIcon)
                        newIcon.Dispose();

                    return;
                }

                Icon oldIcon;

                lock (_currentIconLock)
                {
                    oldIcon = _currentIcon;

                    _notifyIcon.Text = text;
                    _notifyIcon.Icon = newIcon;
                    _currentIcon = newIcon;
                }

                if (oldIcon != null && oldIcon != _imageIcon && oldIcon != newIcon)
                {
                    try
                    {
                        oldIcon.Dispose();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // Already disposed, ignore
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
            }
        }

        #endregion
    }
}