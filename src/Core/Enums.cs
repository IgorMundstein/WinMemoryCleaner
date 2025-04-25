using System;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner
{
    /// <summary>
    /// Enumerators
    /// </summary>
    public static class Enums
    {
        public static class Icon
        {
            public enum Notification
            {
                None,
                Information,
                Warning,
                Error
            }

            public enum Tray
            {
                Image,
                MemoryUsage
            }
        }

        public static class Log
        {
            [Flags]
            public enum Levels
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
            public enum Areas
            {
                None = 0,
                CombinedPageList = 1,
                ModifiedPageList = 2,
                ProcessesWorkingSet = 4,
                StandbyList = 8,
                StandbyListLowPriority = 16,
                SystemWorkingSet = 32
            }

            public static class Optimization
            {
                public enum Reason
                {
                    LowMemory,
                    Manual,
                    Schedule
                }
            }

            public enum Unit { B, KB, MB, GB, TB, PB, EB, ZB, YB }
        }

        public enum Priority
        {
            Low,
            Normal,
            High
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member