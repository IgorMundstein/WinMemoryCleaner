using NUnit.Framework;
using System;
using System.Globalization;
using System.Windows.Media;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.Test
{
    /// <summary>
    /// Unit tests for Converter classes
    /// </summary>
    
    [TestFixture]
    public sealed class ConverterTests
    {
        #region BrushToHexConverter Tests

        
        [TestFixture]
        public class BrushToHexConverterTests
        {
            [Test]
            public void Convert_WithSolidColorBrush_ReturnsHexString()
            {
                // Arrange
                var converter = new BrushToHexConverter();
                var brush = new SolidColorBrush(Colors.White);

                // Act
                var result = converter.Convert(brush, typeof(string), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<string>(result);
            }

            [Test]
            public void Convert_WithNullBrush_ReturnsNull()
            {
                var converter = new BrushToHexConverter();
                var result = converter.Convert(null, typeof(string), null, CultureInfo.InvariantCulture);
                
                Assert.IsNull(result);
            }

            [Test]
            public void Convert_WithNonBrush_ReturnsNull()
            {
                var converter = new BrushToHexConverter();
                var result = converter.Convert("not a brush", typeof(string), null, CultureInfo.InvariantCulture);
                
                Assert.IsNull(result);
            }

            [Test]
            public void ConvertBack_ThrowsNotImplementedException()
            {
                // Arrange
                var converter = new BrushToHexConverter();

                // Act & Assert
                Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, typeof(Brush), null, CultureInfo.InvariantCulture));
            }
        }

        #endregion

        #region EnumToBooleanConverter Tests

        
        [TestFixture]
        public class EnumToBooleanConverterTests
        {
            [Test]
            public void Convert_WithMatchingValues_ReturnsTrue()
            {
                // Arrange
                var converter = new EnumToBooleanConverter();
                var enumValue = DayOfWeek.Monday;
                var parameter = DayOfWeek.Monday;

                // Act
                var result = converter.Convert(enumValue, typeof(bool), parameter, CultureInfo.InvariantCulture);

                // Assert
                Assert.IsTrue((bool)result);
            }

            [Test]
            public void Convert_WithNonMatchingValues_ReturnsFalse()
            {
                // Arrange
                var converter = new EnumToBooleanConverter();
                var enumValue = DayOfWeek.Monday;
                var parameter = DayOfWeek.Tuesday;

                // Act
                var result = converter.Convert(enumValue, typeof(bool), parameter, CultureInfo.InvariantCulture);

                // Assert
                Assert.IsFalse((bool)result);
            }

            [Test]
            public void Convert_WithNullValue_ReturnsFalse()
            {
                var converter = new EnumToBooleanConverter();
                var result = converter.Convert(null, typeof(bool), Enums.Priority.Normal, CultureInfo.InvariantCulture);
                
                Assert.IsFalse((bool)result);
            }

            [Test]
            public void ConvertBack_WithTrue_ReturnsParameter()
            {
                // Arrange
                var converter = new EnumToBooleanConverter();
                var parameter = DayOfWeek.Monday;

                // Act
                var result = converter.ConvertBack(true, typeof(DayOfWeek), parameter, CultureInfo.InvariantCulture);

                // Assert
                Assert.AreEqual(parameter, result);
            }

            [Test]
            public void ConvertBack_WithFalse_ReturnsParameter()
            {
                var converter = new EnumToBooleanConverter();
                var result = converter.ConvertBack(false, typeof(Enums.Priority), Enums.Priority.High, CultureInfo.InvariantCulture);
                
                // EnumToBooleanConverter.ConvertBack always returns the parameter value
                Assert.AreEqual(Enums.Priority.High, result);
            }
        }

        #endregion

        #region InverseBooleanConverter Tests

        
        [TestFixture]
        public class InverseBooleanConverterTests
        {
            [Test]
            public void Convert_WithFalse_ReturnsTrue()
            {
                // Arrange
                var converter = new InverseBooleanConverter();

                // Act
                var result = converter.Convert(false, typeof(bool), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.IsTrue((bool)result);
            }

            [Test]
            public void Convert_WithTrue_ReturnsFalse()
            {
                // Arrange
                var converter = new InverseBooleanConverter();

                // Act
                var result = converter.Convert(true, typeof(bool), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.IsFalse((bool)result);
            }

            [Test]
            public void ConvertBack_WithFalse_ReturnsTrue()
            {
                // Arrange
                var converter = new InverseBooleanConverter();

                // Act
                var result = converter.ConvertBack(false, typeof(bool), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.IsTrue((bool)result);
            }

            [Test]
            public void ConvertBack_WithTrue_ReturnsFalse()
            {
                // Arrange
                var converter = new InverseBooleanConverter();

                // Act
                var result = converter.ConvertBack(true, typeof(bool), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.IsFalse((bool)result);
            }
        }

        #endregion

        #region InverseBooleanToVisibilityConverter Tests

        
        [TestFixture]
        public class InverseBooleanToVisibilityConverterTests
        {
            [Test]
            public void Convert_WithFalse_ReturnsVisible()
            {
                // Arrange
                var converter = new InverseBooleanToVisibilityConverter();

                // Act
                var result = converter.Convert(false, typeof(System.Windows.Visibility), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.AreEqual(System.Windows.Visibility.Visible, result);
            }

            [Test]
            public void Convert_WithTrue_ReturnsCollapsed()
            {
                // Arrange
                var converter = new InverseBooleanToVisibilityConverter();

                // Act
                var result = converter.Convert(true, typeof(System.Windows.Visibility), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.AreEqual(System.Windows.Visibility.Collapsed, result);
            }
        }

        #endregion

        #region NullToVisibilityConverter Tests

        
        [TestFixture]
        public class NullToVisibilityConverterTests
        {
            [Test]
            public void Convert_WithNonNull_ReturnsVisible()
            {
                // Arrange
                var converter = new NullToVisibilityConverter();

                // Act
                var result = converter.Convert("test", typeof(System.Windows.Visibility), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.AreEqual(System.Windows.Visibility.Visible, result);
            }

            [Test]
            public void Convert_WithNull_ReturnsCollapsed()
            {
                // Arrange
                var converter = new NullToVisibilityConverter();

                // Act
                var result = converter.Convert(null, typeof(System.Windows.Visibility), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.AreEqual(System.Windows.Visibility.Collapsed, result);
            }
        }

        #endregion

        #region PercentageToWidthConverter Tests

        
        [TestFixture]
        public class PercentageToWidthConverterTests
        {
            [Test]
            public void Convert_WithValidPercentage_CalculatesWidth()
            {
                // Arrange
                var converter = new PercentageToWidthConverter();

                // Act
                var result = converter.Convert(new object[] { 200.0, 50.0 }, typeof(double), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.AreEqual(100.0, (double)result);
            }
        }

        #endregion

        #region StringFormatConverter Tests

        
        [TestFixture]
        public class StringFormatConverterTests
        {
            [Test]
            public void Convert_WithFormatString_FormatsValue()
            {
                // Arrange
                var converter = new StringFormatConverter();

                // Act
                var result = converter.Convert(123, typeof(string), "{0:D5}", CultureInfo.InvariantCulture);

                // Assert
                Assert.IsInstanceOf<string>(result);
                Assert.AreEqual("00123", result);
            }
        }

        #endregion

        #region SumNumericConverter Tests

        
        [TestFixture]
        public class SumNumericConverterTests
        {
            [Test]
            public void Convert_WithNumericValues_ReturnsSum()
            {
                // Arrange
                var converter = new SumNumericConverter();
                var values = new object[] { 10, 20, 30 };

                // Act
                var result = converter.Convert(values, typeof(double), null, CultureInfo.InvariantCulture);

                // Assert
                Assert.IsInstanceOf<double>(result);
                Assert.AreEqual(60.0, (double)result);
            }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
