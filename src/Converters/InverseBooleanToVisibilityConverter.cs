using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WinMemoryCleaner
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    internal sealed class InverseBooleanToVisibilityConverter : IValueConverter
    {
        private readonly BooleanToVisibilityConverter _converter = new BooleanToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _converter.Convert(value, targetType, parameter, culture) as Visibility? == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _converter.ConvertBack(value, targetType, parameter, culture) as bool? != true;
        }
    }
}