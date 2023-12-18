using System;

namespace WinMemoryCleaner
{
    /// <summary>
    /// INotificationService
    /// </summary>
    public interface INotificationService : IDisposable
    {
        /// <summary>
        /// Initializes
        /// </summary>
        void Initialize();

        /// <summary>
        /// Show/Hide Loading
        /// </summary>
        /// <param name="running">True (ON) / False (OFF)</param>
        void Loading(bool running);

        /// <summary>
        /// Displays a Notification
        /// </summary>
        /// <param name="message">The text</param>
        /// <param name="title">The title</param>
        /// <param name="timeout">The time period, in seconds</param>
        /// <param name="icon">The icon</param>
        void Notify(string message, string title = null, int timeout = 5, Enums.Icon.Notification icon = Enums.Icon.Notification.None);

        /// <summary>
        /// Update Info
        /// </summary>
        /// <param name="memory">The memory.</param>
        void Update(Memory memory);
    }
}
