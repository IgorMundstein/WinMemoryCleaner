using System.Windows;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Donation Window
    /// </summary>
    public partial class DonationWindow : View
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DonationWindow" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="showInTaskbar">if set to <c>true</c> [show in taskbar].</param>
        /// <param name="windowStartupLocation">The window startup location.</param>
        public DonationWindow(Window owner, bool showInTaskbar = false, WindowStartupLocation windowStartupLocation = WindowStartupLocation.CenterOwner)
            : base(owner)
        {
            InitializeComponent();

            ShowInTaskbar = showInTaskbar;
            WindowStartupLocation = windowStartupLocation;
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}