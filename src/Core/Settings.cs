using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner
{
    /// <summary>
    /// Application settings persisted in the Windows registry (HKLM) so they are shared
    /// between the desktop app and the Windows Service (which runs as LOCAL SYSTEM).
    /// </summary>
    public static class Settings
    {
        private static readonly CultureInfo _culture = new CultureInfo(Constants.Windows.Locale.Name.English);
        private static readonly object _syncLock = new object();
        private static bool _isLoaded;

        #region Constructors

        static Settings()
        {
            // Default values only - don't load from registry yet to avoid circular dependency
            LoadDefaults();
            _isLoaded = true;
        }

        #endregion

        #region Properties

        public static bool AlwaysOnTop { get; set; }

        public static int AutoOptimizationInterval { get; set; }

        public static int AutoOptimizationMemoryUsage { get; set; }

        public static bool AutoUpdate { get; set; }

        public static bool CloseAfterOptimization { get; set; }

        public static bool CloseToTheNotificationArea { get; set; }

        public static bool CompactMode { get; set; }

        public static bool CreateStartMenuShortcut { get; set; }

        public static double FontSize { get; set; }

        public static string Language { get; set; }

        public static Enums.Memory.Areas MemoryAreas { get; set; }

        public static Key OptimizationKey { get; set; }

        public static ModifierKeys OptimizationModifiers { get; set; }

        public static ConcurrentDictionary<string, byte> ProcessExclusionList { get; private set; }

        public static bool IsProcessExcluded(string processName)
        {
            if (string.IsNullOrEmpty(processName))
                return false;
            return ProcessExclusionList.ContainsKey(processName);
        }

        public static bool TryAddProcessExclusion(string processName)
        {
            if (string.IsNullOrEmpty(processName))
                return false;
            return ProcessExclusionList.TryAdd(processName, 0);
        }

        public static bool TryRemoveProcessExclusion(string processName)
        {
            if (string.IsNullOrEmpty(processName))
                return false;
            return ProcessExclusionList.TryRemove(processName, out _);
        }

        public static Enums.Priority RunOnPriority { get; set; }

        public static bool RunOnStartup { get; set; }

        public static bool ShowOptimizationNotifications { get; set; }

        public static bool ShowVirtualMemory { get; set; }

        public static bool StartMinimized { get; set; }

        public static Brush TrayIconBackgroundColor { get; set; }

        public static Brush TrayIconDangerColor { get; set; }

        public static byte TrayIconDangerLevel { get; set; }

        public static bool TrayIconOptimizeOnMiddleMouseClick { get; set; }

        public static Brush TrayIconOptimizingColor { get; set; }

        public static bool TrayIconShowMemoryUsage { get; set; }

        public static Brush TrayIconTextColor { get; set; }

        public static bool TrayIconUseTransparentBackground { get; set; }

        public static Brush TrayIconWarningColor { get; set; }

        public static byte TrayIconWarningLevel { get; set; }

        public static bool UseHotkey { get; set; }

        #endregion

        #region Methods

        private static void LoadDefaults()
        {
            AlwaysOnTop = false;
            AutoOptimizationInterval = 0;
            AutoOptimizationMemoryUsage = 0;
            AutoUpdate = true;
            CloseAfterOptimization = false;
            CloseToTheNotificationArea = false;
            CompactMode = false;
            CreateStartMenuShortcut = true;
            FontSize = 14;
            Language = Constants.Windows.Locale.Name.English;
            MemoryAreas = Enums.Memory.Areas.CombinedPageList | Enums.Memory.Areas.ModifiedFileCache | Enums.Memory.Areas.ModifiedPageList | Enums.Memory.Areas.RegistryCache | Enums.Memory.Areas.StandbyList | Enums.Memory.Areas.SystemFileCache | Enums.Memory.Areas.WorkingSet;
            OptimizationKey = Key.M;
            OptimizationModifiers = ModifierKeys.Control | ModifierKeys.Shift;
            ProcessExclusionList = new ConcurrentDictionary<string, byte>(StringComparer.OrdinalIgnoreCase);
            RunOnPriority = Enums.Priority.Low;
            RunOnStartup = false;
            ShowOptimizationNotifications = true;
            ShowVirtualMemory = false;
            StartMinimized = false;
            TrayIconBackgroundColor = Brushes.DarkGreen;
            TrayIconDangerColor = Brushes.DarkRed;
            TrayIconDangerLevel = 90;
            TrayIconOptimizeOnMiddleMouseClick = false;
            TrayIconOptimizingColor = Brushes.DimGray;
            TrayIconShowMemoryUsage = false;
            TrayIconTextColor = Brushes.White;
            TrayIconUseTransparentBackground = false;
            TrayIconWarningColor = Brushes.DarkGoldenrod;
            TrayIconWarningLevel = 80;
            UseHotkey = false;
        }

        public static void Load()
        {
            lock (_syncLock)
            {
                if (!_isLoaded)
                {
                    LoadDefaults();
                    _isLoaded = true;
                }

                try
                {
                    // Process Exclusion List
                    using (var key = Registry.LocalMachine.OpenSubKey(Constants.App.Registry.Key.ProcessExclusionList))
                    {
                        if (key != null)
                        {
                            ProcessExclusionList.Clear();
                            foreach (var name in key.GetValueNames())
                            {
                                var cleanName = name.RemoveWhitespaces().Replace(".exe", string.Empty).ToLower(_culture);
                                ProcessExclusionList.TryAdd(cleanName, 0);
                            }
                        }
                    }

                    // Settings
                    using (var key = Registry.LocalMachine.OpenSubKey(Constants.App.Registry.Key.Settings))
                    {
                        if (key != null)
                        {
                            AlwaysOnTop = Convert.ToBoolean(key.GetValue(nameof(AlwaysOnTop), AlwaysOnTop), _culture);
                            AutoOptimizationInterval = Convert.ToInt32(key.GetValue(nameof(AutoOptimizationInterval), AutoOptimizationInterval), _culture);
                            AutoOptimizationMemoryUsage = Convert.ToInt32(key.GetValue(nameof(AutoOptimizationMemoryUsage), AutoOptimizationMemoryUsage), _culture);
                            AutoUpdate = Convert.ToBoolean(key.GetValue(nameof(AutoUpdate), AutoUpdate), _culture);
                            CloseAfterOptimization = Convert.ToBoolean(key.GetValue(nameof(CloseAfterOptimization), CloseAfterOptimization), _culture);
                            CloseToTheNotificationArea = Convert.ToBoolean(key.GetValue(nameof(CloseToTheNotificationArea), CloseToTheNotificationArea), _culture);
                            CompactMode = Convert.ToBoolean(key.GetValue(nameof(CompactMode), CompactMode), _culture);
                            CreateStartMenuShortcut = Convert.ToBoolean(key.GetValue(nameof(CreateStartMenuShortcut), CreateStartMenuShortcut), _culture);
                            FontSize = Convert.ToDouble(key.GetValue(nameof(FontSize), FontSize), _culture);
                            Language = Convert.ToString(key.GetValue(nameof(Language), Language), CultureInfo.InvariantCulture);

                            if (Enum.TryParse(Convert.ToString(key.GetValue(nameof(MemoryAreas), MemoryAreas), _culture), out Enums.Memory.Areas memoryAreas) && memoryAreas.IsValid())
                            {
                                if ((memoryAreas & Enums.Memory.Areas.StandbyList) != 0 && (memoryAreas & Enums.Memory.Areas.StandbyListLowPriority) != 0)
                                    memoryAreas &= ~Enums.Memory.Areas.StandbyListLowPriority;

                                MemoryAreas = memoryAreas;
                            }

                            if (Enum.TryParse(Convert.ToString(key.GetValue(nameof(OptimizationKey), OptimizationKey), _culture), out Key optimizationKey) && optimizationKey.IsValid())
                                OptimizationKey = optimizationKey;

                            if (Enum.TryParse(Convert.ToString(key.GetValue(nameof(OptimizationModifiers), OptimizationModifiers), _culture), out ModifierKeys optimizationModifiers) && optimizationModifiers.IsValid())
                                OptimizationModifiers = optimizationModifiers;

                            if (Enum.TryParse(Convert.ToString(key.GetValue(nameof(RunOnPriority), RunOnPriority), _culture), out Enums.Priority runOnPriority) && runOnPriority.IsValid())
                                RunOnPriority = runOnPriority;

                            RunOnStartup = Convert.ToBoolean(key.GetValue(nameof(RunOnStartup), RunOnStartup), _culture);
                            ShowOptimizationNotifications = Convert.ToBoolean(key.GetValue(nameof(ShowOptimizationNotifications), ShowOptimizationNotifications), _culture);
                            ShowVirtualMemory = Convert.ToBoolean(key.GetValue(nameof(ShowVirtualMemory), ShowVirtualMemory), _culture);
                            StartMinimized = Convert.ToBoolean(key.GetValue(nameof(StartMinimized), StartMinimized), _culture);
                            TrayIconBackgroundColor = Convert.ToString(key.GetValue(nameof(TrayIconBackgroundColor), TrayIconBackgroundColor), _culture).ToBrush(TrayIconBackgroundColor);
                            TrayIconDangerColor = Convert.ToString(key.GetValue(nameof(TrayIconDangerColor), TrayIconDangerColor), _culture).ToBrush(TrayIconDangerColor);
                            TrayIconDangerLevel = Convert.ToByte(key.GetValue(nameof(TrayIconDangerLevel), TrayIconDangerLevel), _culture);
                            TrayIconOptimizeOnMiddleMouseClick = Convert.ToBoolean(key.GetValue(nameof(TrayIconOptimizeOnMiddleMouseClick), TrayIconOptimizeOnMiddleMouseClick), _culture);
                            TrayIconOptimizingColor = Convert.ToString(key.GetValue(nameof(TrayIconOptimizingColor), TrayIconOptimizingColor), _culture).ToBrush(TrayIconOptimizingColor);
                            TrayIconShowMemoryUsage = Convert.ToBoolean(key.GetValue(nameof(TrayIconShowMemoryUsage), TrayIconShowMemoryUsage), _culture);
                            TrayIconTextColor = Convert.ToString(key.GetValue(nameof(TrayIconTextColor), TrayIconTextColor), _culture).ToBrush(TrayIconTextColor);
                            TrayIconUseTransparentBackground = Convert.ToBoolean(key.GetValue(nameof(TrayIconUseTransparentBackground), TrayIconUseTransparentBackground), _culture);
                            TrayIconWarningColor = Convert.ToString(key.GetValue(nameof(TrayIconWarningColor), TrayIconWarningColor), _culture).ToBrush(TrayIconWarningColor);
                            TrayIconWarningLevel = Convert.ToByte(key.GetValue(nameof(TrayIconWarningLevel), TrayIconWarningLevel), _culture);
                            UseHotkey = Convert.ToBoolean(key.GetValue(nameof(UseHotkey), UseHotkey), _culture);
                        }
                        else
                        {
                            // Smart language setter for the first run
                            var culture = CultureInfo.CurrentCulture;
                            var languages = Localizer.Languages.Select(language => language.Name).ToList();

                            do
                            {
                                if (languages.Contains(culture.Name, StringComparer.OrdinalIgnoreCase))
                                {
                                    Localizer.Language = new Language(culture);
                                    Language = culture.Name;
                                    break;
                                }

                                culture = culture.Parent;
                            }
                            while (culture.LCID != CultureInfo.InvariantCulture.LCID);
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        public static void Reset(bool keepLanguage = false)
        {
            var language = Language;

            lock (_syncLock)
            {
                LoadDefaults();
                if (keepLanguage)
                    Language = language;

                Save();
            }
        }

        public static void Save()
        {
            lock (_syncLock)
            {
                try
                {
                    // Process Exclusion List
                    Registry.LocalMachine.DeleteSubKey(Constants.App.Registry.Key.ProcessExclusionList, false);

                    if (!ProcessExclusionList.IsEmpty)
                    {
                        using (var key = Registry.LocalMachine.CreateSubKey(Constants.App.Registry.Key.ProcessExclusionList))
                        {
                            if (key != null)
                            {
                                foreach (var process in ProcessExclusionList.Keys)
                                    key.SetValue(process.RemoveWhitespaces().Replace(".exe", string.Empty).ToLower(_culture), string.Empty, RegistryValueKind.String);
                            }
                        }
                    }

                    // Settings
                    using (var key = Registry.LocalMachine.CreateSubKey(Constants.App.Registry.Key.Settings))
                    {
                        if (key != null)
                        {
                            key.SetValue(nameof(AlwaysOnTop), AlwaysOnTop ? 1 : 0);
                            key.SetValue(nameof(AutoOptimizationInterval), AutoOptimizationInterval);
                            key.SetValue(nameof(AutoOptimizationMemoryUsage), AutoOptimizationMemoryUsage);
                            key.SetValue(nameof(AutoUpdate), AutoUpdate ? 1 : 0);
                            key.SetValue(nameof(CloseAfterOptimization), CloseAfterOptimization ? 1 : 0);
                            key.SetValue(nameof(CloseToTheNotificationArea), CloseToTheNotificationArea ? 1 : 0);
                            key.SetValue(nameof(CompactMode), CompactMode ? 1 : 0);
                            key.SetValue(nameof(CreateStartMenuShortcut), CreateStartMenuShortcut ? 1 : 0);
                            key.SetValue(nameof(FontSize), FontSize);
                            key.SetValue(nameof(Language), Language);
                            key.SetValue(nameof(MemoryAreas), (int)MemoryAreas);
                            key.SetValue(nameof(OptimizationKey), (int)OptimizationKey);
                            key.SetValue(nameof(OptimizationModifiers), (int)OptimizationModifiers);
                            key.SetValue(nameof(RunOnPriority), (int)RunOnPriority);
                            key.SetValue(nameof(RunOnStartup), RunOnStartup ? 1 : 0);
                            key.SetValue(nameof(ShowOptimizationNotifications), ShowOptimizationNotifications ? 1 : 0);
                            key.SetValue(nameof(ShowVirtualMemory), ShowVirtualMemory ? 1 : 0);
                            key.SetValue(nameof(StartMinimized), StartMinimized ? 1 : 0);
                            key.SetValue(nameof(TrayIconBackgroundColor), TrayIconBackgroundColor.GetHex(true));
                            key.SetValue(nameof(TrayIconDangerColor), TrayIconDangerColor.GetHex(true));
                            key.SetValue(nameof(TrayIconDangerLevel), TrayIconDangerLevel);
                            key.SetValue(nameof(TrayIconOptimizeOnMiddleMouseClick), TrayIconOptimizeOnMiddleMouseClick ? 1 : 0);
                            key.SetValue(nameof(TrayIconOptimizingColor), TrayIconOptimizingColor.GetHex(true));
                            key.SetValue(nameof(TrayIconShowMemoryUsage), TrayIconShowMemoryUsage ? 1 : 0);
                            key.SetValue(nameof(TrayIconTextColor), TrayIconTextColor.GetHex(true));
                            key.SetValue(nameof(TrayIconUseTransparentBackground), TrayIconUseTransparentBackground ? 1 : 0);
                            key.SetValue(nameof(TrayIconWarningColor), TrayIconWarningColor.GetHex(true));
                            key.SetValue(nameof(TrayIconWarningLevel), TrayIconWarningLevel);
                            key.SetValue(nameof(UseHotkey), UseHotkey ? 1 : 0);
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member