namespace WinMemoryCleaner
{
    /// <summary>
    /// ITheme
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// Gets the accent color.
        /// </summary>
        string Accent { get; }

        /// <summary>
        /// Gets the memory bar indicator background color.
        /// </summary>
        string MemoryBarIndicatorBackground { get; }

        /// <summary>
        /// Gets the memory bar track background color.
        /// </summary>
        string MemoryBarTrackBackground { get; }

        /// <summary>
        /// Gets the primary background color.
        /// </summary>
        string PrimaryBackground { get; }

        /// <summary>
        /// Gets the primary border color.
        /// </summary>
        string PrimaryBorder { get; }

        /// <summary>
        /// Gets the secondary background color.
        /// </summary>
        string SecondaryBackground { get; }

        /// <summary>
        /// Gets the secondary border color.
        /// </summary>
        string SecondaryBorder { get; }

        /// <summary>
        /// Gets the secondary disabled color.
        /// </summary>
        string SecondaryDisabled { get; }

        /// <summary>
        /// Gets the secondary foreground color.
        /// </summary>
        string SecondaryForeground { get; }
    }
}