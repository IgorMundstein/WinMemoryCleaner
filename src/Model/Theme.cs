namespace WinMemoryCleaner
{
    /// <summary>
    /// Theme
    /// </summary>
    /// <seealso cref="ITheme" />
    public class Theme : ITheme
    {
        /// <summary>
        /// Gets the accent color.
        /// </summary>
        public string Accent { get; set; }

        /// <summary>
        /// Gets the memory bar indicator background color.
        /// </summary>
        public string MemoryBarIndicatorBackground { get; set; }

        /// <summary>
        /// Gets the memory bar track background color.
        /// </summary>
        public string MemoryBarTrackBackground { get; set; }

        /// <summary>
        /// Gets the primary background color.
        /// </summary>
        public string PrimaryBackground { get; set; }

        /// <summary>
        /// Gets the primary border color.
        /// </summary>
        public string PrimaryBorder { get; set; }

        /// <summary>
        /// Gets the secondary background color.
        /// </summary>
        public string SecondaryBackground { get; set; }

        /// <summary>
        /// Gets the secondary border color.
        /// </summary>
        public string SecondaryBorder { get; set; }

        /// <summary>
        /// Gets the secondary disabled color.
        /// </summary>
        public string SecondaryDisabled { get; set; }

        /// <summary>
        /// Gets the secondary foreground color.
        /// </summary>
        public string SecondaryForeground { get; set; }
    }
}
