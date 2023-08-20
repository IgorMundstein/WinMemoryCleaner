using System;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Enumerators
    /// </summary>
    public static class Enums
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static class Log
        {
            [Flags]
            public enum Level
            {
                Debug = 1,
                Information = 2,
                Warning = 4,
                Error = 8
            }
        }

        public static class Memory
        {
            [Flags]
            public enum Area
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

        public enum NotificationIcon
        {
            None,
            Information,
            Warning,
            Error
        }

        public enum Priority
        {
            Low,
            Normal,
            High
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}