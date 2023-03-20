using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

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
            AutoOptimizationPercentage = 0;
            AutoUpdate = true;
            Culture = Enums.Culture.English;
            MemoryAreas = Enums.Memory.Area.ModifiedPageList | Enums.Memory.Area.ProcessesWorkingSet | Enums.Memory.Area.StandbyList | Enums.Memory.Area.SystemWorkingSet;
            MinimizeToTrayWhenClosed = false;
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
                        AutoOptimizationPercentage = Convert.ToInt32(key.GetValue(Constants.App.Registry.Name.AutoOptimizationPercentage, AutoOptimizationPercentage));
                        AutoUpdate = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.AutoUpdate, AutoUpdate));

                        Enums.Culture culture;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.Registry.Name.Culture, Culture)), out culture) && culture.IsValid())
                            Culture = culture;

                        Enums.Memory.Area memoryAreas;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.Registry.Name.MemoryAreas, MemoryAreas)), out memoryAreas) && memoryAreas.IsValid())
                        {
                            if ((memoryAreas & Enums.Memory.Area.StandbyList) != 0 && (memoryAreas & Enums.Memory.Area.StandbyListLowPriority) != 0)
                                memoryAreas &= ~Enums.Memory.Area.StandbyListLowPriority;

                            MemoryAreas = memoryAreas;
                        }

                        MinimizeToTrayWhenClosed = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.MinimizeToTrayWhenClosed, MinimizeToTrayWhenClosed));
                        RunOnStartup = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.RunOnStartup, RunOnStartup));
                        ShowOptimizationNotifications = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.ShowOptimizationNotifications, ShowOptimizationNotifications));
                        StartMinimized = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.StartMinimized, StartMinimized));
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

        internal static int AutoOptimizationPercentage { get; set; }

        internal static bool AutoUpdate { get; set; }

        internal static Enums.Culture Culture { get; set; }

        internal static Enums.Memory.Area MemoryAreas { get; set; }

        internal static bool MinimizeToTrayWhenClosed { get; set; }

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
                        key.SetValue(Constants.App.Registry.Name.AutoOptimizationPercentage, AutoOptimizationPercentage);
                        key.SetValue(Constants.App.Registry.Name.AutoUpdate, AutoUpdate ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.Culture, (int)Culture);
                        key.SetValue(Constants.App.Registry.Name.MemoryAreas, (int)MemoryAreas);
                        key.SetValue(Constants.App.Registry.Name.MinimizeToTrayWhenClosed, MinimizeToTrayWhenClosed ? 1 : 0);
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
