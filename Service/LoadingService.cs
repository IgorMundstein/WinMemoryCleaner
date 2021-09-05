using System;
using System.Windows;
using System.Windows.Input;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Loading Service
    /// </summary>
    public class LoadingService : ILoadingService
    {
        /// <summary>
        /// Show/Hide Loading
        /// </summary>
        /// <param name="running">True (ON) / False (OFF)</param>
        public void Loading(bool running)
        {
            // Multithreading trick
            Application.Current.Dispatcher.Invoke(new Action(() => Mouse.OverrideCursor = running ? Cursors.Wait : null));
        }
    }
}
