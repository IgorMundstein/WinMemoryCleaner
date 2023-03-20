using System;
using System.Globalization;
using System.Windows.Data;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Value converter that applies NOT operator to a <see cref="bool" /> value.
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    [ValueConversion(typeof(bool), typeof(bool))]
    internal class InverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Convert a boolean value to its negation.
        /// </summary>
        /// <param name="value">The <see cref="bool" /> value to negate.</param>
        /// <param name="targetType">The type of the target property, as a type reference.</param>
        /// <param name="parameter">Optional parameter. Not used.</param>
        /// <param name="culture">The culture of the conversion. Not used</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is bool && (bool)value);
        }

        /// <summary>
        /// Convert back a boolean value to its negation.
        /// </summary>
        /// <param name="value">The <see cref="bool" /> value to negate.</param>
        /// <param name="targetType">The type of the target property, as a type reference.</param>
        /// <param name="parameter">Optional parameter. Not used.</param>
        /// <param name="culture">The culture of the conversion. Not used</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is bool && (bool)value);
        }
    }
}
