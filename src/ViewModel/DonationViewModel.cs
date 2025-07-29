namespace WinMemoryCleaner
{
    /// <summary>
    /// Donation View Model
    /// </summary>
    public class DonationViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DonationViewModel" /> class.
        /// </summary>
        /// <param name="notificationService">Notification service</param>
        public DonationViewModel(INotificationService notificationService)
            : base(notificationService)
        {
        }
    }
}
