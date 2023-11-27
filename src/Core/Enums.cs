using System;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Enumerators
    /// </summary>
    internal static class Enums
    {
        internal static class Log
        {
            [Flags]
            internal enum Level
            {
                Debug = 1,
                Information = 2,
                Warning = 4,
                Error = 8
            }
        }

        internal static class Memory
        {
            [Flags]
            internal enum Area
            {
                None = 0,
                CombinedPageList = 1,
                ModifiedPageList = 2,
                ProcessesWorkingSet = 4,
                StandbyList = 8,
                StandbyListLowPriority = 16,
                SystemWorkingSet = 32
            }
        }

        internal enum NotificationIcon
        {
            None,
            Information,
            Warning,
            Error
        }

        internal enum Priority
        {
            Low,
            Normal,
            High
        }
    }
}