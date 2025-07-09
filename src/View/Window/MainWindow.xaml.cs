using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Main Window
    /// </summary>
    /// <seealso cref="Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow
    {
        private readonly MainViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = DataContext as MainViewModel;
            if (_viewModel != null)
            {
                _viewModel.OnNavigateUriCommandCompleted += OnNavigateUriCommandCompleted;
                _viewModel.OnOptimizeCommandCompleted += OnOptimizeCommandCompleted;
                _viewModel.OnRemoveProcessFromExclusionListCommandCompleted += SetFocusToProcessExclusionList;
            }

            // Slider
            var sliderPreviewMouseLeftButtonDownEvent = new MouseButtonEventHandler((sender, e) =>
                {
                    var slider = (Slider)sender;
                    var track = slider.Template.FindName("PART_Track", slider) as Track;

                    if (!slider.IsMoveToPointEnabled || track == null || track.Thumb == null || track.Thumb.IsMouseOver)
                        return;

                    track.Thumb.UpdateLayout();
                    track.Thumb.RaiseEvent(new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left)
                    {
                        RoutedEvent = MouseLeftButtonDownEvent,
                        Source = track.Thumb
                    });
                });

            AutoOptimizationInterval.AddHandler(PreviewMouseLeftButtonDownEvent, sliderPreviewMouseLeftButtonDownEvent, true);
            AutoOptimizationMemoryUsage.AddHandler(PreviewMouseLeftButtonDownEvent, sliderPreviewMouseLeftButtonDownEvent, true);
        }

        /// <summary>
        /// Closes to the notification area.
        /// </summary>
        private void CloseToTheNotificationArea()
        {
            if (Visibility == Visibility.Visible)
            {
                Hide();
                ShowInTaskbar = false;
            }
        }

        /// <summary>
        /// Called when [close button click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            if (Settings.CloseToTheNotificationArea)
            {
                SetFocusToOptimizationButton();

                CloseToTheNotificationArea();

                App.ReleaseMemory();
            }
            else
                App.Shutdown();
        }

        /// <summary>
        /// Called when [ComboBox mouse right button up].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void OnComboBoxMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetFocusToOptimizationButton();
        }

        /// <summary>
        /// Called when [compact mode button click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnCompactModeButtonClick(object sender, RoutedEventArgs e)
        {
            SetFocusToOptimizationButton();
        }

        /// <summary>
        /// Called when [donate menu item click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnDonateMenuItemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                IsEnabled = false;

                var donationWindow = new DonationWindow() { Owner = this };

                donationWindow.ShowDialog();

                SetFocusToOptimizationButton();
            }
            finally
            {
                IsEnabled = true;
            }
        }

        /// <summary>
        /// Called when [help button click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnHelpButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var contextMenu = button.Resources["HelpContextMenu"] as ContextMenu;

            if (contextMenu != null)
            {
                contextMenu.IsOpen = true;
                contextMenu.Placement = PlacementMode.Bottom;
                contextMenu.PlacementTarget = button;
            }
        }

        /// <summary>
        /// Called when [help button preview mouse right button down].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void OnHelpButtonPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetFocusToOptimizationButton();
            e.Handled = true;
        }

        /// <summary>
        /// Called when [minimize button click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            SetFocusToOptimizationButton();

            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.UIElement.MouseLeftButtonDown" /> routed event is raised on this element. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> that contains the event data. The event data reports that the left mouse button was pressed.</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            DragMove();
        }

        /// <summary>
        /// Called when [navigate URI command completed].
        /// </summary>
        private void OnNavigateUriCommandCompleted()
        {
            SetFocusToOptimizationButton();
        }

        /// <summary>
        /// Called when [optimize command is completed].
        /// </summary>
        private void OnOptimizeCommandCompleted()
        {
            if (Settings.CloseAfterOptimization)
            {
                if (Settings.CloseToTheNotificationArea)
                {
                    SetFocusToOptimizationButton();

                    CloseToTheNotificationArea();
                }
                else
                {
                    Thread.Sleep(1000);
                    App.Shutdown();
                }
            }
            else
            {
                _viewModel.IsBusy = false;

                SetFocusToOptimizationButton();
            }
        }

        /// <summary>
        /// Called when [processes drop down opened].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnProcessesDropDownOpened(object sender, EventArgs e)
        {
            _viewModel.RaisePropertyChanged(() => _viewModel.Processes);
        }

        /// <summary>
        /// Called when [slider preview key down].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        private void OnSliderPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var slider = (Slider)sender;

            switch (e.Key)
            {
                case Key.Left:
                case Key.Down:
                    if (slider.Value > slider.Minimum)
                        slider.Value -= 1;
                    break;

                case Key.Right:
                case Key.Up:
                    if (slider.Value < slider.Maximum)
                        slider.Value += 1;
                    break;
            }
        }

        /// <summary>
        /// Called when [window mouse down].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void OnWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            SetFocusToOptimizationButton();
        }

        /// <summary>
        /// Sets the focus to optimization button.
        /// </summary>
        private void SetFocusToOptimizationButton()
        {
            Keyboard.ClearFocus();
            FocusManager.SetFocusedElement(this, Optimize);
            Optimize.Focus();
        }

        /// <summary>
        /// Sets the focus to process exclusion list.
        /// </summary>
        private void SetFocusToProcessExclusionList()
        {
            FocusManager.SetFocusedElement(this, ProcessExclusionList);
            ProcessExclusionList.Focus();
        }
    }
}
