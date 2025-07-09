using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Donation Window
    /// </summary>
    public partial class DonationWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DonationWindow" /> class.
        /// </summary>
        public DonationWindow()
        {
            InitializeComponent();
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnHyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            using (Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true })) { }
            e.Handled = true;
        }

        private void OnHyperlinkRequestNavigate(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                var url = button.Tag as string;

                if (url != null)
                    using (Process.Start(new ProcessStartInfo(url) { UseShellExecute = true })) { }
            }
        }
    }
}
