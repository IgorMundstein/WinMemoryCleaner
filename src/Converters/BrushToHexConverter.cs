using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Brush to Hex Converter
    /// </summary>
    /// <seealso cref="IValueConverter" />
    public class BrushToHexConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value as SolidColorBrush;

            if (brush == null)
                return null;

            bool includeAlpha = false;

            if (parameter != null)
                includeAlpha = System.Convert.ToBoolean(System.Convert.ToString(parameter, Localizer.Culture), Localizer.Culture);

            return brush.Color.GetHex(includeAlpha);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}