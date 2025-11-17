using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Percentage To Width Converter
    /// Converts a percentage value (0-100) and container width to the actual pixel width
    /// </summary>
    /// <seealso cref="IMultiValueConverter" />
    public class PercentageToWidthConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts a percentage and container width to pixel width.
        /// </summary>
        /// <param name="values">The array of values: [0] = container width, [1] = percentage (0-100)</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The calculated width in pixels</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values == null || values.Length < 2)
                    return 0.0;

                if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
                    return 0.0;

                var containerWidth = System.Convert.ToDouble(values[0], culture);
                var percentage = System.Convert.ToDouble(values[1], culture);

                if (containerWidth <= 0 || percentage < 0)
                    return 0.0;

                // Clamp percentage to 0-100
                if (percentage > 100)
                    percentage = 100;

                return containerWidth * (percentage / 100.0);
            }
            catch
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Converts a width back to percentage (not supported).
        /// </summary>
        /// <param name="value">The value that the binding target produces.</param>
        /// <param name="targetTypes">The array of types to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Not supported</returns>
        /// <exception cref="NotSupportedException"></exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
