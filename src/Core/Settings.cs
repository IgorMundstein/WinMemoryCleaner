using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            // Default values
            AlwaysOnTop = false;
            AutoOptimizationInterval = 0;
            AutoOptimizationMemoryUsage = 0;
            AutoUpdate = true;
            CloseAfterOptimization = false;
            CloseToTheNotificationArea = false;
            CompactMode = false;
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
            TrayIcon = Enums.Icon.Tray.Image;
            UseHotkey = true;

            // User values
            try
            {
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
                        AlwaysOnTop = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.AlwaysOnTop, AlwaysOnTop), _culture);
                        AutoOptimizationInterval = Convert.ToInt32(key.GetValue(Constants.App.Registry.Name.AutoOptimizationInterval, AutoOptimizationInterval), _culture);
                        AutoOptimizationMemoryUsage = Convert.ToInt32(key.GetValue(Constants.App.Registry.Name.AutoOptimizationMemoryUsage, AutoOptimizationMemoryUsage), _culture);
                        AutoUpdate = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.AutoUpdate, AutoUpdate), _culture);
                        CloseAfterOptimization = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.CloseAfterOptimization, CloseAfterOptimization), _culture);
                        CloseToTheNotificationArea = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.CloseToTheNotificationArea, CloseToTheNotificationArea), _culture);
                        CompactMode = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.CompactMode, CompactMode), _culture);
                        Language = Convert.ToString(key.GetValue(Constants.App.Registry.Name.Language, Language), _culture);

                        Enums.Memory.Areas memoryAreas;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.Registry.Name.MemoryAreas, MemoryAreas), _culture), out memoryAreas) && memoryAreas.IsValid())
                        {
                            if ((memoryAreas & Enums.Memory.Areas.StandbyList) != 0 && (memoryAreas & Enums.Memory.Areas.StandbyListLowPriority) != 0)
                                memoryAreas &= ~Enums.Memory.Areas.StandbyListLowPriority;

                            MemoryAreas = memoryAreas;
                        }

                        Key optimizationKey;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.Registry.Name.OptimizationKey, OptimizationKey), _culture), out optimizationKey) && optimizationKey.IsValid())
                            OptimizationKey = optimizationKey;

                        ModifierKeys optimizationModifiers;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.Registry.Name.OptimizationModifiers, OptimizationModifiers), _culture), out optimizationModifiers) && optimizationModifiers.IsValid())
                            OptimizationModifiers = optimizationModifiers;

                        Enums.Priority runOnPriority;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.Registry.Name.RunOnPriority, RunOnPriority), _culture), out runOnPriority) && runOnPriority.IsValid())
                            RunOnPriority = runOnPriority;

                        RunOnStartup = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.RunOnStartup, RunOnStartup), _culture);
                        ShowOptimizationNotifications = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.ShowOptimizationNotifications, ShowOptimizationNotifications), _culture);
                        ShowVirtualMemory = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.ShowVirtualMemory, ShowVirtualMemory), _culture);
                        StartMinimized = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.StartMinimized, StartMinimized), _culture);

                        Enums.Icon.Tray trayIcon;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.Registry.Name.TrayIcon, TrayIcon), _culture), out trayIcon) && trayIcon.IsValid())
                            TrayIcon = trayIcon;

                        UseHotkey = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.UseHotkey, UseHotkey), _culture);
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
            finally
            {
                Save();
            }
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
        
        public static Enums.Icon.Tray TrayIcon { get; set; }

        public static bool UseHotkey { get; set; }

        #endregion

        #region Methods

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
                        key.SetValue(Constants.App.Registry.Name.AlwaysOnTop, AlwaysOnTop ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.AutoOptimizationInterval, AutoOptimizationInterval);
                        key.SetValue(Constants.App.Registry.Name.AutoOptimizationMemoryUsage, AutoOptimizationMemoryUsage);
                        key.SetValue(Constants.App.Registry.Name.AutoUpdate, AutoUpdate ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.CloseAfterOptimization, CloseAfterOptimization ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.CloseToTheNotificationArea, CloseToTheNotificationArea ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.CompactMode, CompactMode ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.Language, Language);
                        key.SetValue(Constants.App.Registry.Name.MemoryAreas, (int)MemoryAreas);
                        key.SetValue(Constants.App.Registry.Name.OptimizationKey, (int)OptimizationKey);
                        key.SetValue(Constants.App.Registry.Name.OptimizationModifiers, (int)OptimizationModifiers);
                        key.SetValue(Constants.App.Registry.Name.RunOnPriority, (int)RunOnPriority);
                        key.SetValue(Constants.App.Registry.Name.RunOnStartup, RunOnStartup ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.ShowOptimizationNotifications, ShowOptimizationNotifications ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.ShowVirtualMemory, ShowVirtualMemory ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.StartMinimized, StartMinimized ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.TrayIcon, (int)TrayIcon);
                        key.SetValue(Constants.App.Registry.Name.UseHotkey, UseHotkey ? 1 : 0);
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