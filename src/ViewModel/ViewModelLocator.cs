using System;

namespace WinMemoryCleaner
{
    /// <summary>
    /// View Model Locator
    /// </summary>
    public class ViewModelLocator : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator" /> class.
        /// </summary>
        public ViewModelLocator()
        {
            IComputerService computerService = null;
            IHotkeyService hotKeyService = null;
            INotificationService notificationService = null;

            if (!App.IsInDesignMode)
            {
                computerService = DependencyInjection.Container.Resolve<IComputerService>();
                hotKeyService = DependencyInjection.Container.Resolve<IHotkeyService>();
                notificationService = DependencyInjection.Container.Resolve<INotificationService>();
            }

            DonationViewModel = new DonationViewModel(notificationService);
            MainViewModel = new MainViewModel(computerService, hotKeyService, notificationService);
            MessageViewModel = new MessageViewModel(notificationService);
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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

        #endregion

        #region Properties

        /// <summary>
        /// Donation View Model
        /// </summary>
        public DonationViewModel DonationViewModel
        {
            get
            {
                return DependencyInjection.Container.Resolve<DonationViewModel>();
            }
            private set
            {
                DependencyInjection.Container.Register(value);
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

        /// <summary>
        /// Message View Model
        /// </summary>
        public MessageViewModel MessageViewModel
        {
            get
            {
                return DependencyInjection.Container.Resolve<MessageViewModel>();
            }
            private set
            {
                DependencyInjection.Container.Register(value);
            }
        }

        #endregion
    }
}
