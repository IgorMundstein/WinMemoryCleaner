using System;
using System.ComponentModel;
using System.Windows;

namespace WinMemoryCleaner
{
    /// <summary>
    /// View Model Locator
    /// </summary>
    internal class ViewModelLocator : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator" /> class.
        /// </summary>
        public ViewModelLocator()
        {
            IComputerService computerService = null;
            INotificationService notificationService = null;

            if (!IsInDesignMode)
            {
                computerService = DependencyInjection.Container.Resolve<IComputerService>();
                notificationService = DependencyInjection.Container.Resolve<INotificationService>();
            }

            MainViewModel = new MainViewModel(computerService, notificationService);
        }

        #endregion

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

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is in design mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in design mode; otherwise, <c>false</c>.
        /// </value>
        private bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }

        /// <summary>
        /// Main View Model
        /// </summary>
        public MainViewModel MainViewModel
        {
            get
            {
                return DependencyInjection.Container.Resolve<MainViewModel>();
            }
            private set
            {
                DependencyInjection.Container.Register(value);
            }
        }

        #endregion
    }
}
