using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner
{
    public static class Settings
    {
        private static readonly CultureInfo _culture = new CultureInfo(Constants.Windows.Locale.Name.English);

        #region Constructors

        static Settings()
        {
            Load();
            Save();
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

        public static SortedSet<string> ProcessExclusionList { get; private set; }

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

        private static void Load(bool loadUserValues = true)
        {
            // Default values
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
            ProcessExclusionList = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
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

            // User values
            try
            {
                if (!loadUserValues)
                    return;

                // Process Exclusion List
                using (var key = Registry.LocalMachine.OpenSubKey(Constants.App.Registry.Key.ProcessExclusionList))
                {
                    if (key != null)
                    {
                        foreach (var name in key.GetValueNames())
                            ProcessExclusionList.Add(name.RemoveWhitespaces().Replace(".exe", string.Empty).ToLower(_culture));
                    }
                }

                // Settings
                using (var key = Registry.LocalMachine.OpenSubKey(Constants.App.Registry.Key.Settings))
                {
                    if (key != null)
                    {
                        AlwaysOnTop = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => AlwaysOnTop), AlwaysOnTop), _culture);
                        AutoOptimizationInterval = Convert.ToInt32(key.GetValue(Helper.NameOf(() => AutoOptimizationInterval), AutoOptimizationInterval), _culture);
                        AutoOptimizationMemoryUsage = Convert.ToInt32(key.GetValue(Helper.NameOf(() => AutoOptimizationMemoryUsage), AutoOptimizationMemoryUsage), _culture);
                        AutoUpdate = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => AutoUpdate), AutoUpdate), _culture);
                        CloseAfterOptimization = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => CloseAfterOptimization), CloseAfterOptimization), _culture);
                        CloseToTheNotificationArea = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => CloseToTheNotificationArea), CloseToTheNotificationArea), _culture);
                        CompactMode = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => CompactMode), CompactMode), _culture);
                        CreateStartMenuShortcut = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => CreateStartMenuShortcut), CreateStartMenuShortcut), _culture);
                        FontSize = Convert.ToDouble(key.GetValue(Helper.NameOf(() => FontSize), FontSize), _culture);
                        Language = Convert.ToString(key.GetValue(Helper.NameOf(() => Language), Language), CultureInfo.InvariantCulture);

                        Enums.Memory.Areas memoryAreas;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Helper.NameOf(() => MemoryAreas), MemoryAreas), _culture), out memoryAreas) && memoryAreas.IsValid())
                        {
                            if ((memoryAreas & Enums.Memory.Areas.StandbyList) != 0 && (memoryAreas & Enums.Memory.Areas.StandbyListLowPriority) != 0)
                                memoryAreas &= ~Enums.Memory.Areas.StandbyListLowPriority;

                            MemoryAreas = memoryAreas;
                        }

                        Key optimizationKey;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Helper.NameOf(() => OptimizationKey), OptimizationKey), _culture), out optimizationKey) && optimizationKey.IsValid())
                            OptimizationKey = optimizationKey;

                        ModifierKeys optimizationModifiers;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Helper.NameOf(() => OptimizationModifiers), OptimizationModifiers), _culture), out optimizationModifiers) && optimizationModifiers.IsValid())
                            OptimizationModifiers = optimizationModifiers;

                        Enums.Priority runOnPriority;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Helper.NameOf(() => RunOnPriority), RunOnPriority), _culture), out runOnPriority) && runOnPriority.IsValid())
                            RunOnPriority = runOnPriority;

                        RunOnStartup = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => RunOnStartup), RunOnStartup), _culture);
                        ShowOptimizationNotifications = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => ShowOptimizationNotifications), ShowOptimizationNotifications), _culture);
                        ShowVirtualMemory = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => ShowVirtualMemory), ShowVirtualMemory), _culture);
                        StartMinimized = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => StartMinimized), StartMinimized), _culture);
                        TrayIconBackgroundColor = Convert.ToString(key.GetValue(Helper.NameOf(() => TrayIconBackgroundColor), TrayIconBackgroundColor), _culture).ToBrush(TrayIconBackgroundColor);
                        TrayIconDangerColor = Convert.ToString(key.GetValue(Helper.NameOf(() => TrayIconDangerColor), TrayIconDangerColor), _culture).ToBrush(TrayIconDangerColor);
                        TrayIconDangerLevel = Convert.ToByte(key.GetValue(Helper.NameOf(() => TrayIconDangerLevel), TrayIconDangerLevel), _culture);
                        TrayIconOptimizeOnMiddleMouseClick = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => TrayIconOptimizeOnMiddleMouseClick), TrayIconOptimizeOnMiddleMouseClick), _culture);
                        TrayIconOptimizingColor = Convert.ToString(key.GetValue(Helper.NameOf(() => TrayIconOptimizingColor), TrayIconOptimizingColor), _culture).ToBrush(TrayIconOptimizingColor);
                        TrayIconShowMemoryUsage = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => TrayIconShowMemoryUsage), TrayIconShowMemoryUsage), _culture);
                        TrayIconTextColor = Convert.ToString(key.GetValue(Helper.NameOf(() => TrayIconTextColor), TrayIconTextColor), _culture).ToBrush(TrayIconTextColor);
                        TrayIconUseTransparentBackground = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => TrayIconUseTransparentBackground), TrayIconUseTransparentBackground), _culture);
                        TrayIconWarningColor = Convert.ToString(key.GetValue(Helper.NameOf(() => TrayIconWarningColor), TrayIconWarningColor), _culture).ToBrush(TrayIconWarningColor);
                        TrayIconWarningLevel = Convert.ToByte(key.GetValue(Helper.NameOf(() => TrayIconWarningLevel), TrayIconWarningLevel), _culture);
                        UseHotkey = Convert.ToBoolean(key.GetValue(Helper.NameOf(() => UseHotkey), UseHotkey), _culture);
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

        public static void Reset(bool keepLanguage = false)
        {
            var language = Language;

            Load(false);

            if (keepLanguage)
                Language = language;

            Save();
        }

        public static void Save()
        {
            try
            {
                // Process Exclusion List
                Registry.LocalMachine.DeleteSubKey(Constants.App.Registry.Key.ProcessExclusionList, false);

                if (ProcessExclusionList.Any())
                {
                    using (var key = Registry.LocalMachine.CreateSubKey(Constants.App.Registry.Key.ProcessExclusionList))
                    {
                        if (key != null)
                        {
                            foreach (var process in ProcessExclusionList)
                                key.SetValue(process.RemoveWhitespaces().Replace(".exe", string.Empty).ToLower(_culture), string.Empty, RegistryValueKind.String);
                        }
                    }
                }

                // Settings
                using (var key = Registry.LocalMachine.CreateSubKey(Constants.App.Registry.Key.Settings))
                {
                    if (key != null)
                    {
                        key.SetValue(Helper.NameOf(() => AlwaysOnTop), AlwaysOnTop ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => AutoOptimizationInterval), AutoOptimizationInterval);
                        key.SetValue(Helper.NameOf(() => AutoOptimizationMemoryUsage), AutoOptimizationMemoryUsage);
                        key.SetValue(Helper.NameOf(() => AutoUpdate), AutoUpdate ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => CloseAfterOptimization), CloseAfterOptimization ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => CloseToTheNotificationArea), CloseToTheNotificationArea ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => CompactMode), CompactMode ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => CreateStartMenuShortcut), CreateStartMenuShortcut ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => FontSize), FontSize);
                        key.SetValue(Helper.NameOf(() => Language), Language);
                        key.SetValue(Helper.NameOf(() => MemoryAreas), (int)MemoryAreas);
                        key.SetValue(Helper.NameOf(() => OptimizationKey), (int)OptimizationKey);
                        key.SetValue(Helper.NameOf(() => OptimizationModifiers), (int)OptimizationModifiers);
                        key.SetValue(Helper.NameOf(() => RunOnPriority), (int)RunOnPriority);
                        key.SetValue(Helper.NameOf(() => RunOnStartup), RunOnStartup ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => ShowOptimizationNotifications), ShowOptimizationNotifications ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => ShowVirtualMemory), ShowVirtualMemory ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => StartMinimized), StartMinimized ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => TrayIconBackgroundColor), TrayIconBackgroundColor.GetHex(true));
                        key.SetValue(Helper.NameOf(() => TrayIconDangerColor), TrayIconDangerColor.GetHex(true));
                        key.SetValue(Helper.NameOf(() => TrayIconDangerLevel), TrayIconDangerLevel);
                        key.SetValue(Helper.NameOf(() => TrayIconOptimizeOnMiddleMouseClick), TrayIconOptimizeOnMiddleMouseClick ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => TrayIconOptimizingColor), TrayIconOptimizingColor.GetHex(true));
                        key.SetValue(Helper.NameOf(() => TrayIconShowMemoryUsage), TrayIconShowMemoryUsage ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => TrayIconTextColor), TrayIconTextColor.GetHex(true));
                        key.SetValue(Helper.NameOf(() => TrayIconUseTransparentBackground), TrayIconUseTransparentBackground ? 1 : 0);
                        key.SetValue(Helper.NameOf(() => TrayIconWarningColor), TrayIconWarningColor.GetHex(true));
                        key.SetValue(Helper.NameOf(() => TrayIconWarningLevel), TrayIconWarningLevel);
                        key.SetValue(Helper.NameOf(() => UseHotkey), UseHotkey ? 1 : 0);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member