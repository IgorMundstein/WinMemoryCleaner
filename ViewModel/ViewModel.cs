namespace WinMemoryCleaner
{
    /// <summary>
    /// View Model Base
    /// </summary>
    public abstract class ViewModel : ObservableObject
    {
        #region Fields

        private bool _isLoading;
        private readonly ILoadingService _loadingService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        /// <param name="loadingService">The loading service.</param>
        protected ViewModel(ILoadingService loadingService)
        {
            _loadingService = loadingService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ViewModel"/> is isloading.
        /// </summary>
        /// <value>
        ///   <c>true</c> if isloading; otherwise, <c>false</c>.
        /// </value>
        public bool Isloading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Show/Hide Loading
        /// </summary>
        /// <param name="on">True (ON) / False (OFF)</param>
        protected void Loading(bool on)
        {
            Isloading = on;

            _loadingService.Loading(on);
        }

        #endregion
    }
}