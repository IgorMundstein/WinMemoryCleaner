using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using WinFormsColor = System.Drawing.Color;
using WpfBrush = System.Windows.Media.SolidColorBrush;
using WpfColor = System.Windows.Media.Color;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Centralized theme manager that provides all theme resources with enhanced security
    /// </summary>
    public static class ThemeManager
    {
        private const int MAX_BRUSHES_COUNT = 500;
        private const int MAX_RESOURCE_KEY_LENGTH = 100;
        private const int MAX_HEX_COLOR_LENGTH = 9;
        private const int INITIALIZATION_TIMEOUT_MS = 30000;

        private static readonly Regex _hexColorPattern = new Regex(@"^#([A-Fa-f0-9]{3}|[A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$", RegexOptions.Compiled);
        private static readonly Regex _resourceKeyPattern = new Regex(@"^[a-zA-Z0-9_.]+$", RegexOptions.Compiled);

        private static readonly object _lockObject = new object();
        private static readonly object _initializationLock = new object();
        private static volatile bool _isInitialized;
        private static volatile bool _isInitializing;
        private static readonly Timer _initializationTimer = InitializeTimer();

        private static Enums.Theme _theme = Enums.Theme.Dark;
        private static List<WpfBrush> _brushes;
        private static List<Enums.Theme> _themes;
        private static readonly Dictionary<string, WinFormsColor> _colorCache = new Dictionary<string, WinFormsColor>(StringComparer.Ordinal);
        private static DateTime _lastInitializationAttempt = DateTime.MinValue;

        private static Timer InitializeTimer()
        {
            try
            {
                return new Timer(OnInitializationTimeout, null, Timeout.Infinite, Timeout.Infinite);
            }
            catch (Exception e)
            {
                try
                {
                    Logger.Error("Failed to initialize timer: " + e.Message);
                }
                catch
                {
                }
                return null;
            }
        }

        private static void OnInitializationTimeout(object state)
        {
            try
            {
                Logger.Error("ThemeManager initialization timed out");

                lock (_initializationLock)
                {
                    if (_isInitializing)
                    {
                        _isInitializing = false;
                        InitializeMinimalDefaults();
                        _isInitialized = true;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Initialization timeout handler failed: " + e.Message);
            }
        }

        private static void InitializeMinimalDefaults()
        {
            try
            {
                _themes = new List<Enums.Theme> { Enums.Theme.Dark, Enums.Theme.Light };
                _brushes = new List<WpfBrush>();
                _theme = Enums.Theme.Dark;
            }
            catch (Exception e)
            {
                Logger.Error("Minimal defaults initialization failed: " + e.Message);
            }
        }

        private static void EnsureInitialized()
        {
            if (_isInitialized)
                return;

            var timeSinceLastAttempt = DateTime.UtcNow - _lastInitializationAttempt;
            if (timeSinceLastAttempt.TotalSeconds < 1 && _lastInitializationAttempt != DateTime.MinValue)
            {
                Thread.Sleep(100);
                if (_isInitialized)
                    return;
            }

            lock (_initializationLock)
            {
                if (_isInitialized)
                    return;

                if (_isInitializing)
                {
                    Monitor.Wait(_initializationLock, 5000);
                    return;
                }

                _isInitializing = true;
                _lastInitializationAttempt = DateTime.UtcNow;

                try
                {
                    if (_initializationTimer != null)
                        _initializationTimer.Change(INITIALIZATION_TIMEOUT_MS, Timeout.Infinite);

                    InitializeDefaults();
                    InitializeThemes();
                    InitializeBrushes();

                    _isInitialized = true;
                    _isInitializing = false;

                    if (_initializationTimer != null)
                        _initializationTimer.Change(Timeout.Infinite, Timeout.Infinite);

                    Monitor.PulseAll(_initializationLock);
                }
                catch (Exception e)
                {
                    Logger.Error("Initialization failed: " + e.Message);

                    InitializeMinimalDefaults();
                    _isInitialized = true;
                    _isInitializing = false;

                    Monitor.PulseAll(_initializationLock);
                }
            }
        }

        private static bool IsValidResourceKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            if (key.Length > MAX_RESOURCE_KEY_LENGTH)
                return false;

            try
            {
                return _resourceKeyPattern.IsMatch(key);
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidTheme(Enums.Theme theme)
        {
            try
            {
                return Enum.IsDefined(typeof(Enums.Theme), theme);
            }
            catch
            {
                return false;
            }
        }

        private static void InitializeDefaults()
        {
            try
            {
                var app = Application.Current;
                if (app == null || app.Resources == null)
                {
                    Logger.Warning("Application.Current or Resources is null during theme initialization");
                    return;
                }

                var defaultResources = new Dictionary<string, WpfColor>
                {
                    { "Accent", WpfColor.FromArgb(0xFF, 0x00, 0xAE, 0xF7) },
                    { "MemoryBarIndicatorBackground", WpfColor.FromArgb(0xFF, 0xE0, 0x36, 0x0A) },
                    { "MemoryBarTrackBackground", WpfColor.FromArgb(0xFF, 0x00, 0xAA, 0x41) },
                    { "PrimaryBackground", WpfColor.FromArgb(0xFF, 0x20, 0x20, 0x20) },
                    { "PrimaryBorder", WpfColor.FromArgb(0xFF, 0x30, 0x30, 0x30) },
                    { "SecondaryBackground", WpfColor.FromArgb(0xFF, 0x2C, 0x2C, 0x2C) },
                    { "SecondaryBorder", WpfColor.FromArgb(0xFF, 0x70, 0x70, 0x70) },
                    { "SecondaryDisabled", WpfColor.FromArgb(0xFF, 0x99, 0x99, 0x99) },
                    { "SecondaryForeground", WpfColor.FromArgb(0xFF, 0xFF, 0xFF, 0xFF) }
                };

                foreach (var kvp in defaultResources)
                {
                    try
                    {
                        if (!IsValidResourceKey(kvp.Key))
                        {
                            Logger.Warning("Invalid resource key skipped: " + kvp.Key);
                            continue;
                        }

                        app.Resources[kvp.Key] = new WpfBrush(kvp.Value);
                    }
                    catch (Exception e)
                    {
                        Logger.Error("Failed to set default resource " + kvp.Key + ": " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Default resources initialization failed: " + e.Message);
            }
        }

        private static void InitializeThemes()
        {
            try
            {
                var enumValues = Enum.GetValues(typeof(Enums.Theme));
                if (enumValues == null || enumValues.Length == 0)
                {
                    _themes = new List<Enums.Theme> { Enums.Theme.Dark };
                    return;
                }

                _themes = new List<Enums.Theme>();

                foreach (Enums.Theme theme in enumValues)
                {
                    if (IsValidTheme(theme))
                    {
                        _themes.Add(theme);
                    }
                }

                if (_themes.Count == 0)
                {
                    _themes.Add(Enums.Theme.Dark);
                }
            }
            catch (Exception e)
            {
                Logger.Error("Theme initialization failed: " + e.Message);
                _themes = new List<Enums.Theme> { Enums.Theme.Dark };
            }
        }

        private static void InitializeBrushes()
        {
            try
            {
                var colorType = typeof(WinFormsColor);
                if (colorType == null)
                {
                    _brushes = new List<WpfBrush>();
                    return;
                }

                var colorProperties = colorType.GetProperties(BindingFlags.Public | BindingFlags.Static);
                if (colorProperties == null || colorProperties.Length == 0)
                {
                    _brushes = new List<WpfBrush>();
                    return;
                }

                _brushes = new List<WpfBrush>();
                var processedCount = 0;

                foreach (var property in colorProperties)
                {
                    try
                    {
                        if (processedCount >= MAX_BRUSHES_COUNT)
                        {
                            Logger.Warning("Brush count limit reached: " + MAX_BRUSHES_COUNT);
                            break;
                        }

                        if (property == null || property.PropertyType != typeof(WinFormsColor))
                            continue;

                        var color = (WinFormsColor)property.GetValue(null, null);

                        if (color.A == 255)
                        {
                            var wpfColor = WpfColor.FromArgb(color.A, color.R, color.G, color.B);
                            _brushes.Add(new WpfBrush(wpfColor));
                            processedCount++;
                        }
                    }
                    catch (Exception e)
                    {
                        var propertyName = (property != null) ? property.Name : "unknown";
                        Logger.Error("Failed to process color property " + propertyName + ": " + e.Message);
                    }
                }

                try
                {
                    _brushes = _brushes
                        .OrderBy(GetColorHueSafe)
                        .ThenBy(GetColorSaturationSafe)
                        .ThenBy(GetColorBrightnessSafe)
                        .ToList();
                }
                catch (Exception e)
                {
                    Logger.Error("Brush sorting failed: " + e.Message);
                }
            }
            catch (Exception e)
            {
                Logger.Error("Brush initialization failed: " + e.Message);
                _brushes = new List<WpfBrush>();
            }
        }

        private static float GetColorHueSafe(WpfBrush brush)
        {
            try
            {
                if (brush == null)
                    return 0f;

                var color = brush.Color;
                var winFormsColor = WinFormsColor.FromArgb(color.A, color.R, color.G, color.B);
                var hue = winFormsColor.GetHue();

                if (float.IsNaN(hue) || float.IsInfinity(hue))
                    return 0f;

                return Math.Max(0f, Math.Min(360f, hue));
            }
            catch
            {
                return 0f;
            }
        }

        private static float GetColorSaturationSafe(WpfBrush brush)
        {
            try
            {
                if (brush == null)
                    return 0f;

                var color = brush.Color;
                var winFormsColor = WinFormsColor.FromArgb(color.A, color.R, color.G, color.B);
                var saturation = winFormsColor.GetSaturation();

                if (float.IsNaN(saturation) || float.IsInfinity(saturation))
                    return 0f;

                return Math.Max(0f, Math.Min(1f, saturation));
            }
            catch
            {
                return 0f;
            }
        }

        private static float GetColorBrightnessSafe(WpfBrush brush)
        {
            try
            {
                if (brush == null)
                    return 0f;

                var color = brush.Color;
                var winFormsColor = WinFormsColor.FromArgb(color.A, color.R, color.G, color.B);
                var brightness = winFormsColor.GetBrightness();

                if (float.IsNaN(brightness) || float.IsInfinity(brightness))
                    return 0f;

                return Math.Max(0f, Math.Min(1f, brightness));
            }
            catch
            {
                return 0f;
            }
        }

        /// <summary>
        /// Gets the accent color for Windows Forms controls.
        /// </summary>
        public static WinFormsColor AccentColor
        {
            get { return GetColorSafe(Helper.NameOf(() => AccentColor).Replace("Color", string.Empty), WinFormsColor.DeepSkyBlue); }
        }

        /// <summary>
        /// Gets the secondary background color for Windows Forms controls.
        /// </summary>
        public static WinFormsColor SecondaryBackgroundColor
        {
            get { return GetColorSafe(Helper.NameOf(() => SecondaryBackgroundColor).Replace("Color", string.Empty), WinFormsColor.DarkSlateGray); }
        }

        /// <summary>
        /// Gets the secondary border color for Windows Forms controls.
        /// </summary>
        public static WinFormsColor SecondaryBorderColor
        {
            get { return GetColorSafe(Helper.NameOf(() => SecondaryBorderColor).Replace("Color", string.Empty), WinFormsColor.DimGray); }
        }

        /// <summary>
        /// Gets the secondary foreground color for Windows Forms controls.
        /// </summary>
        public static WinFormsColor SecondaryForegroundColor
        {
            get { return GetColorSafe(Helper.NameOf(() => SecondaryForegroundColor).Replace("Color", string.Empty), WinFormsColor.White); }
        }

        private static WinFormsColor GetColorSafe(string resourceKey, WinFormsColor fallback)
        {
            if (!IsValidResourceKey(resourceKey))
            {
                Logger.Warning("Invalid resource key: " + resourceKey);
                return fallback;
            }

            try
            {
                lock (_colorCache)
                {
                    WinFormsColor cachedColor;
                    if (_colorCache.TryGetValue(resourceKey, out cachedColor))
                    {
                        return cachedColor;
                    }
                }

                EnsureInitialized();

                var app = Application.Current;
                if (app == null || app.Resources == null)
                    return fallback;

                var resource = app.Resources[resourceKey] as WpfBrush;
                if (resource == null)
                    return fallback;

                var color = resource.Color;
                var result = WinFormsColor.FromArgb(color.A, color.R, color.G, color.B);

                lock (_colorCache)
                {
                    if (!_colorCache.ContainsKey(resourceKey))
                    {
                        _colorCache[resourceKey] = result;
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                Logger.Error("Failed to get color for resource " + resourceKey + ": " + e.Message);
                return fallback;
            }
        }

        /// <summary>
        /// Gets the brushes with defensive copying.
        /// </summary>
        public static List<WpfBrush> Brushes
        {
            get
            {
                EnsureInitialized();
                lock (_lockObject)
                {
                    var brushes = _brushes;
                    if (brushes == null || brushes.Count == 0)
                        return new List<WpfBrush>();

                    return new List<WpfBrush>(brushes);
                }
            }
        }

        /// <summary>
        /// Gets or sets the theme with enhanced validation and error handling.
        /// </summary>
        public static Enums.Theme Theme
        {
            get
            {
                EnsureInitialized();
                lock (_lockObject)
                {
                    return _theme;
                }
            }
            set
            {
                if (!IsValidTheme(value))
                {
                    Logger.Warning("Invalid theme value: " + value);
                    throw new ArgumentException("Invalid theme value: " + value, "value");
                }

                EnsureInitialized();

                lock (_lockObject)
                {
                    if (_theme == value)
                        return;

                    var oldTheme = _theme;

                    try
                    {
                        Load(value);
                        _theme = value;

                        lock (_colorCache)
                        {
                            _colorCache.Clear();
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error("Failed to load theme " + value + ", reverting to " + oldTheme + ": " + e.Message);
                        _theme = oldTheme;
                        throw;
                    }
                }

                try
                {
                    App.ReleaseMemory();
                }
                catch (Exception e)
                {
                    Logger.Error("Failed to release memory after theme change: " + e.Message);
                }
            }
        }

        /// <summary>
        /// Gets the themes with defensive copying.
        /// </summary>
        public static List<Enums.Theme> Themes
        {
            get
            {
                EnsureInitialized();
                lock (_lockObject)
                {
                    var themes = _themes;
                    if (themes == null || themes.Count == 0)
                    {
                        return new List<Enums.Theme> { Enums.Theme.Dark };
                    }

                    return new List<Enums.Theme>(themes);
                }
            }
        }

        private static bool IsValidHexColor(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return false;

            if (hex.Length > MAX_HEX_COLOR_LENGTH)
                return false;

            try
            {
                return _hexColorPattern.IsMatch(hex);
            }
            catch
            {
                return false;
            }
        }

        private static void Load(Enums.Theme theme)
        {
            Theme resource;

            try
            {
                if (string.IsNullOrEmpty(Constants.App.ThemesResourcePath) ||
                    string.IsNullOrEmpty(Constants.App.EmbeddedResourcePathExtension))
                {
                    Logger.Error("Theme resource path constants are null or empty");
                    return;
                }

                if (Constants.App.ThemesResourcePath.Contains("..") ||
                    Constants.App.EmbeddedResourcePathExtension.Contains(".."))
                {
                    Logger.Error("Theme resource path contains directory traversal characters");
                    return;
                }

                var resourcePath = string.Format(CultureInfo.InvariantCulture,
                    "{0}{1}Theme{2}",
                    Constants.App.ThemesResourcePath,
                    theme,
                    Constants.App.EmbeddedResourcePathExtension);

                if (resourcePath.Length > 260)
                {
                    Logger.Error("Theme resource path is too long");
                    return;
                }

                resource = Helper.ReadEmbeddedResource<Theme>(resourcePath);
            }
            catch (Exception e)
            {
                Logger.Error("Failed to read embedded theme resource for " + theme + ": " + e.Message);
                return;
            }

            if (resource == null)
            {
                Logger.Warning("Theme resource is null for " + theme);
                return;
            }

            try
            {
                var app = Application.Current;
                if (app == null || app.Resources == null)
                {
                    Logger.Warning("Application.Current or Resources is null during theme loading");
                    return;
                }

                var properties = typeof(Theme).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (properties == null || properties.Length == 0)
                {
                    Logger.Warning("No properties found on Theme class");
                    return;
                }

                foreach (var property in properties)
                {
                    if (property == null)
                        continue;

                    if (!IsValidResourceKey(property.Name))
                    {
                        Logger.Warning("Invalid property name skipped: " + property.Name);
                        continue;
                    }

                    string hex;

                    try
                    {
                        hex = property.GetValue(resource, null) as string;
                    }
                    catch (Exception e)
                    {
                        Logger.Error("Failed to get property value for " + property.Name + ": " + e.Message);
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(hex))
                        continue;

                    if (!IsValidHexColor(hex))
                    {
                        Logger.Warning("Invalid hex color '" + hex + "' for property " + property.Name);
                        continue;
                    }

                    try
                    {
                        var color = ParseHexColor(hex);
                        app.Resources[property.Name] = new WpfBrush(color);
                    }
                    catch (Exception e)
                    {
                        Logger.Error("Failed to parse or set color for property " + property.Name + ": " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Failed to apply theme " + theme + ": " + e.Message);
            }
        }

        private static WpfColor ParseHexColor(string hexColor)
        {
            if (string.IsNullOrEmpty(hexColor))
                throw new ArgumentException("Hex color cannot be null or empty", "hexColor");

            if (!IsValidHexColor(hexColor))
                throw new ArgumentException("Invalid hex color format: " + hexColor, "hexColor");

            try
            {
                var color = System.Drawing.ColorTranslator.FromHtml(hexColor);
                return WpfColor.FromArgb(color.A, color.R, color.G, color.B);
            }
            catch (Exception e)
            {
                Logger.Error("Failed to parse hex color '" + hexColor + "': " + e.Message);
                throw new ArgumentException("Invalid hex color format: " + hexColor, "hexColor", e);
            }
        }

        /// <summary>
        /// Cleanup method for proper resource disposal
        /// </summary>
        public static void Cleanup()
        {
            try
            {
                lock (_initializationLock)
                {
                    if (_initializationTimer != null)
                        _initializationTimer.Dispose();
                }

                lock (_colorCache)
                {
                    _colorCache.Clear();
                }
            }
            catch (Exception e)
            {
                Logger.Error("Cleanup failed: " + e.Message);
            }
        }
    }
}