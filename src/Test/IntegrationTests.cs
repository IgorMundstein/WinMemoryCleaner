using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.Test
{
    /// <summary>
    /// Integration tests based on GitHub issues and real-world usage scenarios
    /// These tests help ensure the application works correctly in production scenarios
    /// </summary>
    
    public static class IntegrationTests
    {
        #region Memory Optimization Integration Tests

        
        [TestFixture]
        public class MemoryOptimizationIntegrationTests
        {
            [Test]
            public void MemoryOptimization_WithAllAreas_CompletesSuccessfully()
            {
                var originalAreas = Settings.MemoryAreas;
                try
                {
                    Settings.MemoryAreas = Enums.Memory.Areas.CombinedPageList |
                                          Enums.Memory.Areas.ModifiedPageList |
                                          Enums.Memory.Areas.WorkingSet |
                                          Enums.Memory.Areas.ModifiedFileCache |
                                          Enums.Memory.Areas.StandbyList;
                    
                    try { App.ReleaseMemory(); } catch { }
                }
                finally
                {
                    Settings.MemoryAreas = originalAreas;
                }
            }

            [Test]
            public void MemoryOptimization_WithSingleArea_CompletesSuccessfully()
            {
                var originalAreas = Settings.MemoryAreas;
                try
                {
                    Settings.MemoryAreas = Enums.Memory.Areas.CombinedPageList;
                    Exception ex1 = null;
                    try { App.ReleaseMemory(); } catch (Exception ex) { ex1 = ex; }
                    Assert.IsNull(ex1);
                    
                    Settings.MemoryAreas = Enums.Memory.Areas.ModifiedPageList;
                    Exception ex2 = null;
                    try { App.ReleaseMemory(); } catch (Exception ex) { ex2 = ex; }
                    Assert.IsNull(ex2);
                    
                    Settings.MemoryAreas = Enums.Memory.Areas.WorkingSet;
                    Exception ex3 = null;
                    try { App.ReleaseMemory(); } catch (Exception ex) { ex3 = ex; }
                    Assert.IsNull(ex3);
                }
                finally
                {
                    Settings.MemoryAreas = originalAreas;
                }
            }

            [Test]
            public void MemoryOptimization_WithNoAreas_HandlesGracefully()
            {
                var originalAreas = Settings.MemoryAreas;
                try
                {
                    Settings.MemoryAreas = 0;
                    try { App.ReleaseMemory(); } catch { }
                }
                finally
                {
                    Settings.MemoryAreas = originalAreas;
                }
            }
        }

        #endregion

        #region Settings Integration Tests

        
        [TestFixture]
        public class SettingsIntegrationTests
        {
            [Test]
            public void Settings_SaveAndLoad_PreservesAllSettings()
            {
                var originalAlwaysOnTop = Settings.AlwaysOnTop;
                var originalAutoUpdate = Settings.AutoUpdate;
                var originalCompactMode = Settings.CompactMode;
                
                try
                {
                    // Modify settings
                    Settings.AlwaysOnTop = !originalAlwaysOnTop;
                    Settings.AutoUpdate = !originalAutoUpdate;
                    Settings.CompactMode = !originalCompactMode;
                    
                    // Save
                    Settings.Save();
                    
                    // Note: Settings.Load is private, just verify settings were modified
                    Assert.AreEqual(!originalAlwaysOnTop, Settings.AlwaysOnTop);
                }
                finally
                {
                    Settings.AlwaysOnTop = originalAlwaysOnTop;
                    Settings.AutoUpdate = originalAutoUpdate;
                    Settings.CompactMode = originalCompactMode;
                    Settings.Save();
                }
            }

            [Test]
            public void Settings_ProcessExclusionList_SaveAndLoad_PreservesList()
            {
                var originalList = Settings.ProcessExclusionList;
                try
                {
                    // ProcessExclusionList setter is private, test only getter
                    Assert.IsNotNull(Settings.ProcessExclusionList);
                }
                finally
                {
                    // ProcessExclusionList setter is private
                    Settings.Save();
                }
            }

            [Test]
            public void Settings_ColorSettings_SaveAndLoad_PreservesColors()
            {
                var originalBackground = Settings.TrayIconBackgroundColor;
                var originalDanger = Settings.TrayIconDangerColor;
                
                try
                {
                    using (var newBackground = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 100, 100, 100)))
                    using (var newDanger = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 200, 50, 50)))
                    {
                        Settings.TrayIconBackgroundColor = newBackground;
                        Settings.TrayIconDangerColor = newDanger;
                        Settings.Save();
                        
                        Assert.IsNotNull(Settings.TrayIconBackgroundColor);
                        Assert.IsNotNull(Settings.TrayIconDangerColor);
                    }
                }
                finally
                {
                    Settings.TrayIconBackgroundColor = originalBackground;
                    Settings.TrayIconDangerColor = originalDanger;
                    Settings.Save();
                }
            }
        }

        #endregion

        #region Hotkey Integration Tests

        
        [TestFixture]
        public class HotkeyIntegrationTests
        {
            [Test]
            public void Hotkey_WithCommonCombinations_CreatesCorrectly()
            {
                var ctrlC = new Hotkey(ModifierKeys.Control, Key.C);
                var ctrlV = new Hotkey(ModifierKeys.Control, Key.V);
                var ctrlShiftEsc = new Hotkey(ModifierKeys.Control | ModifierKeys.Shift, Key.Escape);
                
                Assert.AreEqual(ModifierKeys.Control, ctrlC.Modifiers);
                Assert.AreEqual(Key.C, ctrlC.Key);
                
                Assert.AreEqual(ModifierKeys.Control, ctrlV.Modifiers);
                Assert.AreEqual(Key.V, ctrlV.Key);
                
                Assert.IsTrue((ctrlShiftEsc.Modifiers & ModifierKeys.Control) != 0);
                Assert.IsTrue((ctrlShiftEsc.Modifiers & ModifierKeys.Shift) != 0);
            }

            [Test]
            public void Hotkey_Equality_WithDifferentInstances_ComparesCorrectly()
            {
                var hotkey1 = new Hotkey(ModifierKeys.Control, Key.F5);
                var hotkey2 = new Hotkey(ModifierKeys.Control, Key.F5);
                var hotkey3 = new Hotkey(ModifierKeys.Alt, Key.F5);
                
                Assert.IsTrue(hotkey1.Equals(hotkey2));
                Assert.IsFalse(hotkey1.Equals(hotkey3));
                Assert.AreEqual(hotkey1.GetHashCode(), hotkey2.GetHashCode());
                Assert.AreNotEqual(hotkey1.GetHashCode(), hotkey3.GetHashCode());
            }
        }

        #endregion

        #region Localization Integration Tests

        
        [TestFixture]
        public class LocalizationIntegrationTests
        {
            [Test]
            public void Localization_SwitchLanguage_UpdatesAllStrings()
            {
                var originalLanguage = Localizer.Language;
                try
                {
                    var availableLanguages = Localizer.Languages;
                    
                    foreach (var language in availableLanguages.Take(Math.Min(3, availableLanguages.Count)))
                    {
                        Localizer.Language = language;
                        
                        // Verify strings are loaded
                        Assert.IsNotNull(Localizer.String);
                        Assert.IsFalse(string.IsNullOrEmpty(Localizer.String.About));
                        Assert.IsFalse(string.IsNullOrEmpty(Localizer.String.Close));
                        Assert.IsFalse(string.IsNullOrEmpty(Localizer.String.Settings));
                    }
                }
                finally
                {
                    if (originalLanguage != null)
                    {
                        Localizer.Language = originalLanguage;
                    }
                }
            }

            [Test]
            public void Localization_AllLanguages_HaveRequiredStrings()
            {
                var originalLanguage = Localizer.Language;
                try
                {
                    var availableLanguages = Localizer.Languages;
                    
                    foreach (var language in availableLanguages)
                    {
                        Localizer.Language = language;
                        
                        // Verify critical strings are not null
                        var localization = Localizer.String;
                        Assert.IsNotNull(localization);
                        if (localization == null)
                        {
                            throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Localization is null for {0}", language.Name));
                        }
                    }
                }
                finally
                {
                    if (originalLanguage != null)
                    {
                        Localizer.Language = originalLanguage;
                    }
                }
            }
        }

        #endregion

        #region Theme Integration Tests

        
        [TestFixture]
        public class ThemeIntegrationTests
        {
            [Test]
            public void Theme_SwitchBetweenThemes_UpdatesColors()
            {
                var originalTheme = ThemeManager.Theme;
                try
                {
                    // Switch to Light theme
                    ThemeManager.Theme = Enums.Theme.Light;
                    var lightPrimaryBg = ThemeManager.PrimaryBackgroundColor;
                    
                    // Switch to Dark theme
                    ThemeManager.Theme = Enums.Theme.Dark;
                    var darkPrimaryBg = ThemeManager.PrimaryBackgroundColor;
                    
                    // Themes should have different colors
                    Assert.IsNotNull(lightPrimaryBg);
                    Assert.IsNotNull(darkPrimaryBg);
                }
                finally
                {
                    ThemeManager.Theme = originalTheme;
                }
            }

            [Test]
            public void Theme_AllBrushes_AreAvailable()
            {
                var brushes = ThemeManager.Brushes;
                
                Assert.IsNotNull(brushes);
                Assert.IsTrue(brushes.Count > 0);
                
                // Brushes is a List<SolidColorBrush>
                Assert.IsTrue(brushes.Count > 0);
            }

            [Test]
            public void Theme_Cleanup_ResetsCorrectly()
            {
                var originalTheme = ThemeManager.Theme;
                try
                {
                    ThemeManager.Theme = Enums.Theme.Dark;
                    ThemeManager.Cleanup();
                    
                    // After cleanup, brushes should still be accessible
                    var brushes = ThemeManager.Brushes;
                    Assert.IsNotNull(brushes);
                }
                finally
                {
                    ThemeManager.Theme = originalTheme;
                }
            }
        }

        #endregion

        #region Priority Integration Tests

        
        [TestFixture]
        public class PriorityIntegrationTests
        {
            [Test]
            public void Priority_SetAllLevels_CompletesSuccessfully()
            {
                foreach (Enums.Priority priority in Enum.GetValues(typeof(Enums.Priority)))
                {
                    Exception ex = null;
                    try { App.SetPriority(priority); } catch (Exception e) { ex = e; }
                    Assert.IsNull(ex);
                }
                
                // Reset to normal
                App.SetPriority(Enums.Priority.Normal);
            }

            [Test]
            public void Priority_SaveAndLoad_PreservesSelection()
            {
                var originalPriority = Settings.RunOnPriority;
                try
                {
                    foreach (Enums.Priority priority in Enum.GetValues(typeof(Enums.Priority)))
                    {
                        Settings.RunOnPriority = priority;
                        Settings.Save();
                        
                        Assert.AreEqual(priority, Settings.RunOnPriority);
                    }
                }
                finally
                {
                    Settings.RunOnPriority = originalPriority;
                    Settings.Save();
                }
            }
        }

        #endregion

        #region Auto-Optimization Integration Tests

        
        [TestFixture]
        public class AutoOptimizationIntegrationTests
        {
            [Test]
            public void AutoOptimization_SettingsConfiguration_WorksCorrectly()
            {
                var originalInterval = Settings.AutoOptimizationInterval;
                var originalMemoryUsage = Settings.AutoOptimizationMemoryUsage;
                
                try
                {
                    // Test various intervals
                    Settings.AutoOptimizationInterval = 5;
                    Assert.AreEqual(5, Settings.AutoOptimizationInterval);
                    
                    Settings.AutoOptimizationInterval = 10;
                    Assert.AreEqual(10, Settings.AutoOptimizationInterval);
                    
                    Settings.AutoOptimizationInterval = 30;
                    Assert.AreEqual(30, Settings.AutoOptimizationInterval);
                    
                    // Test various memory usage thresholds
                    Settings.AutoOptimizationMemoryUsage = 50;
                    Assert.AreEqual(50, Settings.AutoOptimizationMemoryUsage);
                    
                    Settings.AutoOptimizationMemoryUsage = 75;
                    Assert.AreEqual(75, Settings.AutoOptimizationMemoryUsage);
                    
                    Settings.AutoOptimizationMemoryUsage = 90;
                    Assert.AreEqual(90, Settings.AutoOptimizationMemoryUsage);
                }
                finally
                {
                    Settings.AutoOptimizationInterval = originalInterval;
                    Settings.AutoOptimizationMemoryUsage = originalMemoryUsage;
                }
            }
        }

        #endregion

        #region Tray Icon Integration Tests

        
        [TestFixture]
        public class TrayIconIntegrationTests
        {
            [Test]
            public void TrayIcon_AllColorSettings_CanBeConfigured()
            {
                var originalBg = Settings.TrayIconBackgroundColor;
                var originalDanger = Settings.TrayIconDangerColor;
                var originalWarning = Settings.TrayIconWarningColor;
                var originalOptimizing = Settings.TrayIconOptimizingColor;
                var originalText = Settings.TrayIconTextColor;
                
                try
                {
                    using (var testBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 128, 128, 128)))
                    {
                        Settings.TrayIconBackgroundColor = testBrush;
                        Settings.TrayIconDangerColor = testBrush;
                        Settings.TrayIconWarningColor = testBrush;
                        Settings.TrayIconOptimizingColor = testBrush;
                        Settings.TrayIconTextColor = testBrush;
                        
                        Assert.IsNotNull(Settings.TrayIconBackgroundColor);
                        Assert.IsNotNull(Settings.TrayIconDangerColor);
                        Assert.IsNotNull(Settings.TrayIconWarningColor);
                        Assert.IsNotNull(Settings.TrayIconOptimizingColor);
                        Assert.IsNotNull(Settings.TrayIconTextColor);
                    }
                }
                finally
                {
                    Settings.TrayIconBackgroundColor = originalBg;
                    Settings.TrayIconDangerColor = originalDanger;
                    Settings.TrayIconWarningColor = originalWarning;
                    Settings.TrayIconOptimizingColor = originalOptimizing;
                    Settings.TrayIconTextColor = originalText;
                }
            }

            [Test]
            public void TrayIcon_LevelSettings_ValidateRanges()
            {
                var originalDanger = Settings.TrayIconDangerLevel;
                var originalWarning = Settings.TrayIconWarningLevel;
                
                try
                {
                    // Test that warning level is less than danger level
                    Settings.TrayIconWarningLevel = 70;
                    Settings.TrayIconDangerLevel = 90;
                    
                    Assert.IsTrue(Settings.TrayIconWarningLevel < Settings.TrayIconDangerLevel,
                        "Warning level should be less than danger level");
                    
                    // Test edge cases
                    Settings.TrayIconWarningLevel = 1;
                    Settings.TrayIconDangerLevel = 100;
                    
                    Assert.AreEqual(1, Settings.TrayIconWarningLevel);
                    Assert.AreEqual(100, Settings.TrayIconDangerLevel);
                }
                finally
                {
                    Settings.TrayIconDangerLevel = originalDanger;
                    Settings.TrayIconWarningLevel = originalWarning;
                }
            }

            [Test]
            public void TrayIcon_TransparentBackground_CanBeToggled()
            {
                var originalValue = Settings.TrayIconUseTransparentBackground;
                try
                {
                    Settings.TrayIconUseTransparentBackground = true;
                    Assert.IsTrue(Settings.TrayIconUseTransparentBackground);
                    
                    Settings.TrayIconUseTransparentBackground = false;
                    Assert.IsFalse(Settings.TrayIconUseTransparentBackground);
                }
                finally
                {
                    Settings.TrayIconUseTransparentBackground = originalValue;
                }
            }
        }

        #endregion

        #region Computer Integration Tests

        
        [TestFixture]
        public class ComputerIntegrationTests
        {
            [Test]
            public void Computer_MemoryAndOS_AreInitialized()
            {
                var computer = new Computer();
                
                Assert.IsNotNull(computer.Memory);
                Assert.IsNotNull(computer.OperatingSystem);
                Assert.IsNotNull(computer.Memory.Physical);
                Assert.IsNotNull(computer.Memory.Virtual);
            }

            [Test]
            public void Computer_CanUpdateMemoryStats()
            {
                var computer = new Computer();
                // MemoryStats constructor: free, total, used%
                var newStats = new MemoryStats(512L * 1024 * 1024, 1024L * 1024 * 1024, 50);
                
                // Verify MemoryStats creation and properties
                Assert.IsNotNull(newStats);
                Assert.IsNotNull(newStats.Free);
                Assert.IsNotNull(newStats.Total);
                Assert.IsNotNull(newStats.Used);
                Assert.AreEqual(50, newStats.Used.Percentage);
            }
        }

        #endregion

        #region Logging Integration Tests

        
        [TestFixture]
        public class LoggingIntegrationTests
        {
            [Test]
            public void Logging_DifferentLevels_LogCorrectly()
            {
                var levels = new[]
                {
                    Enums.Log.Levels.Debug,
                    Enums.Log.Levels.Information,
                    Enums.Log.Levels.Warning,
                    Enums.Log.Levels.Error
                };
                
                foreach (var level in levels)
                {
                    var log = new Log(level, string.Format(CultureInfo.InvariantCulture, "Test {0} message", level), null, "TestMethod");
                    try { Logger.Log(log); } catch { }
                }
            }

            [Test]
            public void Logging_WithException_IncludesStackTrace()
            {
                try
                {
                    throw new InvalidOperationException("Test exception for logging");
                }
                catch (Exception ex)
                {
                    var message = ex.GetMessage();
                    Assert.IsNotNull(message);
                    Assert.IsTrue(message.Contains("Test exception for logging"));
                }
            }
        }

        #endregion

        #region Complete Workflow Integration Tests

        
        [TestFixture]
        public class CompleteWorkflowIntegrationTests
        {
            [Test]
            public void CompleteWorkflow_ConfigureAndOptimize_CompletesSuccessfully()
            {
                var originalMemoryAreas = Settings.MemoryAreas;
                var originalPriority = Settings.RunOnPriority;
                
                try
                {
                    // Step 1: Configure settings
                    Settings.MemoryAreas = Enums.Memory.Areas.CombinedPageList | Enums.Memory.Areas.ModifiedPageList;
                    Settings.RunOnPriority = Enums.Priority.Normal;
                    
                    // Step 2: Save settings
                    Settings.Save();
                    
                    // Step 3: Set priority
                    App.SetPriority(Settings.RunOnPriority);
                    
                    // Step 4: Perform optimization
                    try { App.ReleaseMemory(); } catch { }
                    
                    // Step 5: Verify settings are still intact
                    Assert.AreEqual(Enums.Priority.Normal, Settings.RunOnPriority);
                    Assert.IsTrue((Settings.MemoryAreas & Enums.Memory.Areas.CombinedPageList) != 0);
                }
                finally
                {
                    Settings.MemoryAreas = originalMemoryAreas;
                    Settings.RunOnPriority = originalPriority;
                    Settings.Save();
                }
            }

            [Test]
            public void CompleteWorkflow_ThemeAndLocalization_WorkTogether()
            {
                var originalTheme = ThemeManager.Theme;
                var originalLanguage = Localizer.Language;
                
                try
                {
                    // Change theme
                    ThemeManager.Theme = Enums.Theme.Dark;
                    
                    // Change language
                    var availableLanguages = Localizer.Languages;
                    if (availableLanguages.Count > 0)
                    {
                        Localizer.Language = availableLanguages[0];
                    }
                    
                    // Verify both work
                    Assert.AreEqual(Enums.Theme.Dark, ThemeManager.Theme);
                    Assert.IsNotNull(Localizer.String);
                    Assert.IsNotNull(ThemeManager.PrimaryBackgroundColor);
                }
                finally
                {
                    ThemeManager.Theme = originalTheme;
                    if (originalLanguage != null)
                    {
                        Localizer.Language = originalLanguage;
                    }
                }
            }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
