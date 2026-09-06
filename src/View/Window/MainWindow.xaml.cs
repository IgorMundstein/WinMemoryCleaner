using System;
using System.ComponentModel;
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
        private const double ExpandedHeight = 700;
        private const double ExpandedMinHeight = 440;
        private const double ExpandedMinWidth = 680;
        private const double ExpandedWidth = 960;

        private bool? _compactModeApplied;
        private bool _expandedLayoutApplied;
        private double _lastExpandedHeight = ExpandedHeight;
        private double _lastExpandedWidth = ExpandedWidth;
        private readonly MainViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            // Check if application is shutting down
            if (Application.Current != null && Application.Current.Dispatcher != null && Application.Current.Dispatcher.HasShutdownStarted)
                return;

            InitializeComponent();

            // Set initial focus and layout once on load
            Loaded += OnMainWindowLoaded;
            Closed += OnMainWindowClosed;

            IsVisibleChanged += OnWindowVisibleChanged;

            _viewModel = DataContext as MainViewModel;

            if (_viewModel != null)
            {
                _viewModel.OnAddProcessToExclusionListCommandCompleted += OnAddProcessToExclusionListCommandCompleted;
                _viewModel.OnLanguageChangeCompleted += Refresh;
                _viewModel.PropertyChanged += OnViewModelPropertyChanged;
                _viewModel.OnNavigateUriCommandCompleted += OnNavigateUriCommandCompleted;
                _viewModel.OnOptimizeCommandCompleted += OnOptimizeCommandCompleted;
                _viewModel.OnRemoveProcessFromExclusionListCommandCompleted += () => SetFocusTo(ProcessExclusionList);
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

        /// <summary>
        /// Applies the requested compact or expanded window layout.
        /// </summary>
        /// <param name="compactMode">If set to <c>true</c>, applies compact mode.</param>
        private void ApplyCompactMode(bool compactMode)
        {
            if (_compactModeApplied.HasValue && _compactModeApplied.Value == compactMode)
                return;

            if (compactMode)
            {
                if (_expandedLayoutApplied)
                {
                    CacheExpandedSize();

                    if (WindowState == WindowState.Maximized)
                        WindowState = WindowState.Normal;
                }

                MinHeight = 0;
                MinWidth = 0;
                ResizeMode = ResizeMode.NoResize;
                SizeToContent = SizeToContent.WidthAndHeight;
                WindowResizeGrip.Visibility = Visibility.Collapsed;
            }
            else
            {
                SizeToContent = SizeToContent.Manual;
                ResizeMode = ResizeMode.CanResizeWithGrip;
                MinHeight = ExpandedMinHeight;
                MinWidth = ExpandedMinWidth;
                Width = _lastExpandedWidth;
                Height = _lastExpandedHeight;
                WindowResizeGrip.Visibility = Visibility.Visible;
                _expandedLayoutApplied = true;
            }

            _compactModeApplied = compactMode;
        }

        /// <summary>
        /// Caches a usable expanded window size for the current session.
        /// </summary>
        private void CacheExpandedSize()
        {
            if (WindowState == WindowState.Minimized)
                return;

            var bounds = WindowState == WindowState.Maximized
                    ? RestoreBounds
                    : new Rect(0, 0, ActualWidth, ActualHeight);

                if (bounds.Width <= 0 || bounds.Height <= 0)
                    return;

                _lastExpandedWidth = bounds.Width;
            _lastExpandedHeight = bounds.Height;
        }

        /// <summary>
        /// Closes the window to the notification area.
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
        /// Called when the add process to exclusion list command is completed.
        /// </summary>
        private void OnAddProcessToExclusionListCommandCompleted()
        {
            ProcessExclusionScrollViewer.ScrollToEnd();
        }

        /// <summary>
        /// Called when the close button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            if (Settings.CloseToTheNotificationArea)
            {
                SetFocusTo(Optimize);

                CloseToTheNotificationArea();

                App.ReleaseMemory();
            }
            else
                App.Shutdown();
        }

        /// <summary>
        /// Called when a combo box is right-clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void OnComboBoxMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetFocusTo(Optimize);
        }

        /// <summary>
        /// Called when the compact mode button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnCompactModeButtonClick(object sender, RoutedEventArgs e)
        {
            SetFocusTo(Optimize);
        }

        /// <summary>
        /// Called when the donate menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnDonateMenuItemClick(object sender, RoutedEventArgs e)
        {
            var window = new DonationWindow(this);

            window.ShowDialog();

            SetFocusTo(Optimize);
        }

        /// <summary>
        /// Called when the help button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
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
        /// Called when the help button receives a right mouse button down event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void OnHelpButtonPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetFocusTo(Optimize);
            e.Handled = true;
        }

        /// <summary>
        /// Called when the minimize button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            SetFocusTo(Optimize);

            WindowState = WindowState.Minimized;
        }

        /// <inheritdoc/>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            base.OnMouseLeftButtonDown(e);

            if (e.Handled || e.OriginalSource is ResizeGrip || e.Source is ResizeGrip)
                return;

            DragMove();
        }

        /// <summary>
        /// Called when the navigate URI command is completed.
        /// </summary>
        private void OnNavigateUriCommandCompleted()
        {
            SetFocusTo(Optimize);
        }

        /// <summary>
        /// Called when the optimize command is completed.
        /// </summary>
        private void OnOptimizeCommandCompleted()
        {
            if (Settings.CloseAfterOptimization)
            {
                if (Settings.CloseToTheNotificationArea)
                {
                    SetFocusTo(Optimize, force: true);
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
                
             // Wait for the button to be enabled before trying to refocus it
           // The button is disabled while IsOptimizationRunning = true
                Dispatcher.BeginInvoke(new Action(() =>
             {
              SetFocusTo(Optimize, force: true);
           }), System.Windows.Threading.DispatcherPriority.Loaded);
 }
        }

        /// <summary>
        /// Called when the processes dropdown is opened.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnProcessesDropDownOpened(object sender, EventArgs e)
        {
            _viewModel.RaisePropertyChanged(() => _viewModel.Processes);
        }

        /// <summary>
        /// Called when the reset settings to default configuration menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnResetSettingsToDefaultConfigurationClick(object sender, RoutedEventArgs e)
        {
            var window = new MessageDialog(this, Localizer.String.ResetConfirmation, Enums.Dialog.Button.No, Enums.Dialog.Button.Yes);

            var result = window.ShowDialog();

            if (result == true && _viewModel.ResetSettingsToDefaultConfigurationCommand.CanExecute(null))
                _viewModel.ResetSettingsToDefaultConfigurationCommand.Execute(null);

            SetFocusTo(Optimize);
        }

        /// <summary>
        /// Called when a tray icon checkbox is checked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnTrayIconItemCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox == null)
                return;

            SetFocusTo(checkBox);
        }

        /// <summary>
        /// Called when a tray icon checkbox is unchecked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnTrayIconItemCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox == null)
                return;

            SetFocusTo(checkBox);
        }

        /// <summary>
        /// Called when the window is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void OnWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            SetFocusTo(Optimize);
        }

        /// <summary>
        /// Called when the window is closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnMainWindowClosed(object sender, EventArgs e)
        {
            if (_viewModel != null)
                _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        /// <summary>
        /// Called when the window is loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            SetFocusTo(Optimize);
            ApplyCompactMode(_viewModel != null && _viewModel.CompactMode);
        }

        /// <summary>
        /// Called when the view model changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CompactMode")
                ApplyCompactMode(_viewModel.CompactMode);
        }

        /// <summary>
        /// Called when the window visibility changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
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
        }

        /// <summary>
        /// Sets the focus to the specified element.
        /// </summary>
        /// <param name="element">The element to focus.</param>
        /// <param name="force">If set to <c>true</c>, forces focus even if already focused.</param>
        private void SetFocusTo(object element, bool force = false)
        {
            var uiElement = element as UIElement;
            var inputElement = element as IInputElement;

            if (uiElement == null || inputElement == null)
                return;

            if (!uiElement.IsEnabled || !uiElement.IsVisible || !uiElement.Focusable)
                return;

            if (!force && uiElement.IsFocused)
                return;

            if (force && uiElement.IsFocused)
            {
                // Move focus to the window itself to ensure button loses focus
                Focus();

                // Then re-focus the element after a brief delay to allow animation state to reset
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Keyboard.Focus(uiElement);
                    FocusManager.SetFocusedElement(this, inputElement);
                }), System.Windows.Threading.DispatcherPriority.Input);
            }
            else
            {
                // Normal focus - immediate
                Keyboard.ClearFocus();
                Keyboard.Focus(uiElement);
                FocusManager.SetFocusedElement(this, inputElement);
            }
        }
    }
}
