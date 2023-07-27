namespace WinMemoryCleaner
{
    internal interface INotificationService
    {
        void Initialize();

        void Loading(bool value);

        void Notify(string message, string title = null, int timeout = 5, Enums.NotificationIcon icon = Enums.NotificationIcon.None);

        void UpdateInfo(Memory memory);
    }
}
