using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WinMemoryCleaner
{
    /// <summary>
    /// View
    /// </summary>
    /// <seealso cref="Window" />
    public abstract class View : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="View" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="isDialog">if set to <c>true</c> [is dialog].</param>
        protected View(Window owner = null, bool isDialog = false)
            : base()
        {
            Closed += OnWindowClosed;
            Loaded += OnWindowLoaded;

            IsDialog = isDialog;
            Owner = owner;

            if (IsDialog && Owner != null)
                WindowStartupLocation = WindowStartupLocation.CenterOwner;

            // If you need custom centering, use dispatcher (no need for Visibility.Hidden)
            Dispatcher.BeginInvoke(new Action(CenterOverOwner), System.Windows.Threading.DispatcherPriority.Loaded);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is dialog.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is dialog; otherwise, <c>false</c>.
        /// </value>
        public bool IsDialog { get; private set; }

        /// <summary>
        /// Centers over owner.
        /// </summary>
        protected void CenterOverOwner()
        {
            if (Owner != null)
            {
                Left = Owner.Left + (Owner.Width - Width) / 2;
                Top = Owner.Top + (Owner.Height - Height) / 2;
            }
        }

        /// <summary>
        /// Called when [window closed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void OnWindowClosed(object sender, System.EventArgs e)
        {
            if (Owner != null)
                Owner.IsEnabled = true;
        }

        /// <summary>
        /// Called when [window loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        protected void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (Owner != null)
                Owner.IsEnabled = false;
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        protected void Refresh()
        {
            // Refresh main window
            InvalidateVisual();
            InvalidateArrange();
            InvalidateMeasure();
            UpdateLayout();

            // Refresh owned windows (non-dialogs)
            foreach (var window in OwnedWindows.Cast<View>().Where(w => w != null && !w.IsDialog))
            {
                window.InvalidateVisual();
                window.InvalidateArrange();
                window.InvalidateMeasure();
                window.UpdateLayout();
            }

            // Reopen context menus if needed
            var helpButton = FindName("HelpButton") as Button;

            if (helpButton != null)
            {
                var contextMenu = helpButton.Resources["HelpContextMenu"] as ContextMenu;

                if (contextMenu != null && contextMenu.IsOpen)
                {
                    contextMenu.IsOpen = false;
                    contextMenu.IsOpen = true;
                }
            }
        }
    }
}
