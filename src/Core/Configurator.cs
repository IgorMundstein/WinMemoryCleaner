using System;
using Microsoft.Win32;

namespace WinMemoryCleaner
{
    internal class Configurator : IConfigurator
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Constructor

        internal Configurator(ILogger logger)
        {
            _logger = logger;

            Initialize();
        }

        #endregion

        #region Properties

        public Config Config { get; private set; }

        #endregion

        #region Methods

        private void Initialize()
        {
            try
            {
                // Default values
                Config = new Config
                {
                    Culture = Enums.Culture.English,
                    MemoryAreas = Enums.Memory.Area.CombinedPageList | Enums.Memory.Area.ModifiedPageList | Enums.Memory.Area.ProcessesWorkingSet | Enums.Memory.Area.StandbyList | Enums.Memory.Area.SystemWorkingSet
                };

                // User values
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(Constants.App.RegistryKey.Name))
                {
                    if (key != null)
                    {
                        Enums.Culture culture;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.RegistryKey.Culture, Config.Culture)), out culture) && culture.IsValid())
                            Config.Culture = culture;

                        Enums.Memory.Area memoryAreas;

                        if (Enum.TryParse(Convert.ToString(key.GetValue(Constants.App.RegistryKey.MemoryAreas, Config.MemoryAreas)), out memoryAreas) && memoryAreas.IsValid())
                        {
                            if ((memoryAreas & Enums.Memory.Area.StandbyList) != 0 && (memoryAreas & Enums.Memory.Area.StandbyListLowPriority) != 0)
                                memoryAreas &= ~Enums.Memory.Area.StandbyListLowPriority;

                            Config.MemoryAreas = memoryAreas;
                        }
                    }

                    Save();
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        public void Save()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(Constants.App.RegistryKey.Name))
                {
                    if (key != null)
                    {
                        key.SetValue(Constants.App.RegistryKey.Culture, (int)Config.Culture);
                        key.SetValue(Constants.App.RegistryKey.MemoryAreas, (int)Config.MemoryAreas);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        #endregion
    }
}
