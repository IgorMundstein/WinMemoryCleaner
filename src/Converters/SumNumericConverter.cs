using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WinMemoryCleaner
{
    /// <summary>
    /// A generic WPF IMultiValueConverter that sums numeric values or a specified numeric property of its input array.
    /// Pass the property name as the converter parameter (e.g., "ActualHeight", "Width").
    /// If no property name is provided, it sums the values directly.
    /// </summary>
    public class SumNumericConverter : IMultiValueConverter
    {
        /// <summary>  
        /// Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.  
        /// </summary>  
        /// <param name="values">The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding" /> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the source binding has no value to provide for conversion.</param>  
        /// <param name="targetType">The type of the binding target property.</param>  
        /// <param name="parameter">The converter parameter to use.</param>  
        /// <param name="culture">The culture to use in the converter.</param>  
        /// <returns>  
        /// A converted value.If the method returns null, the valid null value is used.A return value of <see cref="T:System.Windows.DependencyProperty" />.<see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the converter did not produce a value, and that the binding will use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> if it is available, or else will use the default value.A return value of <see cref="T:System.Windows.Data.Binding" />.<see cref="F:System.Windows.Data.Binding.DoNothing" /> indicates that the binding does not transfer the value or use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> or the default value.  
        /// </returns>  
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double sum = 0;

            if (values == null)
                return sum;

            var propertyName = parameter as string;

            foreach (var value in values)
            {
                if (value == null || value == DependencyProperty.UnsetValue)
                    continue;

                var obj = value;

                if (!string.IsNullOrEmpty(propertyName))
                {
                    var prop = value.GetType().GetProperty(propertyName);

                    if (prop != null)
                        obj = prop.GetValue(value, null);
                    else
                        continue;
                }

                if (obj.IsNumber())
                {
                    sum += System.Convert.ToDouble(obj, culture);
                }
                else if (obj != null)
                {
                    double number;

                    if (double.TryParse(obj.ToString(), NumberStyles.Any, culture, out number))
                        sum += number;
                }
            }

            return sum;
        }

        /// <summary>  
        /// Converts a binding target value to the source binding values.  
        /// </summary>  
        /// <param name="value">The value that the binding target produces.</param>  
        /// <param name="targetTypes">The array of types to convert to. The array length indicates the number and types of values that are suggested for the method to return.</param>  
        /// <param name="parameter">The converter parameter to use.</param>  
        /// <param name="culture">The culture to use in the converter.</param>  
        /// <returns>  
        /// An array of values that have been converted from the target value back to the source values.  
        /// </returns>  
        /// <exception cref="System.NotSupportedException"></exception>  
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
