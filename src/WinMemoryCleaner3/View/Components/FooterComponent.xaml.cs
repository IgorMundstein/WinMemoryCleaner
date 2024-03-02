using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WinMemoryCleaner3.View.Components
{
    /// <summary>
    /// FooterComponent.xaml 的交互逻辑
    /// </summary>
    public partial class FooterComponent : UserControl
    {
        public FooterComponent()
        {
            InitializeComponent();
        }

        private void OnHyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            string url = e.Uri.AbsoluteUri;
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }
}