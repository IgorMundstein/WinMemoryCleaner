using System;
using System.Globalization;
using System.Linq;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Convert bytes size to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string ByteSizeToString(this ulong value)
        {
            // Exabyte (1024^6)
            if (value >= 1152921504606846976L)
                return string.Format("{0:0.#} EB", (value >> 50) / 1024.0);

            // Petabyte (1024^5)
            if (value >= 1125899906842624L)
                return string.Format("{0:0.#} PB", (value >> 40) / 1024.0);

            // Terabyte (1024^4)
            if (value >= 1099511627776L)
                return string.Format("{0:0.#} TB", (value >> 30) / 1024.0);

            // Gigabyte (1024^3)
            if (value >= 1073741824)
                return string.Format("{0:0.#} GB", (value >> 20) / 1024.0);

            // Megabyte (1024^2)
            if (value >= 1048576)
                return string.Format("{0:0.#} MB", (value >> 10) / 1024.0);

            // Kilobyte (1024^1)
            if (value >= 1024)
                return string.Format("{0:0.#} KB", value / 1024.0);

            // Byte
            return string.Format("{0} B", value);
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static object DefaultValue(this Type value)
        {
            return value.IsValueType && Nullable.GetUnderlyingType(value) == null ? Activator.CreateInstance(value) : null;
        }

        /// <summary>
        /// Validates enum value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified enum value is valid; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValid(this Enum value)
        {
            if (value == null)
                return false;

            char firstDigit = value.ToString()[0];

            return !char.IsDigit(firstDigit) && firstDigit != '-';
        }

        /// <summary>
        /// Removes whitespaces.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string RemoveWhitespaces(this string value)
        {
            return new string(value.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        /// <summary>
        /// Converts to System.Drawing.Color.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static System.Drawing.Color ToColor(this System.Windows.Media.SolidColorBrush value)
        {
            return System.Drawing.Color.FromArgb(value.Color.A, value.Color.R, value.Color.G, value.Color.B);
        }

        /// <summary>
        /// Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms)
        /// </summary>
        /// <param name="value">The string to convert to title case</param>
        /// <returns>The specified string converted to title case</returns>
        internal static string ToTitleCase(this string value)
        {
            return value == null ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }
    }
}
