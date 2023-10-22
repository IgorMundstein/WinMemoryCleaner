using System;
using System.Globalization;
using System.Windows.Data;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Enum to Boolean Converter
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    [ValueConversion(typeof(bool), typeof(Enum))]
    public sealed class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts an enumeration value to Boolean value 
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            var enum1 = (Enum)value;
            var enum2 = (Enum)parameter;

            if (enum1.GetType() == enum2.GetType() && enum1.GetType().IsDefined(typeof(FlagsAttribute), false))
                return enum1.HasFlag(enum2);

            return enum1.Equals(enum2);
        }

        /// <summary>
        /// Converts an enumeration value to Boolean value
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <see langword="null" />, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == null)
                return false;

            return System.Convert.ChangeType(parameter ?? targetType.DefaultValue(), targetType, Localizer.Culture);
        }
    }
}
