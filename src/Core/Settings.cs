using System;
using Microsoft.Win32;

namespace WinMemoryCleaner
{
    internal static class Settings
    {
        #region Constructor

        static Settings()
        {
            // Default values
            AlwaysOnTop = false;
            AutoCleanInterval = 0;
            AutoCleanPercentage = 0;
            AutoUpdate = true;
            Culture = Enums.Culture.English;
            MemoryAreas = Enums.Memory.Area.ModifiedPageList | Enums.Memory.Area.ProcessesWorkingSet | Enums.Memory.Area.StandbyList | Enums.Memory.Area.SystemWorkingSet;
            MinimizeToTrayWhenClosed = true;
            RunOnStartup = false;
            ShowNotifications = true;
            StartMinimized = false;

            // User values
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(Constants.App.Registry.Key.Settings))
                {
                    if (key != null)
                    {
                        AlwaysOnTop = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.AlwaysOnTop, AlwaysOnTop));
                        AutoCleanInterval = Convert.ToInt32(key.GetValue(Constants.App.Registry.Name.AutoCleanInterval, AutoCleanInterval));
                        AutoCleanPercentage = Convert.ToInt32(key.GetValue(Constants.App.Registry.Name.AutoCleanPercentage, AutoCleanPercentage));
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
                        ShowNotifications = Convert.ToBoolean(key.GetValue(Constants.App.Registry.Name.ShowNotifications, ShowNotifications));
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

        internal static int AutoCleanInterval { get; set; }

        internal static int AutoCleanPercentage { get; set; }

        internal static bool AutoUpdate { get; set; }

        internal static Enums.Culture Culture { get; set; }

        internal static Enums.Memory.Area MemoryAreas { get; set; }

        internal static bool MinimizeToTrayWhenClosed { get; set; }

        internal static bool RunOnStartup { get; set; }

        internal static bool ShowNotifications { get; set; }

        internal static bool StartMinimized { get; set; }

        #endregion

        #region Methods

        internal static void Save()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(Constants.App.Registry.Key.Settings))
                {
                    if (key != null)
                    {
                        key.SetValue(Constants.App.Registry.Name.AlwaysOnTop, AlwaysOnTop ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.AutoCleanInterval, AutoCleanInterval);
                        key.SetValue(Constants.App.Registry.Name.AutoCleanPercentage, AutoCleanPercentage);
                        key.SetValue(Constants.App.Registry.Name.AutoUpdate, AutoUpdate ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.Culture, (int)Culture);
                        key.SetValue(Constants.App.Registry.Name.MemoryAreas, (int)MemoryAreas);
                        key.SetValue(Constants.App.Registry.Name.MinimizeToTrayWhenClosed, MinimizeToTrayWhenClosed ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.RunOnStartup, RunOnStartup ? 1 : 0);
                        key.SetValue(Constants.App.Registry.Name.ShowNotifications, ShowNotifications ? 1 : 0);
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
