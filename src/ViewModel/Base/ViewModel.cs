using System.ComponentModel;
using System.Windows;

namespace WinMemoryCleaner
{
    /// <summary>
    /// View Model
    /// </summary>
    internal abstract class ViewModel : ObservableObject
    {
        #region Fields

        private bool _isBusy;
        private readonly ILoadingService _loadingService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel" /> class.
        /// </summary>
        /// <param name="configurator">Configurator</param>
        /// <param name="loadingService">Loading service</param>
        /// <param name="logger">Logger</param>
        protected ViewModel(IConfigurator configurator, ILoadingService loadingService, ILogger logger)
        {
            Configurator = configurator;
            _loadingService = loadingService;
            Logger = logger;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Logger
        /// </summary>
        protected readonly IConfigurator Configurator;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                try
                {
                    _loadingService.Loading(value);
                }
                catch
                {
                    // ignored
                }

                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is in design mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in design mode; otherwise, <c>false</c>.
        /// </value>
        protected bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }

        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger Logger;

        #endregion
    }
}