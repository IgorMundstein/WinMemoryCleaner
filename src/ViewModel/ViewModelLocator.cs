using System;
using System.Threading;
using System.Windows.Forms;

namespace WinMemoryCleaner
{
    /// <summary>
    /// View Model Locator
    /// </summary>
    internal class ViewModelLocator : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
        /// </summary>
        public ViewModelLocator()
        {
            MainViewModel = new MainViewModel(App.ComputerService, App.NotificationService);
        }

        #region IDisposable

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (MainViewModel != null)
                        MainViewModel.Dispose();
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Main View Model
        /// </summary>
        public MainViewModel MainViewModel { get; private set; }
    }
}
