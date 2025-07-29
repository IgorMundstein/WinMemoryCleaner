using System.Windows;

namespace WinMemoryCleaner
{
    /// <summary>
    /// MessageDialog
    /// </summary>
    /// <seealso cref="View" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MessageDialog : View
    {
        private readonly MessageViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDialog"/> class.
        /// </summary>
        public MessageDialog(Window owner = null)
            : base(owner, true)
        {
            InitializeComponent();

            _viewModel = DataContext as MessageViewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDialog" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        /// <param name="rightButton">The right button.</param>
        /// <param name="leftButton">The left button.</param>
        /// <param name="showInTaskbar">if set to <c>true</c> [show in taskbar].</param>
        /// <param name="windowStartupLocation">The window startup location.</param>
        public MessageDialog(Window owner, string message, Enums.Dialog.Button rightButton, Enums.Dialog.Button leftButton = Enums.Dialog.Button.None, bool showInTaskbar = false, WindowStartupLocation windowStartupLocation = WindowStartupLocation.CenterOwner)
            : this(owner)
        {
            _viewModel.LeftButton = leftButton.GetKeyValue();
            _viewModel.Message = message;
            _viewModel.RightButton = rightButton.GetKeyValue();
            
            ShowInTaskbar = showInTaskbar;
            WindowStartupLocation = windowStartupLocation;

            InitializeComponent();
        }

        private void OnLeftButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = _viewModel.LeftButton.Value;

            Close();
        }

        private void OnRightButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = _viewModel.RightButton.Value;

            Close();
        }
    }
}
