﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
// ReSharper disable StringLiteralTypo

namespace WinMemoryCleaner
{
    /// <summary>
    /// Localization
    /// </summary>
    internal static class Localization
    {
        #region Constructor

        static Localization()
        {
            Cultures = new Dictionary<string, Enums.Culture> 
            {
                { "English", Enums.Culture.English },
                { "Español", Enums.Culture.Spanish },
                { "Português", Enums.Culture.Portuguese }
            };

            Load(Settings.Culture);
        }

        #endregion

        #region Properties

        internal static Enums.Culture? CurrentCulture { get; private set; }
        internal static readonly Dictionary<string, Enums.Culture> Cultures;

        #endregion

        #region Strings

        public static string About { get; private set; }

        public static string Completed { get; private set; }

        public static string ErrorAdminPrivilegeRequired { get; private set; }

        public static string ErrorCanNotSaveLog { get; private set; }

        public static string ErrorFeatureIsNotSupported { get; private set; }

        public static string Log { get; private set; }

        public static string MemoryCleaned { get; private set; }

        public static string MemoryCleanUp { get; private set; }

        public static string MemoryCombinedPageList { get; private set; }

        public static string MemoryLowPriorityStandbyList { get; private set; }

        public static string MemoryModifiedPageList { get; private set; }

        public static string MemoryProcessesWorkingSet { get; private set; }

        public static string MemoryStandbyList { get; private set; }

        public static string MemorySystemWorkingSet { get; private set; }

        public static string NotifyMenuExit { get; private set; }

        public static string NotifyMenuHide { get; private set; }

        public static string NotifyMenuShow { get; private set; }

        public static string SettingsMemoryAreas { get; private set; }

        #endregion

        #region Methods

        internal static void Load(Enums.Culture culture)
        {
            if (CurrentCulture == culture)
                return;

            CultureInfo cultureInfo;

            try
            {
                cultureInfo = new CultureInfo((int)culture);
            }
            catch
            {
                culture = Enums.Culture.English;
                cultureInfo = new CultureInfo((int)culture);
            }

            CurrentCulture = culture;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            switch (culture)
            {
                case Enums.Culture.English:
                    About = "About";
                    Completed = "Completed";
                    ErrorAdminPrivilegeRequired = "This operation requires administrator privileges ({0})";
                    ErrorCanNotSaveLog = "Can not save LOG: {0} (Exception: {1})";
                    ErrorFeatureIsNotSupported = "{0} clean is not supported on this operating system version";
                    Log = "Log";
                    MemoryCleaned = "Memory cleaned";
                    MemoryCleanUp = "Clean up memory";
                    MemoryCombinedPageList = "Combined Page List";
                    MemoryLowPriorityStandbyList = "Standby List (Low Priority)";
                    MemoryModifiedPageList = "Modified Page List";
                    MemoryProcessesWorkingSet = "Processes Working Set";
                    MemoryStandbyList = "Standby List";
                    MemorySystemWorkingSet = "System Working Set";
                    NotifyMenuExit = "Exit";
                    NotifyMenuHide= "Hide";
                    NotifyMenuShow = "Show";
                    SettingsMemoryAreas = "Memory Areas";
                    break;

                case Enums.Culture.Portuguese:
                    About = "Sobre";
                    Completed = "Completado";
                    ErrorAdminPrivilegeRequired = "Esta operação requer privilégios de administrador ({0})";
                    ErrorCanNotSaveLog = "Não é possível salvar o LOG: {0} (Exceção: {1})";
                    ErrorFeatureIsNotSupported = "{0} clean não é compatível com esta versão do sistema operacional";
                    Log = "Log";
                    MemoryCleaned = "Memória limpa";
                    MemoryCleanUp = "Limpar a memória";
                    MemoryCombinedPageList = "Lista de Páginas Combinadas";
                    MemoryLowPriorityStandbyList = "Lista de Espera (Baixa Prioridade)";
                    MemoryModifiedPageList = "Lista de Páginas Modificadas";
                    MemoryProcessesWorkingSet = "Conjunto de Trabalho de Processos";
                    MemoryStandbyList = "Lista de Espera";
                    MemorySystemWorkingSet = "Conjunto de Trabalho do Sistema";
                    NotifyMenuExit = "Fechar";
                    NotifyMenuHide = "Ocultar";
                    NotifyMenuShow = "Mostrar";
                    SettingsMemoryAreas = "Areas de Memória";
                    break;

                default:
                    throw new NotImplementedException(string.Format("{0} culture not implemented.", cultureInfo.DisplayName));
            }
        }

        #endregion
    }
}