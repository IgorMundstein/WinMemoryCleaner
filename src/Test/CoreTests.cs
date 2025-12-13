using NUnit.Framework;
using System;
using System.ComponentModel;
using System.IO;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.Test
{
    
    public static class CoreTests
    {
        #region Constants Tests

        [TestFixture]
        public class ConstantsTests
        {
            [Test]
            public void App_AutoOptimizationMemoryUsageInterval_Is5Minutes()
            {
                Assert.AreEqual(5, Constants.App.AutoOptimizationMemoryUsageInterval);
            }

            [Test]
            public void App_AutoUpdateInterval_Is24Hours()
            {
                Assert.AreEqual(24, Constants.App.AutoUpdateInterval);
            }

            [Test]
            public void App_Author_Name_IsNotEmpty()
            {
                Assert.IsNotNull(Constants.App.Author.Name);
                Assert.IsFalse(string.IsNullOrEmpty(Constants.App.Author.Name));
                Assert.AreEqual("Igor Mundstein", Constants.App.Author.Name);
            }

            [Test]
            public void App_CommandLineArgument_Install_IsCorrect()
            {
                Assert.AreEqual("Install", Constants.App.CommandLineArgument.Install);
            }

            [Test]
            public void App_CommandLineArgument_Reset_IsCorrect()
            {
                Assert.AreEqual("Reset", Constants.App.CommandLineArgument.Reset);
            }

            [Test]
            public void App_CommandLineArgument_Service_IsCorrect()
            {
                Assert.AreEqual("Service", Constants.App.CommandLineArgument.Service);
            }

            [Test]
            public void App_CommandLineArgument_Uninstall_IsCorrect()
            {
                Assert.AreEqual("Uninstall", Constants.App.CommandLineArgument.Uninstall);
            }

            [Test]
            public void App_Donation_BitcoinUri_IsValid()
            {
                Assert.IsNotNull(Constants.App.Donation.BitcoinUri);
                Assert.IsTrue(Constants.App.Donation.BitcoinUri.IsAbsoluteUri);
            }

            [Test]
            public void App_Donation_EthereumUri_IsValid()
            {
                Assert.IsNotNull(Constants.App.Donation.EthereumUri);
                Assert.IsTrue(Constants.App.Donation.EthereumUri.IsAbsoluteUri);
            }

            [Test]
            public void App_Donation_GitHubSponsorUri_IsValid()
            {
                Assert.IsNotNull(Constants.App.Donation.GitHubSponsorUri);
                Assert.IsTrue(Constants.App.Donation.GitHubSponsorUri.IsAbsoluteUri);
            }

            [Test]
            public void App_Donation_KofiUri_IsValid()
            {
                Assert.IsNotNull(Constants.App.Donation.KofiUri);
                Assert.IsTrue(Constants.App.Donation.KofiUri.IsAbsoluteUri);
            }

            [Test]
            public void App_Id_IsValidGuid()
            {
                var guid = Guid.Parse(Constants.App.Id);

                Assert.IsNotNull(guid);
                Assert.AreNotEqual(Guid.Empty, guid);
            }

            [Test]
            public void App_License_IsGPL3()
            {
                Assert.AreEqual("GPL-3.0", Constants.App.License);
            }

            [Test]
            public void App_Name_IsNotEmpty()
            {
                Assert.IsNotNull(Constants.App.Name);
                Assert.IsFalse(string.IsNullOrEmpty(Constants.App.Name));
                Assert.AreEqual("WinMemoryCleaner", Constants.App.Name);
            }

            [Test]
            public void App_Repository_AboutUri_IsValid()
            {
                Assert.IsNotNull(Constants.App.Repository.AboutUri);
                Assert.IsTrue(Constants.App.Repository.AboutUri.IsAbsoluteUri);
            }

            [Test]
            public void App_Repository_DownloadUri_IsValid()
            {
                Assert.IsNotNull(Constants.App.Repository.DownloadUri);
                Assert.IsTrue(Constants.App.Repository.DownloadUri.IsAbsoluteUri);
            }

            [Test]
            public void App_Repository_LatestExeUri_IsValid()
            {
                Assert.IsNotNull(Constants.App.Repository.LatestExeUri);
                Assert.IsTrue(Constants.App.Repository.LatestExeUri.IsAbsoluteUri);
                Assert.IsTrue(Constants.App.Repository.LatestExeUri.ToString().Contains("releases/latest"));
            }

            [Test]
            public void App_Repository_Uri_IsValid()
            {
                Assert.IsNotNull(Constants.App.Repository.Uri);
                Assert.IsTrue(Constants.App.Repository.Uri.IsAbsoluteUri);
                Assert.IsTrue(Constants.App.Repository.Uri.ToString().Contains("github.com"));
            }

            [Test]
            public void App_Title_IsNotEmpty()
            {
                Assert.IsNotNull(Constants.App.Title);
                Assert.IsFalse(string.IsNullOrEmpty(Constants.App.Title));
                Assert.AreEqual("Windows Memory Cleaner", Constants.App.Title);
            }

            [Test]
            public void App_VersionFormat_IsValid()
            {
                Assert.AreEqual("{0}.{1}.{2}", Constants.App.VersionFormat);
            }

            [Test]
            public void Windows_Console_AttachParentProcess_IsNegativeOne()
            {
                Assert.AreEqual(-1, Constants.Windows.Console.AttachParentProcess);
            }

            [Test]
            public void Windows_Console_StdOutputHandle_IsNegativeEleven()
            {
                Assert.AreEqual(-11, Constants.Windows.Console.StdOutputHandle);
            }

            [Test]
            public void Windows_Privilege_SeDebugName_IsCorrect()
            {
                Assert.AreEqual("SeDebugPrivilege", Constants.Windows.Privilege.SeDebugName);
            }

            [Test]
            public void Windows_Privilege_SeIncreaseQuotaName_IsCorrect()
            {
                Assert.AreEqual("SeIncreaseQuotaPrivilege", Constants.Windows.Privilege.SeIncreaseQuotaName);
            }

            [Test]
            public void Windows_PrivilegeAttribute_Enabled_IsTwo()
            {
                Assert.AreEqual(2, Constants.Windows.PrivilegeAttribute.Enabled);
            }

            [Test]
            public void Windows_SystemErrorCode_ErrorAccessDenied_IsFive()
            {
                Assert.AreEqual(5, Constants.Windows.SystemErrorCode.ErrorAccessDenied);
            }

            [Test]
            public void Windows_SystemErrorCode_ErrorSuccess_IsZero()
            {
                Assert.AreEqual(0, Constants.Windows.SystemErrorCode.ErrorSuccess);
            }
        }

        #endregion

        #region ExtensionMethods Tests

        [TestFixture]
        public class ExtensionMethodsTests
        {
            [Test]
            public void Capitalize_WithEmptyString_ReturnsEmpty()
            {
                string text = string.Empty;
                var result = text.Capitalize();

                Assert.AreEqual(string.Empty, result);
            }

            [Test]
            public void Capitalize_WithException_ReturnsOriginal()
            {
                string text = null;
                var result = text.Capitalize();

                Assert.AreEqual(null, result);
            }

            [Test]
            public void Capitalize_WithMultipleSentences_CapitalizesCorrectly()
            {
                string text = "first sentence. second sentence.";
                var result = text.Capitalize();

                Assert.IsNotNull(result);
                Assert.IsTrue(result.StartsWith("F", System.StringComparison.Ordinal));
            }

            [Test]
            public void Capitalize_WithNormalText_CapitalizesFirstLetter()
            {
                string text = "hello world";
                var result = text.Capitalize();

                Assert.IsNotNull(result);
                Assert.IsTrue(result.StartsWith("H", System.StringComparison.Ordinal));
            }

            [Test]
            public void DefaultValue_WithNullableType_ReturnsNull()
            {
                var type = typeof(int?);
                var result = type.DefaultValue();

                Assert.IsNull(result);
            }

            [Test]
            public void DefaultValue_WithReferenceType_ReturnsNull()
            {
                var type = typeof(string);
                var result = type.DefaultValue();

                Assert.IsNull(result);
            }

            [Test]
            public void DefaultValue_WithValueType_ReturnsDefault()
            {
                var type = typeof(int);
                var result = type.DefaultValue();

                Assert.IsNotNull(result);
                Assert.AreEqual(0, result);
            }

            [Test]
            public void GetHex_WithBrush_ReturnsHexCode()
            {
                using (var brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
                {
                    var hex = brush.GetHex();

                    Assert.IsNotNull(hex);
                    Assert.IsTrue(hex.StartsWith("#", System.StringComparison.Ordinal));
                }
            }

            [Test]
            public void GetHex_WithBrushAndAlpha_ReturnsHexCodeWithAlpha()
            {
                using (var brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(128, 255, 0, 0)))
                {
                    var hex = brush.GetHex(true);

                    Assert.IsNotNull(hex);
                    Assert.IsTrue(hex.StartsWith("#", System.StringComparison.Ordinal));
                    Assert.AreEqual(9, hex.Length); // #AARRGGBB
                }
            }

            [Test]
            public void GetHex_WithColor_ReturnsHexCode()
            {
                var color = System.Drawing.Color.Blue;
                var hex = color.GetHex();

                Assert.IsNotNull(hex);
                Assert.AreEqual("#0000FF", hex);
            }

            [Test]
            public void GetHex_WithMediaColor_ReturnsHexCode()
            {
                var color = System.Windows.Media.Color.FromRgb(0, 255, 0);
                var hex = color.GetHex();

                Assert.IsNotNull(hex);
                Assert.AreEqual("#00FF00", hex);
            }

            [Test]
            public void GetKeyValue_WithNo_ReturnsCorrectKeyValue()
            {
                var button = Enums.Dialog.Button.No;
                var result = button.GetKeyValue();

                Assert.AreEqual(Localizer.String.No, result.Key);
                Assert.AreEqual(false, result.Value);
            }

            [Test]
            public void GetKeyValue_WithNone_ReturnsNullKeyValue()
            {
                var button = Enums.Dialog.Button.None;
                var result = button.GetKeyValue();

                Assert.IsNull(result.Key);
                Assert.IsNull(result.Value);
            }

            [Test]
            public void GetKeyValue_WithYes_ReturnsCorrectKeyValue()
            {
                var button = Enums.Dialog.Button.Yes;
                var result = button.GetKeyValue();

                Assert.AreEqual(Localizer.String.Yes, result.Key);
                Assert.AreEqual(true, result.Value);
            }

            [Test]
            public void GetMessage_WithInnerException_ReturnsAllMessages()
            {
                var innerException = new InvalidOperationException("Inner exception");
                var exception = new Exception("Outer exception", innerException);
                var message = exception.GetMessage();

                Assert.IsNotNull(message);
                Assert.IsTrue(message.Contains("Outer exception"));
                Assert.IsTrue(message.Contains("Inner exception"));
            }

            [Test]
            public void GetMessage_WithMultipleInnerExceptions_ReturnsDeduplicatedMessages()
            {
                var ex1 = new Exception("Message 1");
                var ex2 = new Exception("Message 2", ex1);
                var ex3 = new Exception("Message 1", ex2); // Duplicate message
                var message = ex3.GetMessage();

                Assert.IsNotNull(message);
            }

            [Test]
            public void GetMessage_WithNullException_ReturnsNull()
            {
                Exception exception = null;
                var message = exception.GetMessage();

                Assert.IsNull(message);
            }

            [Test]
            public void GetMessage_WithException_ReturnsMessage()
            {
                var exception = new Exception("Test exception message");
                var message = exception.GetMessage();

                Assert.IsNotNull(message);
                Assert.IsTrue(message.Contains("Test exception message"));
            }

            [Test]
            public void GetString_WithLowMemoryReason_ReturnsCorrectString()
            {
                var reason = Enums.Memory.Optimization.Reason.LowMemory;
                var result = reason.GetString();

                Assert.AreEqual(Localizer.String.LowMemory, result);
            }

            [Test]
            public void GetString_WithManualReason_ReturnsCorrectString()
            {
                var reason = Enums.Memory.Optimization.Reason.Manual;
                var result = reason.GetString();

                Assert.AreEqual(Localizer.String.Manual, result);
            }

            [Test]
            public void GetString_WithScheduleReason_ReturnsCorrectString()
            {
                var reason = Enums.Memory.Optimization.Reason.Schedule;
                var result = reason.GetString();

                Assert.AreEqual(Localizer.String.Schedule, result);
            }

            [Test]
            public void IsEquals_WithDifferentColors_ReturnsFalse()
            {
                var mediaColor = System.Windows.Media.Color.FromRgb(255, 0, 0);
                var drawingColor = System.Drawing.Color.Blue;
                var result = mediaColor.IsEquals(drawingColor);

                Assert.IsFalse(result);
            }

            [Test]
            public void IsEquals_WithMatchingColors_ReturnsTrue()
            {
                var mediaColor = System.Windows.Media.Color.FromArgb(255, 128, 64, 32);
                var drawingColor = System.Drawing.Color.FromArgb(255, 128, 64, 32);
                var result = mediaColor.IsEquals(drawingColor);

                Assert.IsTrue(result);
            }

            [Test]
            public void IsEquals_WithNullMediaColor_ReturnsFalse()
            {
                System.Windows.Media.Color? mediaColor = null;
                var drawingColor = System.Drawing.Color.Red;
                if (mediaColor.HasValue)
                {
                    var result = mediaColor.Value.IsEquals(drawingColor);

                    Assert.IsFalse(result);
                }
            }

            [Test]
            public void IsNumber_WithByte_ReturnsTrue()
            {
                byte value = 100;
                var result = value.IsNumber();

                Assert.IsTrue(result);
            }

            [Test]
            public void IsNumber_WithDecimal_ReturnsTrue()
            {
                decimal value = 123.45m;
                var result = value.IsNumber();

                Assert.IsTrue(result);
            }

            [Test]
            public void IsNumber_WithDouble_ReturnsTrue()
            {
                double value = 123.45;
                var result = value.IsNumber();

                Assert.IsTrue(result);
            }

            [Test]
            public void IsNumber_WithInt_ReturnsTrue()
            {
                int value = 123;
                var result = value.IsNumber();

                Assert.IsTrue(result);
            }

            [Test]
            public void IsNumber_WithNull_ReturnsFalse()
            {
                object value = null;
                var result = value.IsNumber();

                Assert.IsFalse(result);
            }

            [Test]
            public void IsNumber_WithString_ReturnsFalse()
            {
                object value = "not a number";
                var result = value.IsNumber();

                Assert.IsFalse(result);
            }

            [Test]
            public void IsValid_WithInvalidEnum_ReturnsFalse()
            {
                var invalidEnum = (Enums.Priority)999;
                var result = invalidEnum.IsValid();

                Assert.IsFalse(result);
            }

            [Test]
            public void IsValid_WithNullEnum_ReturnsFalse()
            {
                Enum nullEnum = null;
                var result = nullEnum.IsValid();

                Assert.IsFalse(result);
            }

            [Test]
            public void IsValid_WithValidEnum_ReturnsTrue()
            {
                var validEnum = Enums.Priority.Normal;
                var result = validEnum.IsValid();

                Assert.IsTrue(result);
            }

            [Test]
            public void RemoveWhitespaces_WithMixedWhitespace_RemovesAll()
            {
                string text = "  hello   world  \t\n";
                var result = text.RemoveWhitespaces();

                Assert.AreEqual("helloworld", result);
            }

            [Test]
            public void RemoveWhitespaces_WithNoWhitespace_ReturnsSame()
            {
                string text = "helloworld";
                var result = text.RemoveWhitespaces();

                Assert.AreEqual("helloworld", result);
            }

            [Test]
            public void ToBrush_WithInvalidHexString_ReturnsFallback()
            {
                string hex = "invalid";
                using (var fallback = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
                {
                    using (var result = hex.ToBrush(fallback))
                    {
                        Assert.AreEqual(fallback, result);
                    }
                }
            }

            [Test]
            public void ToBrush_WithNullMediaBrush_ReturnsNull()
            {
                System.Windows.Media.Brush mediaBrush = null;
                var result = mediaBrush.ToBrush();

                Assert.IsNull(result);
            }

            [Test]
            public void ToBrush_WithValidHexString_ReturnsCorrectBrush()
            {
                string hex = "#FF0000";
                using (var fallback = new System.Drawing.SolidBrush(System.Drawing.Color.Blue))
                using (var result = hex.ToBrush(fallback))
                {
                    Assert.IsNotNull(result);
                }
            }

            [Test]
            public void ToBrush_WithValidMediaBrush_ReturnsDrawingBrush()
            {
                var mediaBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 128, 64));
                using (var result = mediaBrush.ToBrush())
                {
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOf<System.Drawing.SolidBrush>(result);
                }
            }

            [Test]
            public void ToColor_WithMediaBrush_ReturnsDrawingColor()
            {
                var mediaBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 100, 50, 25));
                var result = mediaBrush.ToColor();

                Assert.AreEqual(200, result.A);
                Assert.AreEqual(100, result.R);
                Assert.AreEqual(50, result.G);
                Assert.AreEqual(25, result.B);
            }

            [Test]
            public void ToMemoryUnit_WithBytesValue_ConvertsCorrectly()
            {
                long bytes = 512;
                var result = bytes.ToMemoryUnit();

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Key > 0);
            }

            [Test]
            public void ToMemoryUnit_WithGigabytesValue_ConvertsCorrectly()
            {
                long bytes = 1073741824; // 1 GB
                var result = bytes.ToMemoryUnit();

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Key > 0);
            }

            [Test]
            public void ToMemoryUnit_WithKilobytesValue_ConvertsCorrectly()
            {
                long bytes = 2048; // 2 KB
                var result = bytes.ToMemoryUnit();

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Key > 0);
            }

            [Test]
            public void ToMemoryUnit_WithMegabytesValue_ConvertsCorrectly()
            {
                long bytes = 1048576; // 1 MB
                var result = bytes.ToMemoryUnit();

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Key > 0);
            }

            [Test]
            public void ToMemoryUnit_WithZeroValue_HandlesCorrectly()
            {
                long bytes = 0;
                var result = bytes.ToMemoryUnit();

                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Key);
            }
        }

        #endregion

        #region Helper Tests

        public sealed class HelperTests
        {
            private string _tempFilePath;

            [SetUp]
            public void SetUp()
            {
                _tempFilePath = Path.Combine(Path.GetTempPath(), "test_" + Guid.NewGuid() + ".txt");
            }

            [TearDown]
            public void TearDown()
            {
                if (File.Exists(_tempFilePath))
                {
                    try { File.Delete(_tempFilePath); } catch { }
                }
            }

            [Test]
            public void DeleteFile_WhenFileDoesNotExist_ReturnsFalse()
            {
                var nonExistentPath = Path.Combine(Path.GetTempPath(), "nonexistent_" + Guid.NewGuid() + ".txt");
                var result = Helper.DeleteFile(nonExistentPath);

                Assert.IsFalse(result);
            }

            [Test]
            public void DeleteFile_WhenFileExists_ReturnsTrue()
            {
                File.WriteAllText(_tempFilePath, "test content");

                Assert.IsTrue(File.Exists(_tempFilePath));
                var result = Helper.DeleteFile(_tempFilePath);

                Assert.IsTrue(result);

                Assert.IsFalse(File.Exists(_tempFilePath));
            }

            [Test]
            public void Deserialize_WithValidJsonString_DeserializesCorrectly()
            {
                var json = "{\"Name\":\"Test\",\"Value\":123}";
                var result = Helper.Deserialize<TestObject>(json);

                Assert.IsNotNull(result);
                Assert.AreEqual("Test", result.Name);
                Assert.AreEqual(123, result.Value);
            }

            [Test]
            public void GetExecutablePath_ReturnsNonEmptyString()
            {
                var path = Helper.GetExecutablePath();

                Assert.IsNotNull(path);
                Assert.IsFalse(string.IsNullOrEmpty(path));
            }

            [Test]
            public void GetVersion_ReturnsValidVersion()
            {
                var version = Helper.GetVersion();

                Assert.IsNotNull(version);
                Assert.IsTrue(version.Major >= 0);
                Assert.IsTrue(version.Minor >= 0);
            }

            [Test]
            public void IsAutoUpdateSupported_ReturnsBoolean()
            {
                var isSupported = Helper.IsAutoUpdateSupported;

                Assert.IsNotNull(isSupported);
            }

            [Test]
            public void NameOf_WithNullExpression_ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => Helper.NameOf<object>(null));
            }

            [Test]
            public void NameOf_WithValidExpression_ReturnsPropertyName()
            {
                var testObject = new { TestProperty = "value" };
                var name = Helper.NameOf(() => testObject.TestProperty);

                Assert.AreEqual("TestProperty", name);
            }

            [Test]
            public void Serialize_WithMinifiedTrue_ReturnsCompactJson()
            {
                var obj = new TestJsonSerializable { Name = "Test", Value = 123 };
                var json = Helper.Serialize(obj, true);

                Assert.IsNotNull(json);
                Assert.IsFalse(json.Contains("\r\n"));
            }

            [Test]
            public void Serialize_WithNullObject_ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => Helper.Serialize(null));
            }

            [Test]
            public void Serialize_WithValidObject_ReturnsFormattedJson()
            {
                var obj = new TestJsonSerializable { Name = "Test", Value = 123 };
                var json = Helper.Serialize(obj, false);

                Assert.IsNotNull(json);
                Assert.IsTrue(json.Contains("Name"));
                Assert.IsTrue(json.Contains("Test"));
            }

            [Test]
            public void ToHexCode_WithMaxValues_ReturnsWhiteHexCode()
            {
                var hexCode = Helper.ToHexCode(255, 255, 255);

                Assert.AreEqual("#FFFFFF", hexCode);
            }

            [Test]
            public void ToHexCode_WithRgbValues_ReturnsCorrectHexCode()
            {
                var hexCode = Helper.ToHexCode(255, 128, 64);

                Assert.AreEqual("#FF8040", hexCode);
            }

            [Test]
            public void ToHexCode_WithRgbaValues_ReturnsCorrectHexCodeWithAlpha()
            {
                var hexCode = Helper.ToHexCode(255, 128, 64, 200);

                Assert.AreEqual("#C8FF8040", hexCode);
            }

            [Test]
            public void ToHexCode_WithZeroValues_ReturnsBlackHexCode()
            {
                var hexCode = Helper.ToHexCode(0, 0, 0);

                Assert.AreEqual("#000000", hexCode);
            }
        }

        #endregion

        #region Localizer Tests

        [TestFixture]
        public class LocalizerTests
        {
            [Test]
            public void Localizer_Culture_IsNotNull()
            {
                var culture = Localizer.Culture;

                Assert.IsNotNull(culture);
            }

            [Test]
            public void Language_CanBeSet()
            {
                var originalLanguage = Localizer.Language;
                try
                {
                    Localizer.Language = Localizer.Languages.Find(language => language.Name == Constants.Windows.Locale.Name.English);

                    Assert.IsNotNull(Localizer.Language);

                    Localizer.Language = Localizer.Languages.Count > 0 ? Localizer.Languages[0] : originalLanguage; ;

                    Assert.IsNotNull(Localizer.Language);
                }
                finally
                {
                    Localizer.Language = originalLanguage;
                }
            }

            [Test]
            public void Languages_IsNotNull()
            {
                var languages = Localizer.Languages;

                Assert.IsNotNull(languages);
            }

            [Test]
            public void Languages_ContainsLanguages()
            {
                var languages = Localizer.Languages;

                Assert.IsTrue(languages.Count > 0);
            }

            [Test]
            public void String_IsNotNull()
            {
                var localization = Localizer.String;

                Assert.IsNotNull(localization);
            }
        }

        #endregion

        #region Logger Tests

        [TestFixture]
        public class LoggerTests
        {
            [Test]
            public void Debug_MultipleCalls_DoesNotThrow()
            {
                Assert.DoesNotThrow(() =>
                {
                    Logger.Debug("Message 1");
                    Logger.Debug("Message 2");
                    Logger.Debug("Message 3");
                });
            }

            [Test]
            public void Debug_WithEmptyMessage_DoesNotThrow()
            {
                Assert.DoesNotThrow(() => Logger.Debug(string.Empty));
            }

            [Test]
            public void Debug_WithException_DoesNotThrow()
            {
                var testException = new Exception("Test exception");

                Assert.DoesNotThrow(() => Logger.Debug(testException, "Test message"));
            }

            [Test]
            public void Debug_WithMessage_DoesNotThrow()
            {
                Assert.DoesNotThrow(() => Logger.Debug("Test debug message"));
            }

            [Test]
            public void Debug_WithNullMessage_DoesNotThrow()
            {
                Assert.DoesNotThrow(() => Logger.Debug(null, string.Empty));
            }

            [Test]
            public void Error_WithException_DoesNotThrow()
            {
                var testException = new Exception("Test error");

                Assert.DoesNotThrow(() => Logger.Error(testException));
            }

            [Test]
            public void Error_WithMessage_DoesNotThrow()
            {
                Assert.DoesNotThrow(() => Logger.Error("Test error message"));
            }

            [Test]
            public void Information_WithMessage_DoesNotThrow()
            {
                Assert.DoesNotThrow(() => Logger.Information("Test information message"));
            }

            [Test]
            public void Level_CanBeSet()
            {
                var originalLevel = Logger.Level;
                try
                {
                    Logger.Level = Enums.Log.Levels.Debug;

                    Assert.DoesNotThrow(() => Logger.Debug("Debug message"));

                    Logger.Level = Enums.Log.Levels.Information;

                    Assert.DoesNotThrow(() => Logger.Information("Info message"));

                    Logger.Level = Enums.Log.Levels.Warning;

                    Assert.DoesNotThrow(() => Logger.Warning("Warning message"));

                    Logger.Level = Enums.Log.Levels.Error;

                    Assert.DoesNotThrow(() => Logger.Error("Error message"));
                }
                finally
                {
                    Logger.Level = originalLevel;
                }
            }

            [Test]
            public void Log_WithNullLog_DoesNotThrow()
            {
                Assert.DoesNotThrow(() => Logger.Log(null));
            }

            [Test]
            public void Log_WithValidLog_DoesNotThrow()
            {
                var log = new Log(Enums.Log.Levels.Debug, "Test message", null, "TestMethod");

                Assert.DoesNotThrow(() => Logger.Log(log));
            }

            [Test]
            public void Warning_WithMessage_DoesNotThrow()
            {
                Assert.DoesNotThrow(() => Logger.Warning("Test warning message"));
            }
        }

        #endregion

        #region ObservableObject Tests

        [TestFixture]
        public class ObservableObjectTests
        {
            [Test]
            public void ImplementsINotifyPropertyChanged()
            {
                var obj = new TestObservableObject();

                Assert.IsInstanceOf<INotifyPropertyChanged>(obj);
            }

            [Test]
            public void OnPropertyChanged_MultipleTimes_RaisesEventEachTime()
            {
                var obj = new TestObservableObject();
                var eventCount = 0;
                obj.PropertyChanged += (sender, e) => eventCount++;
                obj.RaisePropertyChanged("Property1");
                obj.RaisePropertyChanged("Property2");
                obj.RaisePropertyChanged("Property3");

                Assert.AreEqual(3, eventCount);
            }

            [Test]
            public void OnPropertyChanged_RaisesPropertyChangedEvent()
            {
                var obj = new TestObservableObject();
                var eventRaised = false;
                string propertyName = null;
                obj.PropertyChanged += (sender, e) =>
                {
                    eventRaised = true;
                    propertyName = e.PropertyName;
                };
                obj.RaisePropertyChanged("TestProperty");

                Assert.IsTrue(eventRaised);

                Assert.AreEqual("TestProperty", propertyName);
            }

            [Test]
            public void OnPropertyChanged_WithEmptyPropertyName_RaisesEvent()
            {
                var obj = new TestObservableObject();
                var eventRaised = false;
                obj.PropertyChanged += (sender, e) => eventRaised = true;
                obj.RaisePropertyChanged(string.Empty);

                Assert.IsTrue(eventRaised);
            }

            [Test]
            public void OnPropertyChanged_WithNoSubscribers_DoesNotThrow()
            {
                var obj = new TestObservableObject();
                Assert.DoesNotThrow(() => obj.RaisePropertyChanged("TestProperty"));
            }

            [Test]
            public void OnPropertyChanged_WithNullPropertyName_RaisesEvent()
            {
                var obj = new TestObservableObject();
                var eventRaised = false;
                obj.PropertyChanged += (sender, e) => eventRaised = true;
                obj.RaisePropertyChanged(null);

                Assert.IsTrue(eventRaised);
            }

            [Test]
            public void PropertyChanged_MultipleSubscribers_NotifiesAll()
            {
                var obj = new TestObservableObject();
                var firstNotified = false;
                var secondNotified = false;
                obj.PropertyChanged += (sender, e) => firstNotified = true;
                obj.PropertyChanged += (sender, e) => secondNotified = true;
                obj.RaisePropertyChanged("TestProperty");

                Assert.IsTrue(firstNotified);

                Assert.IsTrue(secondNotified);
            }
        }

        #endregion

        #region Settings Tests

        [TestFixture]
        public class SettingsTests
        {
            [Test]
            public void AlwaysOnTop_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var alwaysOnTop = Settings.AlwaysOnTop; });
            }

            [Test]
            public void AutoOptimizationInterval_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var interval = Settings.AutoOptimizationInterval; });
            }

            [Test]
            public void AutoOptimizationMemoryUsage_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var usage = Settings.AutoOptimizationMemoryUsage; });
            }

            [Test]
            public void AutoUpdate_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var autoUpdate = Settings.AutoUpdate; });
            }

            [Test]
            public void CloseAfterOptimization_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var close = Settings.CloseAfterOptimization; });
            }

            [Test]
            public void CloseToTheNotificationArea_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var close = Settings.CloseToTheNotificationArea; });
            }

            [Test]
            public void CompactMode_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var compact = Settings.CompactMode; });
            }

            [Test]
            public void CreateStartMenuShortcut_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var create = Settings.CreateStartMenuShortcut; });
            }

            [Test]
            public void FontSize_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var fontSize = Settings.FontSize; });
            }

            [Test]
            public void Language_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var language = Settings.Language; });
            }

            [Test]
            public void MemoryAreas_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var areas = Settings.MemoryAreas; });
            }

            [Test]
            public void OptimizationKey_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var key = Settings.OptimizationKey; });
            }

            [Test]
            public void OptimizationModifiers_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var modifiers = Settings.OptimizationModifiers; });
            }

            [Test]
            public void ProcessExclusionList_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var list = Settings.ProcessExclusionList; });
            }

            [Test]
            public void RunOnPriority_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var priority = Settings.RunOnPriority; });
            }

            [Test]
            public void RunOnStartup_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var runOnStartup = Settings.RunOnStartup; });
            }

            [Test]
            public void Save_DoesNotThrow()
            {
                Assert.DoesNotThrow(() => Settings.Save());
            }

            [Test]
            public void ShowOptimizationNotifications_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var show = Settings.ShowOptimizationNotifications; });
            }

            [Test]
            public void ShowVirtualMemory_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var show = Settings.ShowVirtualMemory; });
            }

            [Test]
            public void StartMinimized_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var start = Settings.StartMinimized; });
            }

            [Test]
            public void TrayIconBackgroundColor_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var color = Settings.TrayIconBackgroundColor; });
            }

            [Test]
            public void TrayIconDangerColor_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var color = Settings.TrayIconDangerColor; });
            }

            [Test]
            public void TrayIconDangerLevel_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var level = Settings.TrayIconDangerLevel; });
            }

            [Test]
            public void TrayIconOptimizeOnMiddleMouseClick_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var optimize = Settings.TrayIconOptimizeOnMiddleMouseClick; });
            }

            [Test]
            public void TrayIconOptimizingColor_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var color = Settings.TrayIconOptimizingColor; });
            }

            [Test]
            public void TrayIconShowMemoryUsage_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var show = Settings.TrayIconShowMemoryUsage; });
            }

            [Test]
            public void TrayIconTextColor_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var color = Settings.TrayIconTextColor; });
            }

            [Test]
            public void TrayIconUseTransparentBackground_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var use = Settings.TrayIconUseTransparentBackground; });
            }

            [Test]
            public void TrayIconWarningColor_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var color = Settings.TrayIconWarningColor; });
            }

            [Test]
            public void TrayIconWarningLevel_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var level = Settings.TrayIconWarningLevel; });
            }

            [Test]
            public void UseHotkey_CanBeAccessed()
            {
                Assert.DoesNotThrow(() => { var use = Settings.UseHotkey; });
            }
        }

        #endregion

        #region ThemeManager Tests

        [TestFixture]
        public class ThemeManagerTests
        {
            [Test]
            public void Brushes_IsNotNull()
            {
                var brushes = ThemeManager.Brushes;

                Assert.IsNotNull(brushes);
            }

            [Test]
            public void Cleanup_DoesNotThrow()
            {
                Assert.DoesNotThrow(() => ThemeManager.Cleanup());
            }

            [Test]
            public void Theme_CanBeSet()
            {
                var originalTheme = ThemeManager.Theme;
                try
                {
                    ThemeManager.Theme = Enums.Theme.Light;

                    Assert.AreEqual(Enums.Theme.Light, ThemeManager.Theme);

                    ThemeManager.Theme = Enums.Theme.Dark;

                    Assert.AreEqual(Enums.Theme.Dark, ThemeManager.Theme);
                }
                finally
                {
                    ThemeManager.Theme = originalTheme;
                }
            }

            [Test]
            public void Themes_IsNotNull()
            {
                var themes = ThemeManager.Themes;

                Assert.IsNotNull(themes);
            }
        }

        #endregion

        #region Helper Classes

        private class TestJsonSerializable : IJsonSerializable
        {
            public string Name { get; set; }
            public int Value { get; set; }

            public object ToJson()
            {
                return new { Name, Value };
            }
        }

        private class TestObject
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        private class TestObservableObject : ObservableObject
        {
            public new void RaisePropertyChanged(string propertyName)
            {
                base.RaisePropertyChanged(propertyName);
            }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
