using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Capitalizes the first letter and every letter immediately following a dot, converting all other letters to lowercase.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Capitalize(this string value)
        {
            return string.IsNullOrEmpty(value)
                ? value
                : Regex.Replace
                (
                    Localizer.Culture.TextInfo.ToLower(value),
                    @"(^|(?<=\.))(\s*)([a-zA-Z])",
                    m => m.Groups[1].Value + m.Groups[2].Value + Localizer.Culture.TextInfo.ToUpper(m.Groups[3].Value[0])
                );
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object DefaultValue(this Type value)
        {
            return value.IsValueType && Nullable.GetUnderlyingType(value) == null ? Activator.CreateInstance(value) : null;
        }

        /// <summary>
        /// Get exception error message.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetMessage(this Exception value)
        {
            var exception = value;
            var messages = new List<string>();

            do
            {
                messages.Add(exception.Message.Trim());

                exception = exception.InnerException;
            }
            while (exception != null);

            return string.Join(". ", messages.Distinct());
        }

        /// <summary>
        /// Gets the reason string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetString(this Enums.Memory.Optimization.Reason value)
        {
            switch (value)
            {
                case Enums.Memory.Optimization.Reason.LowMemory:
                    return Localizer.String.LowMemory;

                case Enums.Memory.Optimization.Reason.Manual:
                    return Localizer.String.Manual;

                case Enums.Memory.Optimization.Reason.Schedule:
                    return Localizer.String.Schedule;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Determines whether the specified value is a number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is a number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumber(this object value)
        {
            if (value == null)
                return false;

            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Validates enum value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified enum value is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this Enum value)
        {
            if (value == null)
                return false;

            var firstDigit = value.ToString()[0];

            return !char.IsDigit(firstDigit) && firstDigit != '-';
        }

        /// <summary>
        /// Removes whitespaces.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string RemoveWhitespaces(this string value)
        {
            return new string(value.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        /// <summary>
        /// Convert Media Color (WPF) to Drawing Color (WinForm)
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static System.Drawing.Color ToColor(this System.Windows.Media.SolidColorBrush value)
        {
            return System.Drawing.Color.FromArgb(value.Color.A, value.Color.R, value.Color.G, value.Color.B);
        }

        /// <summary>
        /// Convert value to memory unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static KeyValuePair<double, Enums.Memory.Unit> ToMemoryUnit(this long value)
        {
            if (value < 1024)
                return new KeyValuePair<double, Enums.Memory.Unit>(value, Enums.Memory.Unit.B);

            var mag = (int)Math.Log(value, 1024);

            return new KeyValuePair<double, Enums.Memory.Unit>(value / Math.Pow(1024, mag), (Enums.Memory.Unit)mag);
        }
    }
}
