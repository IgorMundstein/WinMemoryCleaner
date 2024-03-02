using System;
using System.Collections.Generic;
using System.Linq;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
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

        public static KeyValuePair<double, Enums.Memory.Unit> ToMemoryUnit(this ulong value)
        {
            if (value < 1024)
                return new KeyValuePair<double, Enums.Memory.Unit>(value, Enums.Memory.Unit.B);

            var mag = (int)Math.Log(value, 1024);

            return new KeyValuePair<double, Enums.Memory.Unit>(value / Math.Pow(1024, mag), (Enums.Memory.Unit)mag);
        }
    }
}