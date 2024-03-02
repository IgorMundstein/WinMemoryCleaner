using System;
using System.Globalization;
using System.Windows.Data;

namespace WinMemoryCleaner
{
    /// <summary>
    /// This class provides a binding converter to display formatted strings
    /// </summary>
    /// <seealso cref="IValueConverter" />
    [ValueConversion(typeof(string), typeof(string))]
    public sealed class StringFormatConverter : IValueConverter
    {
        /// <summary>
        /// Return the formatted string version of the source object.
        /// </summary>
        /// <param name="value">Object to transform to string.</param>
        /// <param name="targetType">The type of the target property, as a type reference</param>
        /// <param name="parameter">An optional parameter to be used in the string.Format method.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>Formatted string.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(Localizer.Culture, (string)(parameter ?? "{0}"), value);
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The type of the target property, as a type reference (System.Type for Microsoft .NET, a TypeName helper struct for Visual C++ component extensions (C++/CX)).</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>The value to be passed to the source object.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}