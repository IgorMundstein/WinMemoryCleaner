namespace WinMemoryCleaner
{
    /// <summary>
    /// Operating System
    /// </summary>
    internal class OperatingSystem
    {
        /// <summary>
        /// Gets a value indicating whether current operating system has combined page list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has combined page list; otherwise, <c>false</c>.
        /// </value>
        internal bool HasCombinedPageList { get { return IsWindows8OrAbove; } }

        /// <summary>
        /// Gets a value indicating whether current operating system has modified page list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has modified page list; otherwise, <c>false</c>.
        /// </value>
        internal bool HasModifiedPageList { get { return IsWindowsVistaOrAbove; } }

        /// <summary>
        /// Gets a value indicating whether current operating system has process working set.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has process working set; otherwise, <c>false</c>.
        /// </value>
        internal bool HasProcessWorkingSet { get { return IsWindowsXp64BitOrAbove; } }

        /// <summary>
        /// Gets a value indicating whether current operating system has standby list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has standby list; otherwise, <c>false</c>.
        /// </value>
        internal bool HasStandbyList { get { return IsWindowsVistaOrAbove; } }

        /// <summary>
        /// Gets a value indicating whether current operating system has system working set.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has system working set; otherwise, <c>false</c>.
        /// </value>
        internal bool HasSystemWorkingSet { get { return IsWindowsXp64BitOrAbove; } }

        /// <summary>
        /// Determines whether the current operating system is a 64-bit operating system
        /// </summary>
        /// <value>
        ///   <c>true</c> if it is 64-bit; otherwise, <c>false</c>.
        /// </value>
        internal bool Is64Bit { get; set; }

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 8 or above.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it is Windows 8 or above; otherwise, <c>false</c>.
        /// </value>
        internal bool IsWindows8OrAbove { get; set; }

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows vista or above.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it Windows vista or above; otherwise, <c>false</c>.
        /// </value>
        internal bool IsWindowsVistaOrAbove { get; set; }

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows XP 64-Bit or above.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it Windows XP 64-Bit or above; otherwise, <c>false</c>.
        /// </value>
        internal bool IsWindowsXp64BitOrAbove { get; set; }
    }
}
