using System;

namespace WinMemoryCleaner
{
    internal static class AutoOptimizationPolicy
    {
        internal static Enums.Memory.Optimization.Reason? GetReason(
            DateTimeOffset now,
            DateTimeOffset lastAutoOptimizationByInterval,
            DateTimeOffset lastAutoOptimizationByMemoryUsage,
            int autoOptimizationInterval,
            int autoOptimizationMemoryUsage,
            int freePhysicalMemoryPercentage)
        {
            if (autoOptimizationInterval > 0 &&
                now.Subtract(lastAutoOptimizationByInterval).TotalHours >= autoOptimizationInterval)
            {
                return Enums.Memory.Optimization.Reason.Schedule;
            }

            if (autoOptimizationMemoryUsage > 0 &&
                freePhysicalMemoryPercentage < autoOptimizationMemoryUsage &&
                now.Subtract(lastAutoOptimizationByMemoryUsage).TotalMinutes >= Constants.App.AutoOptimizationMemoryUsageInterval)
            {
                return Enums.Memory.Optimization.Reason.LowMemory;
            }

            return null;
        }
    }
}
