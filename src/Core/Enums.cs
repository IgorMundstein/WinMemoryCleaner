using System;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Enumerators
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// Culture
        /// </summary>
        public enum Culture
        {
            /// <summary>
            /// English
            /// </summary>
            English = 9,
            /// <summary>
            /// Portuguese
            /// </summary>
            Portuguese = 22,
            /// <summary>
            /// Spanish
            /// </summary>
            Spanish = 10
        }

        /// <summary>
        /// Log
        /// </summary>
        public static class Log
        {
            /// <summary>
            /// Level
            /// </summary>
            [Flags]
            public enum Level
            {
                /// <summary>
                /// Debug
                /// </summary>
                Debug = 1,
                /// <summary>
                /// Information
                /// </summary>
                Information = 2,
                /// <summary>
                /// Warning
                /// </summary>
                Warning = 4,
                /// <summary>
                /// Error
                /// </summary>
                Error = 8
            }
        }

        /// <summary>
        /// Memory
        /// </summary>
        public static class Memory
        {
            /// <summary>
            /// Area
            /// </summary>
            [Flags]
            public enum Area
            {
                /// <summary>
                /// None
                /// </summary>
                None = 0,
                /// <summary>
                /// Combined Page List
                /// </summary>
                CombinedPageList = 1,
                /// <summary>
                /// Modified Page List
                /// </summary>
                ModifiedPageList = 2,
                /// <summary>
                /// Processes Working Set
                /// </summary>
                ProcessesWorkingSet = 4,
                /// <summary>
                /// Standby List
                /// </summary>
                StandbyList = 8,
                /// <summary>
                /// Standby List (Low Priority)
                /// </summary>
                StandbyListLowPriority = 16,
                /// <summary>
                /// System Working Set
                /// </summary>
                SystemWorkingSet = 32
            }
        }

        /// <summary>
        /// Notification Icon
        /// </summary>
        public enum NotificationIcon
        {
            /// <summary>
            /// None
            /// </summary>
            None,
            /// <summary>
            /// Information
            /// </summary>
            Information,
            /// <summary>
            /// Warning
            /// </summary>
            Warning,
            /// <summary>
            /// Error
            /// </summary>
            Error
        }
    }
}
