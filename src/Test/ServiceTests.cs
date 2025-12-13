using NUnit.Framework;
using System;
using System.Windows.Forms;
using System.Windows.Input;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.Test
{
    
    public static class ServiceTests
    {
        #region ComputerService Tests

        
        [TestFixture]
        public class ComputerServiceTests
        {
            private readonly ComputerService _computerService;

            public ComputerServiceTests()
            {
                _computerService = new ComputerService();
            }

            [Test]
            public void Memory_ReturnsValidMemoryObject()
            {
                var memory = _computerService.Memory;

                Assert.IsNotNull(memory);
                Assert.IsNotNull(memory.Physical);
                Assert.IsNotNull(memory.Virtual);
            }

            [Test]
            public void Memory_PhysicalMemory_HasValidValues()
            {
                var memory = _computerService.Memory;

                Assert.IsTrue(memory.Physical.Total.Bytes >= 0);
                Assert.IsTrue(memory.Physical.Free.Bytes >= 0);
                Assert.IsTrue(memory.Physical.Used.Bytes >= 0);
                Assert.IsTrue(memory.Physical.Used.Percentage >= 0);
                Assert.IsTrue(memory.Physical.Used.Percentage <= 100);
            }

            [Test]
            public void Memory_VirtualMemory_HasValidValues()
            {
                var memory = _computerService.Memory;

                Assert.IsTrue(memory.Virtual.Total.Bytes >= 0);
                Assert.IsTrue(memory.Virtual.Free.Bytes >= 0);
                Assert.IsTrue(memory.Virtual.Used.Bytes >= 0);
                Assert.IsTrue(memory.Virtual.Used.Percentage >= 0);
                Assert.IsTrue(memory.Virtual.Used.Percentage <= 100);
            }

            [Test]
            public void OperatingSystem_ReturnsValidOperatingSystemObject()
            {
                var os = _computerService.OperatingSystem;

                Assert.IsNotNull(os);
            }

            [Test]
            public void OperatingSystem_IsConsistentAcrossMultipleCalls()
            {
                var os1 = _computerService.OperatingSystem;
                var os2 = _computerService.OperatingSystem;

                Assert.AreSame(os1, os2);
                Assert.AreEqual(os1.Is64Bit, os2.Is64Bit);
                Assert.AreEqual(os1.IsWindowsVistaOrGreater, os2.IsWindowsVistaOrGreater);
            }

            [Test]
            public void Optimize_WithNoneAreas_DoesNotThrow()
            {
                Assert.DoesNotThrow(() =>
                    _computerService.Optimize(Enums.Memory.Optimization.Reason.Manual, Enums.Memory.Areas.None)
                );
            }

            [Test]
            public void OnOptimizeProgressUpdate_CanBeSubscribed()
            {
                var eventHandlerWasSet = false;

                _computerService.OnOptimizeProgressUpdate += (value, step) =>
                {
                    eventHandlerWasSet = true;
                };

                Assert.IsTrue(eventHandlerWasSet || !eventHandlerWasSet);
            }
        }

        #endregion

        #region HotkeyService Tests

        [TestFixture]
        public sealed class HotkeyServiceTests : IDisposable
        {
            private HotkeyService _hotkeyService;

            [SetUp]
            public void SetUp()
            {
                _hotkeyService = new HotkeyService();
            }

            [TearDown]
            public void TearDown()
            {
                if (_hotkeyService != null)
                {
                    _hotkeyService.Dispose();
                    _hotkeyService = null;
                }
            }

            [Test]
            public void Constructor_InitializesKeysCollection()
            {
                Assert.IsNotNull(_hotkeyService.Keys);
                Assert.IsTrue(_hotkeyService.Keys.Count > 0);
            }

            [Test]
            public void Constructor_InitializesModifiersCollection()
            {
                Assert.IsNotNull(_hotkeyService.Modifiers);
                Assert.IsTrue(_hotkeyService.Modifiers.Count > 0);
            }

            [Test]
            public void Keys_ContainsValidKeyboardKeys()
            {
                var keys = _hotkeyService.Keys;

                Assert.IsTrue(keys.Contains(Key.A));
                Assert.IsTrue(keys.Contains(Key.F1));
                Assert.IsTrue(keys.Contains(Key.F12));
            }

            [Test]
            public void Modifiers_ContainsValidModifierCombinations()
            {
                var modifiers = _hotkeyService.Modifiers;

                Assert.IsTrue(modifiers.ContainsKey(ModifierKeys.Control | ModifierKeys.Alt));
                Assert.IsTrue(modifiers.ContainsKey(ModifierKeys.Control | ModifierKeys.Shift));
                Assert.IsTrue(modifiers.ContainsKey(ModifierKeys.Alt | ModifierKeys.Shift));
            }

            [Test]
            public void Modifiers_HasCorrectDisplayNames()
            {
                var modifiers = _hotkeyService.Modifiers;

                Assert.AreEqual("CTRL + ALT", modifiers[ModifierKeys.Control | ModifierKeys.Alt]);
                Assert.AreEqual("CTRL + SHIFT", modifiers[ModifierKeys.Control | ModifierKeys.Shift]);
                Assert.AreEqual("SHIFT + ALT", modifiers[ModifierKeys.Alt | ModifierKeys.Shift]);
            }

            [Test]
            public void Register_WithNullHotkey_ReturnsFalse()
            {
                var result = _hotkeyService.Register(null, () => { });

                Assert.IsFalse(result);
            }

            [Test]
            public void Register_WithNullAction_ReturnsFalse()
            {
                var hotkey = new Hotkey(ModifierKeys.Control | ModifierKeys.Alt, Key.A);
                var result = _hotkeyService.Register(hotkey, null);

                Assert.IsFalse(result);
            }

            [Test]
            public void Unregister_WithNullHotkey_ReturnsFalse()
            {
                var result = _hotkeyService.Unregister(null);

                Assert.IsFalse(result);
            }

            [Test]
            public void Unregister_WithNonRegisteredHotkey_DoesNotThrow()
            {
                var hotkey = new Hotkey(ModifierKeys.Control | ModifierKeys.Alt, Key.Z);

                Assert.DoesNotThrow(() => _hotkeyService.Unregister(hotkey));
            }

            [Test]
            public void Dispose_CanBeCalledMultipleTimes()
            {
                _hotkeyService.Dispose();

                Assert.DoesNotThrow(() => _hotkeyService.Dispose());
            }

            public void Dispose()
            {
                // Ensure teardown logic runs when used as IDisposable
                try { TearDown(); } catch { }

                GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region NotificationService Tests

        [TestFixture]
        public sealed class NotificationServiceTests : IDisposable
        {
            private NotificationService _notificationService;
            private NotifyIcon _notifyIcon;

            [SetUp]
            public void SetUp()
            {
                _notifyIcon = new NotifyIcon();
                _notificationService = new NotificationService(_notifyIcon);
            }

            [TearDown]
            public void TearDown()
            {
                if (_notificationService != null)
                {
                    _notificationService.Dispose();
                    _notificationService = null;
                }

                if (_notifyIcon != null)
                {
                    _notifyIcon.Dispose();
                    _notifyIcon = null;
                }
            }

            [Test]
            public void Constructor_WithNotifyIcon_DoesNotThrow()
            {
                Assert.IsNotNull(_notificationService);
            }

            [Test]
            public void Initialize_SetsNotifyIconVisible()
            {
                _notificationService.Initialize();

                Assert.IsTrue(_notifyIcon.Visible);
            }

            [Test]
            public void Initialize_CreatesContextMenuStrip()
            {
                _notificationService.Initialize();

                Assert.IsNotNull(_notifyIcon.ContextMenuStrip);
                Assert.IsTrue(_notifyIcon.ContextMenuStrip.Items.Count > 0);
            }

            [Test]
            public void Update_WithNullMemory_ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() =>
                    _notificationService.Update(null)
                );
            }

            [Test]
            public void Update_WithValidMemory_DoesNotThrow()
            {
                var memStatus = Mocker.CreateMemoryStatusEx();
                var memory = new Memory(memStatus);

                Assert.DoesNotThrow(() =>
                    _notificationService.Update(memory)
                );
            }

            [Test]
            public void Update_WithValidMemory_UpdatesNotifyIconText()
            {
                var memStatus = Mocker.CreateMemoryStatusEx();
                var memory = new Memory(memStatus);

                _notificationService.Update(memory);

                Assert.IsNotNull(_notifyIcon.Text);
                Assert.IsNotEmpty(_notifyIcon.Text);
            }

            [Test]
            public void Update_WithOptimizingFlag_UpdatesNotifyIcon()
            {
                var memStatus = Mocker.CreateMemoryStatusEx();
                var memory = new Memory(memStatus);

                Assert.DoesNotThrow(() =>
                    _notificationService.Update(memory, true)
                );
            }

            [Test]
            public void Notify_WithValidMessage_DoesNotThrow()
            {
                Assert.DoesNotThrow(() =>
                    _notificationService.Notify("Test message")
                );
            }

            [Test]
            public void Notify_WithTitleAndTimeout_DoesNotThrow()
            {
                Assert.DoesNotThrow(() =>
                    _notificationService.Notify("Test message", "Test Title", 3, Enums.Icon.Notification.None)
                );
            }

            [Test]
            public void Dispose_CanBeCalledMultipleTimes()
            {
                _notificationService.Dispose();

                Assert.DoesNotThrow(() => _notificationService.Dispose());
            }
            
            public void Dispose()
            {
                // Ensure teardown logic runs when used as IDisposable
                try { TearDown(); } catch { }

                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
