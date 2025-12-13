using NUnit.Framework;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.Test
{
    /// <summary>
    /// Cleanup that resets settings to defaults after all other tests complete.
    /// </summary>
    [SetUpFixture]
    public sealed class TestCleanup
    {
        [TearDown]
        public void ResetSettingsAfterAllTests()
        {   
            Settings.Reset(true);
            Assert.IsTrue(true, "Settings have been reset to defaults");
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
