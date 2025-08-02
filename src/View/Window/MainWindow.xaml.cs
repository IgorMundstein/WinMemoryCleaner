using System;
using System.Linq;
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
    public partial class MainWindow : View
    {
        private readonly MainViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
            IsVisibleChanged += OnWindowVisibleChanged;

            _viewModel = DataContext as MainViewModel;

            if (_viewModel != null)
            {
                _viewModel.OnAddProcessToExclusionListCommandCompleted += OnAddProcessToExclusionListCommandCompleted;
                _viewModel.OnLanguageChangeCompleted += Refresh;
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

            AutoOptimizationIntervalSlider.AddHandler(PreviewMouseLeftButtonDownEvent, sliderPreviewMouseLeftButtonDownEvent, true);
            AutoOptimizationMemoryUsageSlider.AddHandler(PreviewMouseLeftButtonDownEvent, sliderPreviewMouseLeftButtonDownEvent, true);
            FontSizeSlider.AddHandler(PreviewMouseLeftButtonDownEvent, sliderPreviewMouseLeftButtonDownEvent, true);
        }

        private void CloseToTheNotificationArea()
        {
            if (Visibility == Visibility.Visible)
            {
                Hide();
                ShowInTaskbar = false;
            }
        }

        private void OnAddProcessToExclusionListCommandCompleted()
        {
            ProcessExclusionScrollViewer.ScrollToEnd();
        }

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

        private void OnComboBoxMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetFocusToOptimizationButton();
        }

        private void OnCompactModeButtonClick(object sender, RoutedEventArgs e)
        {
            SetFocusToOptimizationButton();
        }

        private void OnDonateMenuItemClick(object sender, RoutedEventArgs e)
        {
            var window = new DonationWindow(this);

            window.ShowDialog();

            SetFocusToOptimizationButton();
        }

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

        private void OnHelpButtonPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetFocusToOptimizationButton();
            e.Handled = true;
        }

        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            SetFocusToOptimizationButton();

            WindowState = WindowState.Minimized;
        }

        /// <inheritdoc/>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            DragMove();
        }

        private void OnNavigateUriCommandCompleted()
        {
            SetFocusToOptimizationButton();
        }

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

        private void OnProcessesDropDownOpened(object sender, EventArgs e)
        {
            _viewModel.RaisePropertyChanged(() => _viewModel.Processes);
        }

        private void OnResetSettingsToDefaultConfigurationClick(object sender, RoutedEventArgs e)
        {
            var window = new MessageDialog(this, Localizer.String.ResetConfirmation, Enums.Dialog.Button.No, Enums.Dialog.Button.Yes);

            var result = window.ShowDialog();

            if (result == true && _viewModel.ResetSettingsToDefaultConfigurationCommand.CanExecute(null))
                _viewModel.ResetSettingsToDefaultConfigurationCommand.Execute(null);

            SetFocusToOptimizationButton();
        }

        private void OnWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            SetFocusToOptimizationButton();
        }

        private void OnWindowVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            foreach (var window in OwnedWindows.Cast<View>().Where(window => window != null && !window.IsDialog))
            {
                if (window.IsLoaded)
                {
                    if (IsVisible)
                        window.Visibility = Visibility.Visible;
                    else
                        window.Visibility = Visibility.Hidden;
                }
            }

            SetFocusToOptimizationButton();
        }

        private void SetFocusToOptimizationButton()
        {
            if (Optimize == null)
                return;

            Keyboard.ClearFocus();
            FocusManager.SetFocusedElement(this, Optimize);
            Optimize.Focus();
        }

        private void SetFocusToProcessExclusionList()
        {
            FocusManager.SetFocusedElement(this, ProcessExclusionList);
            ProcessExclusionList.Focus();
        }
    }
}
