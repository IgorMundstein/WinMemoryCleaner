using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

// ReSharper disable StringLiteralTypo
namespace WinMemoryCleaner
{
    /// <summary>
    /// Localization
    /// </summary>
    internal static class Localization
    {
        #region Events

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        #endregion

        #region Fields

        private static Enums.Culture _culture;

        #endregion

        #region Constructors

        static Localization()
        {
            Culture = WinMemoryCleaner.Settings.Culture;

            Languages = new Dictionary<string, Enums.Culture>
            {
                { "English", Enums.Culture.English },
                //{ "Español", Enums.Culture.Spanish },
                { "Português", Enums.Culture.Portuguese }
            };
        }

        #endregion

        #region Properties

        internal static Enums.Culture Culture
        {
            get { return _culture; }
            set
            {
                Load(value);
            }
        }

        public static Dictionary<string, Enums.Culture> Languages { get; private set; }

        #endregion

        #region Strings

        public static string Add { get; private set; }

        public static string AlwaysOnTop { get; private set; }

        public static string AutoOptimization { get; private set; }

        public static string AutoOptimizationInterval { get; private set; }

        public static string AutoUpdate { get; private set; }

        public static string Close { get; private set; }

        public static string Completed { get; private set; }

        public static string Error { get; private set; }

        public static string ErrorAdminPrivilegeRequired { get; private set; }

        public static string ErrorCanNotSaveLog { get; private set; }

        public static string ErrorFeatureIsNotSupported { get; private set; }

        public static string Every { get; private set; }

        public static string Exit { get; private set; }

        public static string Free { get; private set; }

        public static string GitHubInfo { get; private set; }

        public static string Language { get; private set; }

        public static string MemoryAreas { get; private set; }

        public static string MemoryCleanReport { get; private set; }

        public static string MemoryCombinedPageList { get; private set; }

        public static string MemoryModifiedPageList { get; private set; }

        public static string MemoryOptimized { get; private set; }

        public static string MemoryProcessesWorkingSet { get; private set; }

        public static string MemoryStandbyList { get; private set; }

        public static string MemoryStandbyListLowPriority { get; private set; }

        public static string MemorySystemWorkingSet { get; private set; }

        public static string Minimize { get; private set; }

        public static string MinimizeToTrayWhenClosed { get; private set; }

        public static string Optimize { get; private set; }

        public static string ProcessExclusionList { get; private set; }

        public static string Remove { get; private set; }

        public static string RunOnStartup { get; private set; }

        public static string Settings { get; private set; }

        public static string ShowOptimizationNotifications { get; private set; }

        public static string StartMinimized { get; private set; }

        public static string UpdatedToVersion { get; private set; }

        public static string Used { get; private set; }

        public static string WhenFreeMemoryIsBelow { get; private set; }

        #endregion

        #region Methods

        private static void Load(Enums.Culture culture)
        {
            if (_culture == culture)
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

            _culture = culture;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            switch (culture)
            {
                case Enums.Culture.English:
                    Add = "Add";
                    AlwaysOnTop = "Always on top";
                    AutoOptimization = "Auto optimization";
                    AutoOptimizationInterval = "The interval between this auto optimization is 5 minutes";
                    AutoUpdate = "Auto update";
                    Close = "Close";
                    Completed = "Completed";
                    Error = "Error";
                    ErrorAdminPrivilegeRequired = "This operation requires administrator privileges ({0})";
                    ErrorCanNotSaveLog = "Can not save LOG: {0} (Exception: {1})";
                    ErrorFeatureIsNotSupported = "{0} clean is not supported on this operating system version";
                    Every = "Every";
                    Exit = "Exit";
                    Free = "Free";
                    GitHubInfo = string.Format("Documentation, Issues and Suggestions.{0}{0}© {1}{0}Developed by {2}", Environment.NewLine, Constants.App.License, Constants.App.Author.Name);
                    Language = "Language";
                    MemoryAreas = "Memory areas";
                    MemoryCleanReport = "Memory clean report";
                    MemoryCombinedPageList = "Combined Page List";
                    MemoryModifiedPageList = "Modified Page List";
                    MemoryOptimized = "Memory optimized";
                    MemoryProcessesWorkingSet = "Processes Working Set";
                    MemoryStandbyList = "Standby List";
                    MemoryStandbyListLowPriority = "Standby List (Low Priority)";
                    MemorySystemWorkingSet = "System Working Set";
                    Minimize = "Minimize";
                    MinimizeToTrayWhenClosed = "Minimize to tray when closed";
                    Optimize = "Optimize";
                    ProcessExclusionList = "Processes excluded from optimization";
                    Remove = "Remove";
                    RunOnStartup = "Run on startup";
                    Settings = "Settings";
                    ShowOptimizationNotifications = "Show optimization notifications";
                    StartMinimized = "Start minimized";
                    UpdatedToVersion = "Updated to version {0}";
                    Used = "Used";
                    WhenFreeMemoryIsBelow = "When free memory is below";
                    break;

                case Enums.Culture.Portuguese:
                    Add = "Adicionar";
                    AlwaysOnTop = "Sempre na frente";
                    AutoOptimization = "Otimização automática";
                    AutoOptimizationInterval = "O intervalo entre esta otimização automática é de 5 minutos";
                    AutoUpdate = "Atualização automática";
                    Close = "Fechar";
                    Completed = "Completado";
                    Error = "Erro";
                    ErrorAdminPrivilegeRequired = "Esta operação requer privilégios de administrador ({0})";
                    ErrorCanNotSaveLog = "Não é possível salvar o LOG: {0} (Exceção: {1})";
                    ErrorFeatureIsNotSupported = "{0} clean não é compatível com esta versão do sistema operacional";
                    Every = "A cada";
                    Exit = "Sair";
                    Free = "Livre";
                    GitHubInfo = string.Format("Documentação, problemas e sugestões.{0}{0}© {1}{0}Desenvolvido por {2}", Environment.NewLine, Constants.App.License, Constants.App.Author.Name);
                    Language = "Idioma";
                    MemoryAreas = "Areas de memória";
                    MemoryCleanReport = "Relatório de limpeza de memória";
                    MemoryCombinedPageList = "Lista de Páginas Combinadas";
                    MemoryModifiedPageList = "Lista de Páginas Modificadas";
                    MemoryOptimized = "Memória optimizada";
                    MemoryProcessesWorkingSet = "Conjunto de Trabalho de Processos";
                    MemoryStandbyList = "Lista de Espera";
                    MemoryStandbyListLowPriority = "Lista de Espera (Baixa Prioridade)";
                    MemorySystemWorkingSet = "Conjunto de Trabalho do Sistema";
                    Minimize = "Minimizar";
                    MinimizeToTrayWhenClosed = "Minimizar para a bandeja quando fechado";
                    Optimize = "Otimizar";
                    ProcessExclusionList = "Processos excluídos da otimização";
                    Remove = "Remover";
                    RunOnStartup = "Executar na inicialização";
                    Settings = "Configurações";
                    ShowOptimizationNotifications = "Mostrar notificações de otimização";
                    StartMinimized = "Iniciar minimizado";
                    UpdatedToVersion = "Atualizado para versão {0}";
                    Used = "Usada";
                    WhenFreeMemoryIsBelow = "Quando a memória livre estiver abaixo de";
                    break;

                default:
                    throw new NotImplementedException(string.Format("{0} culture not implemented.", cultureInfo.DisplayName));
            }

            RaiseStaticPropertyChanged(string.Empty);
        }

        private static void RaiseStaticPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
