using NUnit.Framework;
using System;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.Test
{
    /// <summary>
    /// Unit tests for Model classes
    /// </summary>
    
    [TestFixture]
    public sealed class ModelTests
    {
        #region Computer Tests

        
        [TestFixture]
        public class ComputerTests
        {
            [Test]
            public void CanBeInstantiated()
            {
                Assert.DoesNotThrow(() => new Computer());
            }

            [Test]
            public void Memory_IsNotNullAfterInstantiation()
            {
                var computer = new Computer();

                Assert.IsNotNull(computer.Memory);
            }

            [Test]
            public void Memory_CanBeSet()
            {
                var computer = new Computer();
                var memStatus = Mocker.CreateMemoryStatusEx(
                    totalPhysical: 8589934592,
                    availPhysical: 4294967296);
                var memory = new Memory(memStatus);
                computer.Memory = memory;

                Assert.AreEqual(memory, computer.Memory);
            }

            [Test]
            public void OperatingSystem_IsNotNullAfterInstantiation()
            {
                var computer = new Computer();

                Assert.IsNotNull(computer.OperatingSystem);
            }

            [Test]
            public void OperatingSystem_CanBeSet()
            {
                var computer = new Computer();
                var os = new OperatingSystem();
                computer.OperatingSystem = os;

                Assert.AreEqual(os, computer.OperatingSystem);
            }
        }

        #endregion

        #region Hotkey Tests

        
        [TestFixture]
        public class HotkeyTests
        {
            [Test]
            public void Equals_WithDifferentHotkey_ReturnsFalse()
            {
                var hotkey1 = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.A);
                var hotkey2 = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.B);

                Assert.IsFalse(hotkey1.Equals(hotkey2));
            }

            [Test]
            public void Equals_WithDifferentObject_ReturnsFalse()
            {
                var hotkey = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M);
                var obj = new object();

                Assert.IsFalse(hotkey.Equals(obj));
            }

            [Test]
            public void Equals_WithNull_ReturnsFalse()
            {
                var hotkey = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M);

                Assert.IsFalse(hotkey.Equals((Hotkey)null));
            }

            [Test]
            public void Equals_WithNullObject_ReturnsFalse()
            {
                var hotkey = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M);

                Assert.IsFalse(hotkey.Equals((object)null));
            }

            [Test]
            public void Equals_WithSameHotkey_ReturnsTrue()
            {
                var hotkey1 = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M);
                var hotkey2 = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M);

                Assert.IsTrue(hotkey1.Equals(hotkey2));
            }

            [Test]
            public void GetHashCode_ReturnsValidHashCode()
            {
                var hotkey = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M);
                var hashCode = hotkey.GetHashCode();

                Assert.IsNotNull(hashCode);
                Assert.AreNotEqual(0, hashCode);
            }

            [Test]
            public void GetHashCode_SameHotkeys_ReturnsSameHashCode()
            {
                var hotkey1 = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M);
                var hotkey2 = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M);

                Assert.AreEqual(hotkey1.GetHashCode(), hotkey2.GetHashCode());
            }

            [Test]
            public void Key_IsSetByConstructor()
            {
                var hotkey = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M);

                Assert.AreEqual(System.Windows.Input.Key.M, hotkey.Key);
            }

            [Test]
            public void Modifiers_IsSetByConstructor()
            {
                var hotkey = new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M);

                Assert.AreEqual(System.Windows.Input.ModifierKeys.Control, hotkey.Modifiers);
            }

            [Test]
            public void ToString_ReturnsFormattedString()
            {
                var hotkey = new Hotkey(System.Windows.Input.ModifierKeys.Control | System.Windows.Input.ModifierKeys.Shift, System.Windows.Input.Key.M);
                var result = hotkey.ToString();

                Assert.IsNotNull(result);
                Assert.IsFalse(string.IsNullOrEmpty(result));
            }

            [Test]
            public void WithModifiersAndKey_CanBeInstantiated()
            {
                Assert.DoesNotThrow(() => new Hotkey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.M));
            }
        }

        #endregion

        #region Language Tests

        
        [TestFixture]
        public class LanguageTests
        {
            [Test]
            public void WithCulture_CanBeInstantiated()
            {
                Assert.DoesNotThrow(() => new Language(System.Globalization.CultureInfo.InvariantCulture));
            }
        }

        #endregion

        #region Localization Tests

        
        [TestFixture]
        public class LocalizationTests
        {
            [Test]
            public void CanBeInstantiated()
            {
                Assert.DoesNotThrow(() => new Localization());
            }
        }

        #endregion

        #region Log Tests

        
        [TestFixture]
        public class LogTests
        {
            [Test]
            public void WithLevelAndMessage_CanBeInstantiated()
            {
                Assert.DoesNotThrow(() => new Log(Enums.Log.Levels.Information, "Test message", null, null));
            }
        }

        #endregion

        #region Memory Tests

        
        [TestFixture]
        public class MemoryTests
        {
            [Test]
            public void Constructor_Default_InitializesWithZeroValues()
            {
                var memory = new Memory();

                Assert.IsNotNull(memory);
                Assert.IsNotNull(memory.Physical);
                Assert.IsNotNull(memory.Virtual);
                Assert.AreEqual(0, memory.Physical.Total.Bytes);
                Assert.AreEqual(0, memory.Physical.Free.Bytes);
                Assert.AreEqual(0, memory.Virtual.Total.Bytes);
                Assert.AreEqual(0, memory.Virtual.Free.Bytes);
            }

            [Test]
            public void Constructor_WithFullMemoryUsage_HandlesCorrectly()
            {
                var memStatus = Mocker.CreateMemoryStatusEx(
                    totalPhysical: 8589934592,
                    availPhysical: 0,
                    memoryLoad: 100);
                var memory = new Memory(memStatus);

                Assert.AreEqual(100, memory.Physical.Used.Percentage);
                Assert.AreEqual(0, memory.Physical.Free.Bytes);
            }

            [Test]
            public void Constructor_WithMemoryLoad_SetsPercentagesCorrectly()
            {
                var memStatus = Mocker.CreateMemoryStatusEx(memoryLoad: 75);
                var memory = new Memory(memStatus);

                Assert.AreEqual(75, memory.Physical.Used.Percentage);
                Assert.AreEqual(25, memory.Physical.Free.Percentage);
            }

            [Test]
            public void Constructor_WithMemoryStatusEx_InitializesCorrectly()
            {
                var memStatus = Mocker.CreateMemoryStatusEx(
                    totalPhysical: 8589934592,
                    availPhysical: 4294967296,
                    totalPageFile: 17179869184,
                    availPageFile: 12884901888,
                    memoryLoad: 50);
                var memory = new Memory(memStatus);

                Assert.IsNotNull(memory);
                Assert.IsNotNull(memory.Physical);
                Assert.IsNotNull(memory.Virtual);
                Assert.AreEqual(8589934592, memory.Physical.Total.Bytes);
                Assert.AreEqual(4294967296, memory.Physical.Free.Bytes);
                Assert.AreEqual(17179869184, memory.Virtual.Total.Bytes);
                Assert.AreEqual(12884901888, memory.Virtual.Free.Bytes);
            }

            [Test]
            public void Constructor_WithMinimalMemoryUsage_HandlesCorrectly()
            {
                var memStatus = Mocker.CreateMemoryStatusEx(
                    totalPhysical: 8589934592,
                    availPhysical: 8589934592,
                    memoryLoad: 0);
                var memory = new Memory(memStatus);

                Assert.AreEqual(0, memory.Physical.Used.Percentage);
                Assert.AreEqual(8589934592, memory.Physical.Free.Bytes);
            }

            [Test]
            public void Constructor_WithNullMemoryStatusEx_ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => new Memory(null));
            }

            [Test]
            public void Physical_IsNotNull()
            {
                var memStatus = Mocker.CreateMemoryStatusEx();
                var memory = new Memory(memStatus);

                Assert.IsNotNull(memory.Physical);
            }

            [Test]
            public void Virtual_IsNotNull()
            {
                var memStatus = Mocker.CreateMemoryStatusEx();
                var memory = new Memory(memStatus);

                Assert.IsNotNull(memory.Virtual);
            }
        }

        #endregion

        #region MemorySize Tests

        
        [TestFixture]
        public class MemorySizeTests
        {
            [Test]
            public void Bytes_ReturnsCorrectValue()
            {
                long bytes = 8589934592; // 8 GB
                var memorySize = new MemorySize(bytes);
                var result = memorySize.Bytes;

                Assert.AreEqual(bytes, result);
            }

            [Test]
            public void Constructor_WithLargeValue_HandlesCorrectly()
            {
                long bytes = long.MaxValue / 2;
                var memorySize = new MemorySize(bytes);

                Assert.AreEqual(bytes, memorySize.Bytes);
                Assert.IsTrue(memorySize.Value > 0);
            }

            [Test]
            public void Constructor_WithPositiveBytes_InitializesCorrectly()
            {
                long bytes = 1073741824; // 1 GB
                var memorySize = new MemorySize(bytes);

                Assert.AreEqual(bytes, memorySize.Bytes);
                Assert.IsTrue(memorySize.Value > 0);
            }

            [Test]
            public void Constructor_WithZeroBytes_InitializesCorrectly()
            {
                var memorySize = new MemorySize(0);

                Assert.AreEqual(0, memorySize.Bytes);
                Assert.AreEqual(0, memorySize.Value);
            }

            [Test]
            public void Percentage_DefaultValue_IsZero()
            {
                var memorySize = new MemorySize(1024);

                Assert.AreEqual(0, memorySize.Percentage);
            }

            [Test]
            public void Percentage_IsSettable()
            {
                var memorySize = new MemorySize(1024) { Percentage = 75 };

                Assert.AreEqual(75, memorySize.Percentage);
            }

            [Test]
            public void ToString_ReturnsFormattedString()
            {
                var memorySize = new MemorySize(1073741824) { Percentage = 50 }; // 1 GB
                var result = memorySize.ToString();

                Assert.IsNotNull(result);
                Assert.IsFalse(string.IsNullOrEmpty(result));
                Assert.IsTrue(result.Contains("%"));
            }

            [Test]
            public void Unit_IsSetByConstructor()
            {
                long bytes = 1073741824; // 1 GB
                var memorySize = new MemorySize(bytes);

                Assert.IsNotNull(memorySize.Unit);
            }

            [Test]
            public void Value_IsSetByConstructor()
            {
                long bytes = 2147483648; // 2 GB
                var memorySize = new MemorySize(bytes);

                Assert.IsTrue(memorySize.Value > 0);
            }
        }

        #endregion

        #region MemoryStats Tests

        
        [TestFixture]
        public class MemoryStatsTests
        {
            [Test]
            public void Constructor_With50PercentUsage_CalculatesCorrectly()
            {
                long free = 4294967296;
                long total = 8589934592;
                int used = 50;
                var stats = new MemoryStats(free, total, used);

                Assert.AreEqual(50, stats.Used.Percentage);
                Assert.AreEqual(50, stats.Free.Percentage);
            }

            [Test]
            public void Constructor_WithAllFreeMemory_Shows0PercentUsed()
            {
                long free = 8589934592;
                long total = 8589934592;
                int used = 0;
                var stats = new MemoryStats(free, total, used);

                Assert.AreEqual(0, stats.Used.Percentage);
                Assert.AreEqual(100, stats.Free.Percentage);
            }

            [Test]
            public void Constructor_WithFullMemory_Shows100PercentUsed()
            {
                long free = 0;
                long total = 8589934592;
                int used = 100;
                var stats = new MemoryStats(free, total, used);

                Assert.AreEqual(100, stats.Used.Percentage);
                Assert.AreEqual(0, stats.Free.Percentage);
            }

            [Test]
            public void Constructor_WithUsedPercentage_SetsPercentagesCorrectly()
            {
                long free = 2147483648;
                long total = 8589934592;
                int used = 75;
                var stats = new MemoryStats(free, total, used);

                Assert.AreEqual(75, stats.Used.Percentage);
                Assert.AreEqual(25, stats.Free.Percentage);
            }

            [Test]
            public void Constructor_WithValidValues_InitializesCorrectly()
            {
                long free = 4294967296;
                long total = 8589934592;
                var stats = new MemoryStats(free, total);

                Assert.IsNotNull(stats);
                Assert.IsNotNull(stats.Free);
                Assert.IsNotNull(stats.Total);
                Assert.IsNotNull(stats.Used);
                Assert.AreEqual(free, stats.Free.Bytes);
                Assert.AreEqual(total, stats.Total.Bytes);
            }

            [Test]
            public void Constructor_WithoutUsedPercentage_CalculatesPercentages()
            {
                long free = 4294967296;
                long total = 8589934592;
                var stats = new MemoryStats(free, total);

                Assert.IsTrue(stats.Used.Percentage > 0);
                Assert.IsTrue(stats.Free.Percentage > 0);
                Assert.AreEqual(100, stats.Used.Percentage + stats.Free.Percentage);
            }

            [Test]
            public void Constructor_WithZeroTotal_HandlesCorrectly()
            {
                long free = 0;
                long total = 0;
                var stats = new MemoryStats(free, total);

                Assert.IsNotNull(stats);
                Assert.AreEqual(0, stats.Free.Bytes);
                Assert.AreEqual(0, stats.Total.Bytes);
                Assert.AreEqual(0, stats.Used.Bytes);
            }

            [Test]
            public void Free_IsNotNull()
            {
                var stats = new MemoryStats(1024, 2048);

                Assert.IsNotNull(stats.Free);
            }

            [Test]
            public void ToString_ReturnsFormattedString()
            {
                var stats = new MemoryStats(4294967296, 8589934592);
                var result = stats.ToString();

                Assert.IsNotNull(result);
                Assert.IsFalse(string.IsNullOrEmpty(result));
            }

            [Test]
            public void Total_IsNotNull()
            {
                var stats = new MemoryStats(1024, 2048);

                Assert.IsNotNull(stats.Total);
            }

            [Test]
            public void Used_EqualsTotalMinusFree()
            {
                long free = 3221225472;
                long total = 8589934592;
                var stats = new MemoryStats(free, total);

                Assert.AreEqual(total - free, stats.Used.Bytes);
            }

            [Test]
            public void Used_IsNotNull()
            {
                var stats = new MemoryStats(1024, 2048);

                Assert.IsNotNull(stats.Used);
            }
        }

        #endregion

        #region ObservableItem Tests

        
        [TestFixture]
        public class ObservableItemTests
        {
            [Test]
            public void Generic_CanBeInstantiated()
            {
                Assert.DoesNotThrow(() => new ObservableItem<string>("TestItem", () => "value", null));
            }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
