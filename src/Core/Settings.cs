using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace WinMemoryCleaner
{
    internal static class Settings
    {
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
            Language = Constants.App.Language;
            MemoryAreas = Enums.Memory.Area.ModifiedPageList | Enums.Memory.Area.ProcessesWorkingSet | Enums.Memory.Area.StandbyList | Enums.Memory.Area.SystemWorkingSet;
            OptimizationKey = Key.M;
            OptimizationModifiers = ModifierKeys.Control | ModifierKeys.Alt;
            ProcessExclusionList = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            RunOnStartup = false;
            ShowOptimizationNotifications = true;
            StartMinimized = false;

            // User values
            try
            {
                // Process Exclusion List
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(Constants.App.Registry.Key.ProcessExclusionList))
                {
                    if (key != null)
                    {
                        foreach (var name in key.GetValueNames())
                            ProcessExclusionList.Add(name.RemoveWhitespaces().Replace(".exe", string.Empty).ToLower());
                    }
                }

                // Settings
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(Constants.App.Registry.Key.Settings))
                {
                    if (key != null)
                    {
                        AlwaysOnTop = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.AlwaysOnTop, AlwaysOnTop));
                        AutoOptimizationInterval = Convert.ToInt32(key.GetValue(Constants.App.Registry.Name.AutoOptimizationInterval, AutoOptimizationInterval));
                        AutoOptimizationMemoryUsage = Convert.ToInt32(key.GetValue(Constants.App.Registry.Name.AutoOptimizationMemoryUsage, AutoOptimizationMemoryUsage));
                        AutoUpdate = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.AutoUpdate, AutoUpdate));
                        CloseAfterOptimization = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.CloseAfterOptimization, CloseAfterOptimization));
                        CloseToTheNotificationArea = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.CloseToTheNotificationArea, CloseToTheNotificationArea));
                        Language = Convert.ToString(key.GetValue(Constants.App.Registry.Name.Language, Language));

                        Enums.Memory.Area memoryAreas;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.Registry.Name.MemoryAreas, MemoryAreas)), out memoryAreas) && memoryAreas.IsValid())
                        {
                            if ((memoryAreas & Enums.Memory.Area.StandbyList) != 0 && (memoryAreas & Enums.Memory.Area.StandbyListLowPriority) != 0)
                                memoryAreas &= ~Enums.Memory.Area.StandbyListLowPriority;

                            MemoryAreas = memoryAreas;
                        }

                        Key optimizationKey;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.Registry.Name.OptimizationKey, OptimizationKey)), out optimizationKey))
                            OptimizationKey = optimizationKey;

                        ModifierKeys optimizationModifiers;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.Registry.Name.OptimizationModifiers, OptimizationModifiers)), out optimizationModifiers))
                            OptimizationModifiers = optimizationModifiers;

                        RunOnStartup = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.RunOnStartup, RunOnStartup));
                        ShowOptimizationNotifications = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.ShowOptimizationNotifications, ShowOptimizationNotifications));
                        StartMinimized = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.StartMinimized, StartMinimized));
                    }
                    else
                    {
                        // Smart language setter for the first run
                        var culture = CultureInfo.CurrentCulture;
                        var languages = Localizer.Languages.Keys;

                        do
                        {
                            if (languages.Contains(culture.EnglishName))
                            {
                                Language = culture.EnglishName;
                                Localizer.Language = culture.EnglishName;
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

        internal static bool AlwaysOnTop { get; set; }

        internal static int AutoOptimizationInterval { get; set; }

        internal static int AutoOptimizationMemoryUsage { get; set; }

        internal static bool AutoUpdate { get; set; }

        internal static bool CloseAfterOptimization { get; set; }

        internal static bool CloseToTheNotificationArea { get; set; }

        internal static string Language { get; set; }

        internal static Enums.Memory.Area MemoryAreas { get; set; }

        internal static Key OptimizationKey { get; set; }

        internal static ModifierKeys OptimizationModifiers { get; set; }

        internal static SortedSet<string> ProcessExclusionList { get; set; }

        internal static bool RunOnStartup { get; set; }

        internal static bool ShowOptimizationNotifications { get; set; }

        internal static bool StartMinimized { get; set; }

        #endregion

        #region Methods

        internal static void Save()
        {
            try
            {
                // Process Exclusion List
                Registry.CurrentUser.DeleteSubKey(Constants.App.Registry.Key.ProcessExclusionList, false);

                if (ProcessExclusionList.Any())
                {
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(Constants.App.Registry.Key.ProcessExclusionList))
                    {
                        if (key != null)
                        {
                            foreach (var process in ProcessExclusionList)
                                key.SetValue(process.RemoveWhitespaces().Replace(".exe", string.Empty).ToLower(), string.Empty, RegistryValueKind.String);
                        }
                    }
                }

                // Settings
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(Constants.App.Registry.Key.Settings))
                {
                    if (key != null)
                    {
                        key.SetValue(Constants.App.Registry.Name.AlwaysOnTop, AlwaysOnTop ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.AutoOptimizationInterval, AutoOptimizationInterval);
                        key.SetValue(Constants.App.Registry.Name.AutoOptimizationMemoryUsage, AutoOptimizationMemoryUsage);
                        key.SetValue(Constants.App.Registry.Name.AutoUpdate, AutoUpdate ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.CloseAfterOptimization, CloseAfterOptimization ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.CloseToTheNotificationArea, CloseToTheNotificationArea ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.Language, Language);
                        key.SetValue(Constants.App.Registry.Name.MemoryAreas, (int)MemoryAreas);
                        key.SetValue(Constants.App.Registry.Name.OptimizationKey, (int)OptimizationKey);
                        key.SetValue(Constants.App.Registry.Name.OptimizationModifiers, (int)OptimizationModifiers);
                        key.SetValue(Constants.App.Registry.Name.RunOnStartup, RunOnStartup ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.ShowOptimizationNotifications, ShowOptimizationNotifications ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.StartMinimized, StartMinimized ? 1 : 0);
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
