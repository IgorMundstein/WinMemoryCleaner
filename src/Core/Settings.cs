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
            Culture = Enums.Culture.English;
            MemoryAreas = Enums.Memory.Area.ModifiedPageList | Enums.Memory.Area.ProcessesWorkingSet | Enums.Memory.Area.StandbyList | Enums.Memory.Area.SystemWorkingSet;

            // User values
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(Constants.App.RegistryKey.Name))
                {
                    if (key != null)
                    {
                        Enums.Culture culture;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.RegistryKey.Culture, Culture)), out culture) && culture.IsValid())
                            Culture = culture;

                        Enums.Memory.Area memoryAreas;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.RegistryKey.MemoryAreas, MemoryAreas)), out memoryAreas) && memoryAreas.IsValid())
                        {
                            if ((memoryAreas & Enums.Memory.Area.StandbyList) != 0 && (memoryAreas & Enums.Memory.Area.StandbyListLowPriority) != 0)
                                memoryAreas &= ~Enums.Memory.Area.StandbyListLowPriority;

                            MemoryAreas = memoryAreas;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            // Save
            try
            {
                Save();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        #endregion

        #region Properties

        internal static Enums.Culture Culture { get; set; }

        internal static Enums.Memory.Area MemoryAreas { get; set; }

        #endregion

        #region Methods

        internal static void Save()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(Constants.App.RegistryKey.Name))
                {
                    if (key != null)
                    {
                        key.SetValue(Constants.App.RegistryKey.Culture, (int)Culture);
                        key.SetValue(Constants.App.RegistryKey.MemoryAreas, (int)MemoryAreas);
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
