using Microsoft.Win32;
using System;

namespace WinMemoryCleaner
{
    /// <summary>  
    /// Migrator  
    /// </summary>  
    public static class Migrator
    {
        /// <summary>  
        /// Check and run migration if needed  
        /// </summary>  
        public static void Run()
        {
            // 2.9+  
            if (App.Version >= new Version(2, 9))
            {
                MigrateSettingsFromCurrentUserToLocalMachine();
                RemoveStartupRegistry();
            }
            // 3.0+
            if (App.Version >= new Version(3, 0))
            {
                RemoveRegistryPath();
            }
        }

        #region Classes  

        private static class V28
        {
            internal class Enums
            {
                [Flags]
                internal enum MemoryAreas
                {
                    None = 0,
                    CombinedPageList = 1,
                    ModifiedPageList = 2,
                    ProcessesWorkingSet = 4,
                    StandbyList = 8,
                    StandbyListLowPriority = 16,
                    SystemWorkingSet = 32
                }
            }

            internal class Registry
            {
                internal const string MemoryAreas = "MemoryAreas";
                internal const string TrayIcon = "TrayIcon";
                internal const string TrayIconShowMemoryUsage = "TrayIconShowMemoryUsage";
            }
        }

        private static class V30
        {
            internal class Registry
            {
                internal const string Path = "Path";
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// From version 2.9, settings were moved from the current user key to the local machine to support a new feature that enables the app to run in service mode.
        /// </summary>
        private static void MigrateSettingsFromCurrentUserToLocalMachine()
        {
            try
            {
                // Settings  
                using (var userKey = Registry.CurrentUser.OpenSubKey(Constants.App.Registry.Key.Settings))
                {
                    if (userKey == null)
                        return;

                    using (var machineKey = Registry.LocalMachine.CreateSubKey(Constants.App.Registry.Key.Settings))
                    {
                        foreach (var name in userKey.GetValueNames())
                        {
                            var value = userKey.GetValue(name);

                            switch (name)
                            {
                                case V28.Registry.MemoryAreas:
                                    var newMemoryAreas = Enums.Memory.Areas.None;
                                    V28.Enums.MemoryAreas oldMemoryAreas;

                                    if (Enum.TryParse(value.ToString(), out oldMemoryAreas) && oldMemoryAreas.IsValid())
                                    {
                                        if ((oldMemoryAreas & V28.Enums.MemoryAreas.StandbyList) != 0 && (oldMemoryAreas & V28.Enums.MemoryAreas.StandbyListLowPriority) != 0)
                                            oldMemoryAreas &= ~V28.Enums.MemoryAreas.StandbyListLowPriority;
                                    }

                                    if ((oldMemoryAreas & V28.Enums.MemoryAreas.CombinedPageList) != 0)
                                        newMemoryAreas |= Enums.Memory.Areas.CombinedPageList;

                                    if ((oldMemoryAreas & V28.Enums.MemoryAreas.ModifiedPageList) != 0)
                                        newMemoryAreas |= Enums.Memory.Areas.ModifiedPageList;

                                    if ((oldMemoryAreas & V28.Enums.MemoryAreas.ProcessesWorkingSet) != 0)
                                        newMemoryAreas |= Enums.Memory.Areas.WorkingSet;

                                    if ((oldMemoryAreas & V28.Enums.MemoryAreas.StandbyList) != 0)
                                        newMemoryAreas |= Enums.Memory.Areas.StandbyList;

                                    if ((oldMemoryAreas & V28.Enums.MemoryAreas.StandbyListLowPriority) != 0)
                                        newMemoryAreas |= Enums.Memory.Areas.StandbyListLowPriority;

                                    if ((oldMemoryAreas & V28.Enums.MemoryAreas.SystemWorkingSet) != 0)
                                        newMemoryAreas |= Enums.Memory.Areas.SystemFileCache;

                                    newMemoryAreas |= Enums.Memory.Areas.RegistryCache | Enums.Memory.Areas.ModifiedFileCache;

                                    machineKey.SetValue(name, (int)newMemoryAreas);
                                    break;

                                case V28.Registry.TrayIcon:
                                    machineKey.SetValue(V28.Registry.TrayIconShowMemoryUsage, value);
                                    break;

                                default:
                                    machineKey.SetValue(name, value);
                                    break;
                            }
                        }
                    }
                }

                // Process Exclusion List
                using (var userKey = Registry.CurrentUser.OpenSubKey(Constants.App.Registry.Key.ProcessExclusionList))
                {
                    if (userKey != null)
                    {
                        Registry.LocalMachine.DeleteSubKey(Constants.App.Registry.Key.ProcessExclusionList, false);

                        using (var machineKey = Registry.LocalMachine.CreateSubKey(Constants.App.Registry.Key.ProcessExclusionList))
                        {
                            foreach (var name in userKey.GetValueNames())
                                machineKey.SetValue(name, userKey.GetValue(name, string.Empty));
                        }
                    }
                }

                Registry.CurrentUser.DeleteSubKey(Constants.App.Registry.Key.ProcessExclusionList, false);
                Registry.CurrentUser.DeleteSubKey(Constants.App.Registry.Key.Settings, false);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// Removes the registry path.
        /// </summary>
        private static void RemoveRegistryPath()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(Constants.App.Registry.Key.Settings, true))
                {
                    if (key != null)
                        key.DeleteValue(V30.Registry.Path, false);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// Removes the startup registry to avoid UAC warnings. Use a scheduled task instead to run at startup.
        /// </summary>
        private static void RemoveStartupRegistry()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    if (key != null)
                        key.DeleteValue(Constants.App.Title, false);
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
