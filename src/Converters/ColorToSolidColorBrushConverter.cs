using System;
using System.Windows.Data;
using System.Windows.Media;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Color to Brush Converter
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    internal class ColorToSolidColorBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) 
                return default(SolidColorBrush);

            if (value is System.Drawing.Color)
            {
                System.Drawing.Color color = (System.Drawing.Color)value;
                return new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            }

            if (value is Color)
                return new SolidColorBrush((Color)value);

            throw new InvalidOperationException("Unsupported type [" + value.GetType().Name + "], ColorToSolidColorBrushConverter.Convert()");
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var solidColorBrush = value as SolidColorBrush;

            if (solidColorBrush != null)
                return solidColorBrush.Color;

            return default(SolidColorBrush);
        }
    }
}
