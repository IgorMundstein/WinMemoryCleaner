using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace WinMemoryCleaner
{
    public static class Localizer
    {
        #region Events

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        #endregion

        #region Fields

        private static CultureInfo _culture;
        private static Language _language;
        private static bool _isInitialized;
        private static readonly Lazy<Localization> _fallbackLocalizationLazy = new(InitializeFallback);
        private static List<Language> _cachedLanguages;
        private static readonly object _languagesLock = new();

        #endregion

        #region Constructor

        static Localizer()
        {
            String = _fallbackLocalizationLazy.Value;
            _isInitialized = true;
        }

        #endregion

        #region Properties

        public static CultureInfo Culture
        {
            get => _culture;
            private set
            {
                _culture = value;
                RaiseStaticPropertyChanged();
            }
        }

        public static Language Language
        {
            get => _language;
            set
            {
                if (_language != null && _language.Equals(value))
                    return;

                try
                {
                    if (value == null)
                        throw new ArgumentNullException(nameof(value));

                    Load(value);

                    Settings.Language = value.Name;
                    Settings.Save();
                }
                catch
                {
                    Settings.Language = Constants.Windows.Locale.Name.English;
                    Settings.Save();
                    throw;
                }

                _language = value;
                RaiseStaticPropertyChanged(string.Empty);

                if (_isInitialized)
                    App.ReleaseMemory();
            }
        }

        public static List<Language> Languages
        {
            get
            {
                if (_cachedLanguages != null)
                    return _cachedLanguages;

                lock (_languagesLock)
                {
                    if (_cachedLanguages != null)
                        return _cachedLanguages;

                    try
                    {
                        var assembly = Assembly.GetExecutingAssembly();
                        var resourcePrefix = Constants.App.LocalizationResourcePath;
                        var resourceSuffix = Constants.App.EmbeddedResourcePathExtension;

                        var resourceNames = assembly.GetManifestResourceNames()
                            .Where(file => file.StartsWith(resourcePrefix, StringComparison.OrdinalIgnoreCase) &&
                                         file.EndsWith(resourceSuffix, StringComparison.OrdinalIgnoreCase))
                            .Select(file => file[resourcePrefix.Length..^resourceSuffix.Length])
                            .OrderBy(file => file)
                            .ToList();

                        try
                        {
                            var searchDirectories = new[]
                            {
                                AppDomain.CurrentDomain.BaseDirectory,
                                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Localization"),
                                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Themes")
                            };

                            var localResources = new List<string>();
                            foreach (var dir in searchDirectories)
                            {
                                try
                                {
                                    var files = Directory.GetFiles(dir,
                                        $"*{Constants.App.EmbeddedResourcePathExtension}", SearchOption.TopDirectoryOnly);
                                    localResources.AddRange(files.Select(Path.GetFileNameWithoutExtension));
                                }
                                catch
                                {
                                }
                            }

                            if (localResources.Any())
                                resourceNames.AddRange(localResources.Distinct());
                        }
                        catch
                        {
                        }

                        _cachedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures)
                            .Where(culture => resourceNames.Contains(culture.EnglishName, StringComparer.OrdinalIgnoreCase))
                            .OrderBy(culture => culture.EnglishName, StringComparer.InvariantCultureIgnoreCase)
                            .Select(culture => new Language(culture))
                            .ToList();

                        return _cachedLanguages;
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                        return new List<Language> { new Language(new CultureInfo(Constants.Windows.Locale.Name.English)) };
                    }
                }
            }
        }

        public static Localization String { get; private set; }

        #endregion

        #region Methods

        private static Localization InitializeFallback()
        {
            var fallback = new Localization();
            
            // Use English format strings as fallback so placeholders work even if JSON fails to load
            var englishStrings = new Dictionary<string, string>
            {
                { nameof(Localization.About), "about" },
                { nameof(Localization.Add), "add" },
                { nameof(Localization.AlwaysOnTop), "always on top" },
                { nameof(Localization.AutoOptimization), "auto optimization" },
                { nameof(Localization.AutoOptimizationInterval), "the interval between subsequent free memory auto optimizations is {0} minutes" },
                { nameof(Localization.AutoUpdate), "auto update" },
                { nameof(Localization.Background), "background" },
                { nameof(Localization.Close), "close" },
                { nameof(Localization.CloseAfterOptimization), "close after optimization" },
                { nameof(Localization.CloseToTheNotificationArea), "close to the notification area" },
                { nameof(Localization.Collapse), "collapse" },
                { nameof(Localization.CombinedPageList), "combined page list" },
                { nameof(Localization.CreateStartMenuShortcut), "create start menu shortcut" },
                { nameof(Localization.DangerLevel), "danger level" },
                { nameof(Localization.Donate), "donate" },
                { nameof(Localization.DonationMessage), "if you find this app helpful, please consider donating. your contribution helps keep the project alive, optimized, and free for everyone." },
                { nameof(Localization.DonationTitle), "support this project" },
                { nameof(Localization.Error), "error" },
                { nameof(Localization.ErrorAdminPrivilegeRequired), "this operation requires administrator privileges ({0})" },
                { nameof(Localization.ErrorCanNotSaveLog), "cannot save the log: {0} ({1})" },
                { nameof(Localization.ErrorMemoryAreaOptimizationNotSupported), "the memory area {0} optimization is not supported on this version of the operating system" },
                { nameof(Localization.ErrorResetCommand), "reset failed: {0}" },
                { nameof(Localization.EveryHour), "every {0}h" },
                { nameof(Localization.Exit), "exit" },
                { nameof(Localization.Expand), "expand" },
                { nameof(Localization.Free), "free" },
                { nameof(Localization.GarbageCollector), "garbage collector" },
                { nameof(Localization.Help), "help" },
                { nameof(Localization.HotkeyIsInUseByOperatingSystem), "the hotkey ({0}) is in use by the operating system" },
                { nameof(Localization.Invalid), "invalid" },
                { nameof(Localization.LowMemory), "low memory" },
                { nameof(Localization.Manual), "manual" },
                { nameof(Localization.MemoryAreas), "memory areas" },
                { nameof(Localization.MemoryOptimized), "memory optimized" },
                { nameof(Localization.MemoryUsage), "memory usage" },
                { nameof(Localization.Minimize), "minimize" },
                { nameof(Localization.ModifiedFileCache), "modified file cache" },
                { nameof(Localization.ModifiedPageList), "modified page list" },
                { nameof(Localization.No), "no" },
                { nameof(Localization.OptimizationHotkey), "optimization hotkey" },
                { nameof(Localization.Optimize), "optimize" },
                { nameof(Localization.OptimizeOnMiddleMouseClick), "optimize on middle mouse click" },
                { nameof(Localization.Optimizing), "optimizing" },
                { nameof(Localization.PhysicalMemory), "physical memory" },
                { nameof(Localization.ProcessExclusionList), "processes excluded from optimization" },
                { nameof(Localization.Reason), "reason" },
                { nameof(Localization.RegistryCache), "registry cache" },
                { nameof(Localization.Remove), "remove" },
                { nameof(Localization.Reset), "reset" },
                { nameof(Localization.ResetCommand), "reset successful." },
                { nameof(Localization.ResetConfirmation), "are you sure you want to reset to the default configuration?" },
                { nameof(Localization.RunOnLowPriority), "run on low priority" },
                { nameof(Localization.RunOnStartup), "run on startup" },
                { nameof(Localization.Schedule), "schedule" },
                { nameof(Localization.Seconds), "seconds" },
                { nameof(Localization.Settings), "settings" },
                { nameof(Localization.ShowMemoryUsage), "show memory usage" },
                { nameof(Localization.ShowOptimizationNotifications), "show optimization notifications" },
                { nameof(Localization.ShowVirtualMemory), "show virtual memory" },
                { nameof(Localization.StandbyList), "standby list" },
                { nameof(Localization.StandbyListLowPriority), "standby list (low priority)" },
                { nameof(Localization.StartMinimized), "start minimized" },
                { nameof(Localization.SystemFileCache), "system file cache" },
                { nameof(Localization.Text), "text" },
                { nameof(Localization.TrayIcon), "tray icon" },
                { nameof(Localization.UpdatedToVersion), "updated to version {0}" },
                { nameof(Localization.UseTransparentBackground), "use transparent background" },
                { nameof(Localization.Used), "used" },
                { nameof(Localization.VirtualMemory), "virtual memory" },
                { nameof(Localization.WhenFreePhysicalMemoryIsBelow), "when free physical memory is below {0}%" },
                { nameof(Localization.WarningLevel), "warning level" },
                { nameof(Localization.WorkingSet), "working set" },
                { nameof(Localization.Yes), "yes" }
            };

            var props = typeof(Localization).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(string) && prop.CanWrite)
                {
                    var name = prop.Name;
                    if (englishStrings.TryGetValue(name, out var value))
                    {
                        prop.SetValue(fallback, value);
                    }
                    else
                    {
                        prop.SetValue(fallback, name);
                    }
                }
            }
            return fallback;
        }

        private static void Load(Language language)
        {
            Localization localization = null;
            var resourceName = $"{Constants.App.LocalizationResourcePath}{language.EnglishName}{Constants.App.EmbeddedResourcePathExtension}";

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            var assembliesToTry = new[] { Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly() }.Distinct();

            foreach (var assembly in assembliesToTry)
            {
                if (assembly == null) continue;

                try
                {
                    using var stream = assembly.GetManifestResourceStream(resourceName);
                    if (stream != null)
                    {
                        localization = JsonSerializer.Deserialize<Localization>(stream, options);
                        if (localization != null)
                        {
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Debug($"Failed to load embedded resource {resourceName} from {assembly.FullName}: {e.Message}");
                }
            }

            if (localization == null)
            {
                var searchDirectories = new[]
                {
                    AppDomain.CurrentDomain.BaseDirectory,
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Localization"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Themes")
                };

                foreach (var dir in searchDirectories)
                {
                    var localResource = Path.Combine(dir, $"{language.EnglishName}{Constants.App.EmbeddedResourcePathExtension}");
                    try
                    {
                        if (File.Exists(localResource))
                        {
                            using var stream = File.OpenRead(localResource);
                            localization = JsonSerializer.Deserialize<Localization>(stream, options);
                            if (localization != null)
                            {
                                break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Debug($"Failed to load local file {localResource}: {e.Message}");
                    }
                }
            }

            if (localization == null)
            {
                Logger.Warning($"Language file for {language.EnglishName} not found or invalid, using fallback");
                localization = _fallbackLocalizationLazy.Value;
            }

            var nullOrEmptyStrings = localization
                .GetType()
                .GetProperties()
                .Where(pi => pi.PropertyType == typeof(string) && string.IsNullOrWhiteSpace((string)pi.GetValue(localization)))
                .Select(pi => pi.Name)
                .ToList();

            if (nullOrEmptyStrings.Any())
            {
                Logger.Warning($"Language file for {language.EnglishName} has missing values: {string.Join(", ", nullOrEmptyStrings)}, using fallback");
                localization = _fallbackLocalizationLazy.Value;
            }

            Culture = new CultureInfo(language.Name);
            String = localization;
        }

        private static void RaiseStaticPropertyChanged(string propertyName = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}