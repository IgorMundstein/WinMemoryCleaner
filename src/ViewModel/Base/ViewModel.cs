﻿using System.ComponentModel;
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel" /> class.
        /// </summary>
        /// <param name="notificationService">Notification service</param>
        protected ViewModel(INotificationService notificationService)
        {
            NotificationService = notificationService;
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
        /// Gets the notification service.
        /// </summary>
        /// <value>
        /// The notification service.
        /// </value>
        protected INotificationService NotificationService { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Displays a Notification
        /// </summary>
        /// <param name="message">The text</param>
        /// <param name="title">The title</param>
        /// <param name="timeout">The time period, in seconds</param>
        /// <param name="icon">The icon</param>
        protected void Notify(string message, string title = null, int timeout = 5, Enums.NotificationIcon icon = Enums.NotificationIcon.None)
        {
            NotificationService.Notify(message, title, timeout, icon);
        }

        #endregion
    }
}