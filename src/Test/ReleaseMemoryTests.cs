using System;
using System.Collections.Generic;
using NUnit.Framework;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.MemoryReleaseTests
{
    [TestFixture]
    public sealed class ReleaseMemoryTests
    {
        [Test]
        public void ReleaseMemory_WhenModeIsNone_DoesNotRunCleanupStages()
        {
            var calls = new List<string>();

            App.ReleaseMemory(
                ReleaseMemoryMode.None,
                () => calls.Add("GC"),
                () => calls.Add("Trim"));

            Assert.AreEqual(0, calls.Count);
        }

        [Test]
        public void ReleaseMemory_WhenModeIsGCOnly_RunsOnlyGarbageCollection()
        {
            var calls = new List<string>();

            App.ReleaseMemory(
                ReleaseMemoryMode.GCOnly,
                () => calls.Add("GC"),
                () => calls.Add("Trim"));

            CollectionAssert.AreEqual(new[] { "GC" }, calls);
        }

        [Test]
        public void ReleaseMemory_WhenModeIsTrimOnly_RunsOnlyWorkingSetTrim()
        {
            var calls = new List<string>();

            App.ReleaseMemory(
                ReleaseMemoryMode.TrimOnly,
                () => calls.Add("GC"),
                () => calls.Add("Trim"));

            CollectionAssert.AreEqual(new[] { "Trim" }, calls);
        }

        [Test]
        public void ReleaseMemory_WhenModeIsCombined_RunsBothCleanupStages()
        {
            var calls = new List<string>();

            App.ReleaseMemory(
                ReleaseMemoryMode.Combined,
                () => calls.Add("GC"),
                () => calls.Add("Trim"));

            CollectionAssert.AreEqual(new[] { "GC", "Trim" }, calls);
        }

        [Test]
        public void ReleaseMemory_WhenCombined_RunsGarbageCollectionBeforeWorkingSetTrim()
        {
            var calls = new List<string>();

            App.ReleaseMemory(
                ReleaseMemoryMode.Combined,
                () => calls.Add("GC"),
                () => calls.Add("Trim"));

            Assert.Less(calls.IndexOf("GC"), calls.IndexOf("Trim"));
        }

        [Test]
        public void ReleaseMemory_WhenGarbageCollectionThrows_StillRunsWorkingSetTrim()
        {
            var calls = new List<string>();

            App.ReleaseMemory(
                ReleaseMemoryMode.Combined,
                () =>
                {
                    calls.Add("GC");
                    throw new InvalidOperationException();
                },
                () => calls.Add("Trim"));

            CollectionAssert.AreEqual(new[] { "GC", "Trim" }, calls);
        }

        [Test]
        public void ReleaseMemory_WhenWorkingSetTrimThrows_DoesNotPreventGarbageCollection()
        {
            var calls = new List<string>();

            App.ReleaseMemory(
                ReleaseMemoryMode.Combined,
                () => calls.Add("GC"),
                () =>
                {
                    calls.Add("Trim");
                    throw new InvalidOperationException();
                });

            CollectionAssert.AreEqual(new[] { "GC", "Trim" }, calls);
        }

        [Test]
        public void OptimizeTiming_WhenFinalAppReleaseRuns_IncludesItInTotalDuration()
        {
            var calls = new List<string>();
            var clock = new FakeClock();
            var timing = new OptimizationTiming(clock.Now);

            timing.Measure(() =>
            {
                calls.Add("Native");
                clock.Advance(TimeSpan.FromSeconds(2));
            });

            var totalRuntime = timing.Complete(() =>
            {
                calls.Add("AppRelease");
                clock.Advance(TimeSpan.FromSeconds(3));
            });

            CollectionAssert.AreEqual(new[] { "Native", "AppRelease" }, calls);
            Assert.AreEqual(TimeSpan.FromSeconds(3), timing.FinalAppReleaseDuration);
            Assert.AreEqual(TimeSpan.FromSeconds(5), totalRuntime);
        }

        [Test]
        public void OptimizeTiming_WhenNativeStageFailsAndOverheadRuns_IncludesAllElapsedTime()
        {
            var clock = new FakeClock(TimeSpan.FromSeconds(100));
            var timing = new OptimizationTiming(clock.Now);

            Assert.Throws<InvalidOperationException>(() => timing.Measure(() =>
            {
                clock.Advance(TimeSpan.FromSeconds(2));
                throw new InvalidOperationException();
            }));

            clock.Advance(TimeSpan.FromSeconds(1));

            var totalRuntime = timing.Complete(() => clock.Advance(TimeSpan.FromSeconds(3)));

            Assert.AreEqual(TimeSpan.FromSeconds(6), totalRuntime);
        }

        [Test]
        public void OptimizeTiming_WhenFinalAppReleaseThrows_CapturesItsDuration()
        {
            var clock = new FakeClock();
            var timing = new OptimizationTiming(clock.Now);

            clock.Advance(TimeSpan.FromSeconds(1));

            Assert.Throws<InvalidOperationException>(() => timing.Complete(() =>
            {
                clock.Advance(TimeSpan.FromSeconds(2));
                throw new InvalidOperationException();
            }));

            Assert.AreEqual(TimeSpan.FromSeconds(2), timing.FinalAppReleaseDuration);
            Assert.AreEqual(TimeSpan.FromSeconds(3), timing.Elapsed);
        }

        private sealed class FakeClock
        {
            private TimeSpan _now;

            public FakeClock(TimeSpan? initialTime = null)
            {
                _now = initialTime ?? TimeSpan.Zero;
            }

            public void Advance(TimeSpan duration)
            {
                _now = _now.Add(duration);
            }

            public TimeSpan Now()
            {
                return _now;
            }
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
