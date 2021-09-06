namespace WinMemoryCleaner
{
    /// <summary>
    /// ILoadingService
    /// </summary>
    public interface ILoadingService
    {
        /// <summary>
        /// Show/Hide Loading
        /// </summary>
        /// <param name="running">True (ON) / False (OFF)</param>
        void Loading(bool running);
    }
}
