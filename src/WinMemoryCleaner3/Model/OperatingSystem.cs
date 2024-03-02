namespace WinMemoryCleaner
{
    /// <summary>
    /// Operating System
    /// </summary>
    public class OperatingSystem
    {
        /// <summary>
        /// Gets a value indicating whether current operating system has combined page list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has combined page list; otherwise, <c>false</c>.
        /// </value>
        public bool HasCombinedPageList
        { get { return IsWindows8OrGreater; } }

        /// <summary>
        /// Gets a value indicating whether current operating system has a hotkey manager.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has a hotkey manager; otherwise, <c>false</c>.
        /// </value>
        public bool HasHotKeyManager
        { get { return IsWindowsVistaOrGreater; } }

        /// <summary>
        /// Gets a value indicating whether current operating system has modified page list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has modified page list; otherwise, <c>false</c>.
        /// </value>
        public bool HasModifiedPageList
        { get { return IsWindowsVistaOrGreater; } }

        /// <summary>
        /// Gets a value indicating whether current operating system has processes working set.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has processes working set; otherwise, <c>false</c>.
        /// </value>
        public bool HasProcessesWorkingSet
        { get { return IsWindowsXpOrGreater; } }

        /// <summary>
        /// Gets a value indicating whether current operating system has standby list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has standby list; otherwise, <c>false</c>.
        /// </value>
        public bool HasStandbyList
        { get { return IsWindowsVistaOrGreater; } }

        /// <summary>
        /// Gets a value indicating whether current operating system has system working set.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has system working set; otherwise, <c>false</c>.
        /// </value>
        public bool HasSystemWorkingSet
        { get { return IsWindowsXpOrGreater; } }

        /// <summary>
        /// Determines whether the current operating system is a 64-bit operating system
        /// </summary>
        /// <value>
        ///   <c>true</c> if it is 64-bit; otherwise, <c>false</c>.
        /// </value>
        public bool Is64Bit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current operating system is Windows 8 or greater.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it is Windows 8 or greater; otherwise, <c>false</c>.
        /// </value>
        public bool IsWindows8OrGreater { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current operating system is Windows Vista or greater.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it is Windows Vista or greater; otherwise, <c>false</c>.
        /// </value>
        public bool IsWindowsVistaOrGreater { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current operating system is Windows XP or greater.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it is Windows XP or greater; otherwise, <c>false</c>.
        /// </value>
        public bool IsWindowsXpOrGreater { get; set; }
    }
}