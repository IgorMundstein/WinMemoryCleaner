using NUnit.Framework;
using System;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.AutomationTests
{
    [TestFixture]
    public class AutoOptimizationPolicyTests
    {
        [Test]
        public void DisabledBoth_ReturnsNull()
        {
            var now = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

            var result = AutoOptimizationPolicy.GetReason(now, now.AddHours(-24), now.AddMinutes(-30), 0, 0, 0);

            Assert.IsNull(result);
        }

        [Test]
        public void Schedule_WhenJustBeforeDue_ReturnsNull()
        {
            var now = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

            var result = AutoOptimizationPolicy.GetReason(now, now.AddHours(-1).AddTicks(1), now, 1, 0, 0);

            Assert.IsNull(result);
        }

        [Test]
        public void Schedule_WhenExactlyDue_ReturnsSchedule()
        {
            var now = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

            var result = AutoOptimizationPolicy.GetReason(now, now.AddHours(-1), now, 1, 0, 0);

            Assert.AreEqual(Enums.Memory.Optimization.Reason.Schedule, result);
        }

        [Test]
        public void LowMemory_WhenFreePercentageIsBelowThreshold_ReturnsLowMemory()
        {
            var now = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

            var result = AutoOptimizationPolicy.GetReason(now, now, now.AddMinutes(-Constants.App.AutoOptimizationMemoryUsageInterval), 0, 40, 39);

            Assert.AreEqual(Enums.Memory.Optimization.Reason.LowMemory, result);
        }

        [Test]
        public void LowMemory_WhenFreePercentageEqualsThreshold_ReturnsNull()
        {
            var now = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

            var result = AutoOptimizationPolicy.GetReason(now, now, now.AddMinutes(-Constants.App.AutoOptimizationMemoryUsageInterval), 0, 40, 40);

            Assert.IsNull(result);
        }

        [Test]
        public void LowMemory_WhenCooldownIsJustBeforeDue_ReturnsNull()
        {
            var now = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

            var result = AutoOptimizationPolicy.GetReason(now, now, now.AddMinutes(-Constants.App.AutoOptimizationMemoryUsageInterval).AddTicks(1), 0, 40, 39);

            Assert.IsNull(result);
        }

        [Test]
        public void LowMemory_WhenCooldownIsExactlyDue_ReturnsLowMemory()
        {
            var now = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

            var result = AutoOptimizationPolicy.GetReason(now, now, now.AddMinutes(-Constants.App.AutoOptimizationMemoryUsageInterval), 0, 40, 39);

            Assert.AreEqual(Enums.Memory.Optimization.Reason.LowMemory, result);
        }

        [Test]
        public void Schedule_WhenBothTriggersAreDue_ReturnsSchedule()
        {
            var now = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

            var result = AutoOptimizationPolicy.GetReason(now, now.AddHours(-1), now.AddMinutes(-Constants.App.AutoOptimizationMemoryUsageInterval), 1, 40, 39);

            Assert.AreEqual(Enums.Memory.Optimization.Reason.Schedule, result);
        }

        [Test]
        public void Schedule_WhenNonZeroIntervalCrossesDateBoundary_ReturnsSchedule()
        {
            var now = new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero);

            var result = AutoOptimizationPolicy.GetReason(now, now.AddHours(-7), now, 7, 35, 100);

            Assert.AreEqual(Enums.Memory.Optimization.Reason.Schedule, result);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
