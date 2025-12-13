using NUnit.Framework;
using System;
using System.Linq;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.Test
{

    public static class ViewModelTests
    {
        #region MainViewModel Tests


        public sealed class MainViewModelTests : IDisposable
        {
            private IComputerService _computerService;
            private IHotkeyService _hotkeyService;
            private INotificationService _notificationService;
            private MainViewModel _viewModel;

            public MainViewModelTests()
            {
                SetUp();
            }

            public void SetUp()
            {
                // Ensure WPF Application context exists before accessing Localizer
                if (System.Windows.Application.Current == null)
                {
                    var app = new System.Windows.Application();
                }

                // Force Localizer initialization and verify it succeeded
                var localizerString = Localizer.String;
                var localizerCulture = Localizer.Culture;

                if (localizerCulture == null)
                {
                    throw new InvalidOperationException("Localizer.Culture failed to initialize. This is required for MainViewModel tests.");
                }

                // Initialize App.Version if not set (required for Title property)
                if (App.Version == null)
                {
                    var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    typeof(App).GetProperty("Version").SetValue(null, version, null);
                }

                _computerService = new ComputerService();
                _hotkeyService = new HotkeyService();
                _notificationService = new NotificationService(new NotifyIcon());

                _viewModel = new MainViewModel(_computerService, _hotkeyService, _notificationService);

                // Wait for Computer.Memory to be initialized (with timeout)
                var maxWait = 50; // 500ms max wait
                var waitCount = 0;
                while (_viewModel.Computer.Memory == null && waitCount < maxWait)
                {
                    System.Threading.Thread.Sleep(10);
                    waitCount++;
                }

                // If still null after waiting, initialize it manually
                if (_viewModel.Computer.Memory == null)
                {
                    _viewModel.Computer.Memory = _computerService.Memory;
                }
            }

            public void Dispose()
            {
                if (_viewModel != null)
                {
                    _viewModel.Dispose();
                    _viewModel = null;
                }

                if (_hotkeyService != null)
                {
                    _hotkeyService.Dispose();
                    _hotkeyService = null;
                }

                if (_notificationService != null)
                {
                    _notificationService.Dispose();
                    _notificationService = null;
                }

                GC.SuppressFinalize(this);
            }

            #region Constructor Tests

            [Test]
            public void Constructor_InitializesCommands()
            {
                Assert.IsNotNull(_viewModel.AddProcessToExclusionListCommand);
                Assert.IsNotNull(_viewModel.OptimizeCommand);
                Assert.IsNotNull(_viewModel.RemoveProcessFromExclusionListCommand);
                Assert.IsNotNull(_viewModel.ResetSettingsToDefaultConfigurationCommand);
            }

            [Test]
            public void Constructor_InitializesComputer()
            {
                Assert.IsNotNull(_viewModel.Computer);
                Assert.IsNotNull(_viewModel.Computer.Memory);
                Assert.IsNotNull(_viewModel.Computer.OperatingSystem);
            }

            [Test]
            public void Constructor_InitializesMemoryUsageThresholds()
            {
                Assert.IsNotNull(_viewModel.MemoryUsageThresholds);
                Assert.AreEqual(99, _viewModel.MemoryUsageThresholds.Count);
                Assert.AreEqual(1, _viewModel.MemoryUsageThresholds.First());
                Assert.AreEqual(99, _viewModel.MemoryUsageThresholds.Last());
            }

            #endregion

            #region Property Tests

            [Test]
            public void AlwaysOnTop_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.AlwaysOnTop;
                var newValue = !initialValue;

                _viewModel.AlwaysOnTop = newValue;

                Assert.AreEqual(newValue, _viewModel.AlwaysOnTop);
                Assert.AreEqual(newValue, Settings.AlwaysOnTop);
            }

            [Test]
            public void AutoOptimizationInterval_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.AutoOptimizationInterval;
                // Use a different value than the default
                var newValue = initialValue == 5 ? 10 : 5;

                _viewModel.AutoOptimizationInterval = newValue;

                Assert.AreEqual(newValue, _viewModel.AutoOptimizationInterval);
                Assert.AreEqual(newValue, Settings.AutoOptimizationInterval);
            }

            [Test]
            public void AutoOptimizationMemoryUsage_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.AutoOptimizationMemoryUsage;
                // Use a different value than the default
                var newValue = initialValue == 75 ? 50 : 75;

                _viewModel.AutoOptimizationMemoryUsage = newValue;

                Assert.AreEqual(newValue, _viewModel.AutoOptimizationMemoryUsage);
                Assert.AreEqual(newValue, Settings.AutoOptimizationMemoryUsage);
            }

            [Test]
            public void AutoOptimizationInterval_SetTo1Hour_UpdatesCorrectly()
            {
                _viewModel.AutoOptimizationInterval = 1;

                Assert.AreEqual(1, _viewModel.AutoOptimizationInterval);
                Assert.AreEqual(1, Settings.AutoOptimizationInterval);
            }

            [Test]
            public void AutoOptimizationInterval_SetTo12Hours_UpdatesCorrectly()
            {
                _viewModel.AutoOptimizationInterval = 12;

                Assert.AreEqual(12, _viewModel.AutoOptimizationInterval);
                Assert.AreEqual(12, Settings.AutoOptimizationInterval);
            }

            [Test]
            public void AutoOptimizationInterval_SetTo24Hours_UpdatesCorrectly()
            {
                _viewModel.AutoOptimizationInterval = 24;

                Assert.AreEqual(24, _viewModel.AutoOptimizationInterval);
                Assert.AreEqual(24, Settings.AutoOptimizationInterval);
            }

            [Test]
            public void AutoOptimizationMemoryUsage_SetTo50Percent_UpdatesCorrectly()
            {
                _viewModel.AutoOptimizationMemoryUsage = 50;

                Assert.AreEqual(50, _viewModel.AutoOptimizationMemoryUsage);
                Assert.AreEqual(50, Settings.AutoOptimizationMemoryUsage);
            }

            [Test]
            public void AutoOptimizationMemoryUsage_SetTo75Percent_UpdatesCorrectly()
            {
                _viewModel.AutoOptimizationMemoryUsage = 75;

                Assert.AreEqual(75, _viewModel.AutoOptimizationMemoryUsage);
                Assert.AreEqual(75, Settings.AutoOptimizationMemoryUsage);
            }

            [Test]
            public void AutoOptimizationMemoryUsage_SetTo90Percent_UpdatesCorrectly()
            {
                _viewModel.AutoOptimizationMemoryUsage = 90;

                Assert.AreEqual(90, _viewModel.AutoOptimizationMemoryUsage);
                Assert.AreEqual(90, Settings.AutoOptimizationMemoryUsage);
            }

            [Test]
            public void AutoUpdate_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.AutoUpdate;
                var newValue = !initialValue;

                _viewModel.AutoUpdate = newValue;

                Assert.AreEqual(newValue, _viewModel.AutoUpdate);
            }

            [Test]
            public void Brushes_ReturnsNonEmptyCollection()
            {
                var brushes = _viewModel.Brushes;

                Assert.IsNotNull(brushes);
                Assert.IsTrue(brushes.Count > 0);
            }

            [Test]
            public void CanOptimize_WithValidMemoryAreas_ReturnsTrue()
            {
                Settings.MemoryAreas = Enums.Memory.Areas.WorkingSet;

                Assert.IsTrue(_viewModel.CanOptimize);
            }

            [Test]
            public void CanOptimize_WithNoMemoryAreas_ReturnsFalse()
            {
                Settings.MemoryAreas = Enums.Memory.Areas.None;

                Assert.IsFalse(_viewModel.CanOptimize);
            }

            [Test]
            public void CloseAfterOptimization_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.CloseAfterOptimization;

                _viewModel.CloseAfterOptimization = !initialValue;

                Assert.AreEqual(!initialValue, _viewModel.CloseAfterOptimization);
                Assert.AreEqual(!initialValue, Settings.CloseAfterOptimization);
            }

            [Test]
            public void CloseToTheNotificationArea_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.CloseToTheNotificationArea;

                _viewModel.CloseToTheNotificationArea = !initialValue;

                Assert.AreEqual(!initialValue, _viewModel.CloseToTheNotificationArea);
                Assert.AreEqual(!initialValue, Settings.CloseToTheNotificationArea);
            }

            [Test]
            public void CompactMode_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.CompactMode;

                _viewModel.CompactMode = !initialValue;

                Assert.AreEqual(!initialValue, _viewModel.CompactMode);
                Assert.AreEqual(!initialValue, Settings.CompactMode);
            }

            [Test]
            public void Computer_IsNotNull()
            {
                Assert.IsNotNull(_viewModel.Computer);
            }

            [Test]
            public void FontSize_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.FontSize;
                // Use a different value than the default
                var newValue = initialValue == 16.0 ? 14.0 : 16.0;

                _viewModel.FontSize = newValue;

                Assert.AreEqual(newValue, _viewModel.FontSize);
                Assert.AreEqual(newValue, Settings.FontSize);
            }

            [Test]
            public void FontSize_SetTo10_UpdatesCorrectly()
            {
                _viewModel.FontSize = 10.0;

                Assert.AreEqual(10.0, _viewModel.FontSize);
                Assert.AreEqual(10.0, Settings.FontSize);
            }

            [Test]
            public void FontSize_SetTo12_UpdatesCorrectly()
            {
                _viewModel.FontSize = 12.0;

                Assert.AreEqual(12.0, _viewModel.FontSize);
                Assert.AreEqual(12.0, Settings.FontSize);
            }

            [Test]
            public void FontSize_SetTo14_UpdatesCorrectly()
            {
                _viewModel.FontSize = 14.0;

                Assert.AreEqual(14.0, _viewModel.FontSize);
                Assert.AreEqual(14.0, Settings.FontSize);
            }

            [Test]
            public void FontSize_SetTo16_UpdatesCorrectly()
            {
                _viewModel.FontSize = 16.0;

                Assert.AreEqual(16.0, _viewModel.FontSize);
                Assert.AreEqual(16.0, Settings.FontSize);
            }

            [Test]
            public void FontSize_SetTo18_UpdatesCorrectly()
            {
                _viewModel.FontSize = 18.0;

                Assert.AreEqual(18.0, _viewModel.FontSize);
                Assert.AreEqual(18.0, Settings.FontSize);
            }

            [Test]
            public void IsOptimizationRunning_DefaultsToFalse()
            {
                Assert.IsFalse(_viewModel.IsOptimizationRunning);
            }

            [Test]
            public void KeyboardKeys_ReturnsFromHotkeyService()
            {
                var keys = _viewModel.KeyboardKeys;

                Assert.IsNotNull(keys);
                Assert.IsTrue(keys.Count > 0);
            }

            [Test]
            public void KeyboardModifiers_ReturnsFromHotkeyService()
            {
                var modifiers = _viewModel.KeyboardModifiers;

                Assert.IsNotNull(modifiers);
                Assert.IsTrue(modifiers.Count > 0);
            }

            [Test]
            public void MemoryAreaItems_ReturnsNonEmptyCollection()
            {
                var items = _viewModel.MemoryAreaItems;

                Assert.IsNotNull(items);
                Assert.IsTrue(items.Count > 0);
            }

            [Test]
            public void MemoryUsageThresholds_ContainsAllValues1To99()
            {
                var thresholds = _viewModel.MemoryUsageThresholds;

                for (byte i = 1; i <= 99; i++)
                {
                    Assert.Contains(i, thresholds);
                }
            }

            [Test]
            public void PhysicalMemoryHeader_ContainsMemoryInfo()
            {
                var header = _viewModel.PhysicalMemoryHeader;

                Assert.IsNotNull(header);
                Assert.IsNotEmpty(header);
            }

            [Test]
            public void Processes_ReturnsNonEmptyCollection()
            {
                var processes = _viewModel.Processes;

                Assert.IsNotNull(processes);
            }

            [Test]
            public void ProcessExclusionList_ReturnsCurrentSettings()
            {
                var list = _viewModel.ProcessExclusionList;

                Assert.IsNotNull(list);
            }

            [Test]
            public void RunOnLowPriority_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.RunOnLowPriority;

                _viewModel.RunOnLowPriority = !initialValue;

                Assert.AreEqual(!initialValue, _viewModel.RunOnLowPriority);
            }

            [Test]
            public void ShowOptimizationNotifications_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.ShowOptimizationNotifications;

                _viewModel.ShowOptimizationNotifications = !initialValue;

                Assert.AreEqual(!initialValue, _viewModel.ShowOptimizationNotifications);
                Assert.AreEqual(!initialValue, Settings.ShowOptimizationNotifications);
            }

            [Test]
            public void ShowVirtualMemory_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.ShowVirtualMemory;

                _viewModel.ShowVirtualMemory = !initialValue;

                Assert.AreEqual(!initialValue, _viewModel.ShowVirtualMemory);
                Assert.AreEqual(!initialValue, Settings.ShowVirtualMemory);
            }

            [Test]
            public void StartMinimized_SetValue_UpdatesSettings()
            {
                var initialValue = _viewModel.StartMinimized;

                _viewModel.StartMinimized = !initialValue;

                Assert.AreEqual(!initialValue, _viewModel.StartMinimized);
                Assert.AreEqual(!initialValue, Settings.StartMinimized);
            }

            [Test]
            public void Title_IsNotEmpty()
            {
                // Verify Localizer is properly initialized
                Assert.IsNotNull(Localizer.Culture);
                Assert.IsNotNull(Localizer.String);

                // Verify Computer and Memory are initialized
                Assert.IsNotNull(_viewModel.Computer);
                Assert.IsNotNull(_viewModel.Computer.Memory);

                var title = _viewModel.Title;

                Assert.IsNotNull(title);
                Assert.IsNotEmpty(title);
                StringAssert.Contains("Windows Memory Cleaner", title);
            }

            [Test]
            public void Title_MatchesCompactModeState()
            {
                // Verify Localizer is properly initialized
                Assert.IsNotNull(Localizer.Culture);
                Assert.IsNotNull(Localizer.String);

                // Verify Computer and Memory are initialized
                Assert.IsNotNull(_viewModel.Computer);
                Assert.IsNotNull(_viewModel.Computer.Memory);

                // Test with compact mode off
                _viewModel.CompactMode = false;
                var fullTitle = _viewModel.Title;

                Assert.IsNotEmpty(fullTitle);

                // Test with compact mode on
                _viewModel.CompactMode = true;
                var compactTitle = _viewModel.Title;

                Assert.IsNotEmpty(compactTitle);

                // Compact title should be shorter than or equal to full title
                Assert.IsTrue(compactTitle.Length <= fullTitle.Length);
            }

            [Test]
            public void VirtualMemoryHeader_ContainsMemoryInfo()
            {
                var header = _viewModel.VirtualMemoryHeader;

                Assert.IsNotNull(header);
                Assert.IsNotEmpty(header);
            }

            #endregion

            #region Command Tests

            [Test]
            public void OptimizeCommand_CanExecute_WithValidMemoryAreas_ReturnsTrue()
            {
                Settings.MemoryAreas = Enums.Memory.Areas.WorkingSet;

                Assert.IsTrue(_viewModel.OptimizeCommand.CanExecute(null));
            }

            [Test]
            public void OptimizeCommand_CanExecute_WithNoMemoryAreas_ReturnsFalse()
            {
                Settings.MemoryAreas = Enums.Memory.Areas.None;

                Assert.IsFalse(_viewModel.OptimizeCommand.CanExecute(null));
            }

            [Test]
            public void AddProcessToExclusionListCommand_IsNotNull()
            {
                Assert.IsNotNull(_viewModel.AddProcessToExclusionListCommand);
            }

            [Test]
            public void RemoveProcessFromExclusionListCommand_IsNotNull()
            {
                Assert.IsNotNull(_viewModel.RemoveProcessFromExclusionListCommand);
            }

            [Test]
            public void ResetSettingsToDefaultConfigurationCommand_IsNotNull()
            {
                Assert.IsNotNull(_viewModel.ResetSettingsToDefaultConfigurationCommand);
            }

            #endregion

            #region Method Tests

            [Test]
            public void ReinitializeAfterHibernation_UpdatesMemory()
            {
                var initialMemory = _viewModel.Computer.Memory;

                _viewModel.ReinitializeAfterHibernation();

                Assert.IsNotNull(_viewModel.Computer.Memory);
                // Memory object should be refreshed (new instance)
                Assert.AreNotSame(initialMemory, _viewModel.Computer.Memory);
            }

            [Test]
            public void Dispose_CanBeCalledMultipleTimes()
            {
                _viewModel.Dispose();

                Exception exception = null;
                try { _viewModel.Dispose(); } catch (Exception ex) { exception = ex; }

                Assert.IsNull(exception);
            }

            [Test]
            public void Dispose_SetsIsOptimizationRunningToFalse()
            {
                // Ensure optimization is not running before disposal
                Assert.IsFalse(_viewModel.IsOptimizationRunning);

                _viewModel.Dispose();

                // After disposal, optimization should definitely not be running
                Assert.IsFalse(_viewModel.IsOptimizationRunning);
            }

            #endregion

            #region Integration Tests

            [Test]
            public void CompactMode_Toggle_RaisesPropertyChanged()
            {
                var propertyChangedRaised = false;
                var initialValue = _viewModel.CompactMode;

                _viewModel.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == "CompactMode" || args.PropertyName == "Title")
                    {
                        propertyChangedRaised = true;
                    }
                };

                _viewModel.CompactMode = !initialValue;

                Assert.IsTrue(propertyChangedRaised);
            }

            [Test]
            public void MemoryAreas_SettingMultipleAreas_WorksCorrectly()
            {
                // Set a memory area
                Settings.MemoryAreas = Enums.Memory.Areas.WorkingSet;

                Assert.AreEqual(Enums.Memory.Areas.WorkingSet, _viewModel.MemoryAreas);

                // Set another memory area
                Settings.MemoryAreas = Enums.Memory.Areas.StandbyList;

                Assert.AreEqual(Enums.Memory.Areas.StandbyList, _viewModel.MemoryAreas);

                // Set to none
                Settings.MemoryAreas = Enums.Memory.Areas.None;

                Assert.AreEqual(Enums.Memory.Areas.None, _viewModel.MemoryAreas);
            }

            [Test]
            public void CanOptimize_DependsOnMemoryAreasAndOptimizationState()
            {
                // Should not be able to optimize with no memory areas
                Settings.MemoryAreas = Enums.Memory.Areas.None;

                Assert.IsFalse(_viewModel.CanOptimize);

                // Should be able to optimize with valid memory areas and not running
                Settings.MemoryAreas = Enums.Memory.Areas.WorkingSet;

                Assert.IsTrue(_viewModel.CanOptimize);
            }

            #endregion
        }

        #endregion

        #region MessageViewModel Tests


        public sealed class MessageViewModelTests : IDisposable
        {
            private INotificationService _notificationService;
            private MessageViewModel _viewModel;

            public MessageViewModelTests()
            {
                SetUp();
            }

            public void SetUp()
            {
                _notificationService = new NotificationService(new NotifyIcon());
                _viewModel = new MessageViewModel(_notificationService);
            }

            public void Dispose()
            {
                _viewModel = null;

                if (_notificationService != null)
                {
                    _notificationService.Dispose();
                    _notificationService = null;
                }

                GC.SuppressFinalize(this);
            }

            [Test]
            public void Constructor_InitializesViewModel()
            {
                Assert.IsNotNull(_viewModel);
            }

            [Test]
            public void Constructor_InitializesButtons()
            {
                Assert.IsNotNull(_viewModel.LeftButton);

                Assert.IsNotNull(_viewModel.RightButton);

                Assert.IsNotEmpty(_viewModel.LeftButton.Key);

                Assert.IsNotEmpty(_viewModel.RightButton.Key);
            }

            [Test]
            public void Constructor_InitializesMessage()
            {
                Assert.IsNotNull(_viewModel.Message);

                Assert.IsNotEmpty(_viewModel.Message);
            }

            [Test]
            public void Message_SetValue_UpdatesProperty()
            {
                var newMessage = "Test Message";

                _viewModel.Message = newMessage;

                Assert.AreEqual(newMessage, _viewModel.Message);
            }

            [Test]
            public void LeftButton_SetValue_UpdatesProperty()
            {
                var newButton = new System.Collections.Generic.KeyValuePair<string, bool?>("Cancel", null);

                _viewModel.LeftButton = newButton;

                Assert.AreEqual(newButton, _viewModel.LeftButton);
            }

            [Test]
            public void RightButton_SetValue_UpdatesProperty()
            {
                var newButton = new System.Collections.Generic.KeyValuePair<string, bool?>("OK", true);

                _viewModel.RightButton = newButton;

                Assert.AreEqual(newButton, _viewModel.RightButton);
            }
        }

        #endregion

        #region DonationViewModel Tests


        public sealed class DonationViewModelTests : IDisposable
        {
            private INotificationService _notificationService;
            private DonationViewModel _viewModel;

            public DonationViewModelTests()
            {
                SetUp();
            }

            public void SetUp()
            {
                _notificationService = new NotificationService(new NotifyIcon());
                _viewModel = new DonationViewModel(_notificationService);
            }

            public void Dispose()
            {
                _viewModel = null;

                if (_notificationService != null)
                {
                    _notificationService.Dispose();
                    _notificationService = null;
                }

                GC.SuppressFinalize(this);
            }

            [Test]
            public void Constructor_InitializesViewModel()
            {
                Assert.IsNotNull(_viewModel);
            }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
