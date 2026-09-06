using NUnit.Framework;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.NativeMemoryTests
{
    [TestFixture]
    public sealed class NativeMemoryInteropTests
    {
        [Test]
        public void CreateNtStatusException_WhenStatusIsAccessDenied_MapsToWin32AccessDenied()
        {
            var exception = ComputerService.CreateNtStatusException(Constants.Windows.NtStatus.StatusAccessDenied);

            Assert.AreEqual(Constants.Windows.SystemErrorCode.ErrorAccessDenied, exception.NativeErrorCode);
        }

        [Test]
        public void CreateNtStatusException_WhenStatusIsInvalidParameter_MapsWithoutStaleWin32Error()
        {
            var exception = ComputerService.CreateNtStatusException(Constants.Windows.NtStatus.StatusInvalidParameter);

            Assert.AreEqual(Constants.Windows.SystemErrorCode.ErrorInvalidParameter, exception.NativeErrorCode);
        }

        [Test]
        public void IsAdjustTokenPrivilegesSuccessful_WhenNativeCallAndLastErrorSucceed_ReturnsTrue()
        {
            Assert.IsTrue(ComputerService.IsAdjustTokenPrivilegesSuccessful(
                true,
                Constants.Windows.SystemErrorCode.ErrorSuccess));
        }

        [Test]
        public void IsAdjustTokenPrivilegesSuccessful_WhenNotAllPrivilegesAreAssigned_ReturnsFalse()
        {
            Assert.IsFalse(ComputerService.IsAdjustTokenPrivilegesSuccessful(
                true,
                Constants.Windows.SystemErrorCode.ErrorNotAllAssigned));
        }

        [Test]
        public void IsAdjustTokenPrivilegesSuccessful_WhenNativeCallSucceedsWithError_ReturnsFalse()
        {
            Assert.IsFalse(ComputerService.IsAdjustTokenPrivilegesSuccessful(
                true,
                Constants.Windows.SystemErrorCode.ErrorAccessDenied));
        }

        [Test]
        public void IsAdjustTokenPrivilegesSuccessful_WhenNativeCallFailsWithSuccessError_ReturnsFalse()
        {
            Assert.IsFalse(ComputerService.IsAdjustTokenPrivilegesSuccessful(
                false,
                Constants.Windows.SystemErrorCode.ErrorSuccess));
        }

        [Test]
        public void IsAdjustTokenPrivilegesSuccessful_WhenNativeCallFailsWithError_ReturnsFalse()
        {
            Assert.IsFalse(ComputerService.IsAdjustTokenPrivilegesSuccessful(
                false,
                Constants.Windows.SystemErrorCode.ErrorAccessDenied));
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
