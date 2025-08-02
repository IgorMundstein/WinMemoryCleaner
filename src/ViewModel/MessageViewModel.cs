using System.Collections.Generic;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Message View Model
    /// </summary>
    public class MessageViewModel : ViewModel
    {
        private KeyValuePair<string, bool?> _leftButton;
        private string _message;
        private KeyValuePair<string, bool?> _rightButton;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageViewModel" /> class.
        /// </summary>
        /// <param name="notificationService">Notification service</param>
        public MessageViewModel(INotificationService notificationService)
            : base(notificationService)
        {
            LeftButton = new KeyValuePair<string, bool?>("Left", false);
            RightButton = new KeyValuePair<string, bool?>("Right", true);
            Message = "Message";
        }

        /// <summary>
        /// Gets the left button.
        /// </summary>
        /// <value>
        /// The left button.
        /// </value>
        public KeyValuePair<string, bool?> LeftButton
        {
            get { return _leftButton; }
            set
            {
                _leftButton = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the right button.
        /// </summary>
        /// <value>
        /// The right button.
        /// </value>
        public KeyValuePair<string, bool?> RightButton
        {
            get { return _rightButton; }
            set
            {
                _rightButton = value;
                RaisePropertyChanged();
            }
        }
    }
}
