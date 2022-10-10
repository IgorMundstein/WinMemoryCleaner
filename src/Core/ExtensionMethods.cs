using System;
using System.Globalization;

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
                return string.Format(CultureInfo.CurrentCulture, "{0:0.#} EB", (value >> 50) / 1024.0);

            // Petabyte (1024^5)
            if (value >= 1125899906842624L)
                return string.Format(CultureInfo.CurrentCulture, "{0:0.#} PB", (value >> 40) / 1024.0);

            // Terabyte (1024^4)
            if (value >= 1099511627776L)
                return string.Format(CultureInfo.CurrentCulture, "{0:0.#} TB", (value >> 30) / 1024.0);

            // Gigabyte (1024^3)
            if (value >= 1073741824)
                return string.Format(CultureInfo.CurrentCulture, "{0:0.#} GB", (value >> 20) / 1024.0);

            // Megabyte (1024^2)
            if (value >= 1048576)
                return string.Format(CultureInfo.CurrentCulture, "{0:0.#} MB", (value >> 10) / 1024.0);

            // Kilobyte (1024^1)
            if (value >= 1024)
                return string.Format(CultureInfo.CurrentCulture, "{0:0.#} KB", value / 1024.0);

            // Byte
            return string.Format(CultureInfo.CurrentCulture, "{0} B", value);
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
    }
}
