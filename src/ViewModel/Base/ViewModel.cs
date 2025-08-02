using System;
using System.Windows.Input;

namespace WinMemoryCleaner
{
    /// <summary>
    /// View Model
    /// </summary>
    public abstract class ViewModel : ObservableObject
    {
        #region Fields

        private bool _isBusy;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel" /> class.
        /// </summary>
        /// <param name="notificationService">Notification service</param>
        protected ViewModel(INotificationService notificationService)
        {
            // Services
            NotificationService = notificationService;

            // Commands
            NavigateUriCommand = new RelayCommand<Uri>(Navigate);
        }

        #endregion

        #region Properties

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
                    NotificationService.Loading(value);
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
        /// Gets the notification service.
        /// </summary>
        /// <value>
        /// The notification service.
        /// </value>
        protected INotificationService NotificationService { get; private set; }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the navigate URI command.
        /// </summary>
        /// <value>
        /// The navigate URI command.
        /// </value>
        public ICommand NavigateUriCommand { get; private set; }

        #endregion

        #region Actions

        /// <summary>
        /// Occurs when [on navigate URI command completed].
        /// </summary>
        public event Action OnNavigateUriCommandCompleted;

        #endregion

        #region Methods

        /// <summary>  
        /// Navigates the specified URI.  
        /// </summary>  
        /// <param name="uri">The URI.</param>  
        protected void Navigate(Uri uri)
        {
            App.Navigate(uri);

            if (OnNavigateUriCommandCompleted != null)
            {
                System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    OnNavigateUriCommandCompleted();
                });
            }
        }

        /// <summary>
        /// Displays a Notification
        /// </summary>
        /// <param name="message">The text</param>
        /// <param name="title">The title</param>
        /// <param name="timeout">The time period, in seconds</param>
        /// <param name="icon">The icon</param>
        protected void Notify(string message, string title = null, int timeout = 5, Enums.Icon.Notification icon = Enums.Icon.Notification.None)
        {
            NotificationService.Notify(message, title, timeout, icon);
        }

        #endregion
    }
}