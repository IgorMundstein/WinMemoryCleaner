using System;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Provides a deterministic seam for measuring optimization completion.
    /// </summary>
    internal sealed class OptimizationTiming
    {
        private readonly Func<TimeSpan> _clock;
        private readonly TimeSpan _startedAt;

        internal OptimizationTiming(Func<TimeSpan> clock)
        {
            if (clock == null)
                throw new ArgumentNullException("clock");

            _clock = clock;
            _startedAt = _clock();
        }

        internal TimeSpan FinalAppReleaseDuration { get; private set; }

        internal TimeSpan Elapsed
        {
            get { return _clock().Subtract(_startedAt); }
        }

        internal TimeSpan Measure(Action stage)
        {
            if (stage == null)
                throw new ArgumentNullException("stage");

            var startedAt = _clock();

            stage();

            return _clock().Subtract(startedAt);
        }

        internal TimeSpan Complete(Action finalAppRelease)
        {
            if (finalAppRelease == null)
                throw new ArgumentNullException("finalAppRelease");

            var finalAppReleaseStartedAt = _clock();

            try
            {
                finalAppRelease();
            }
            finally
            {
                FinalAppReleaseDuration = _clock().Subtract(finalAppReleaseStartedAt);
            }

            return Elapsed;
        }
    }

    internal enum ReleaseMemoryMode
    {
        None,
        GCOnly,
        TrimOnly,
        Combined
    }
}
