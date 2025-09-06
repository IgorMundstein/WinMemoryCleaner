using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string Capitalize(this string obj)
        {
            return string.IsNullOrEmpty(obj)
                ? obj
                : Regex.Replace
                (
                    Localizer.Culture.TextInfo.ToLower(obj),
                    @"(^|(?<=\.))(\s*)(\p{L})",
                    m => m.Groups[1].Value + m.Groups[2].Value + Localizer.Culture.TextInfo.ToUpper(m.Groups[3].Value[0])
                );
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static object DefaultValue(this Type obj)
        {
            return obj.IsValueType && Nullable.GetUnderlyingType(obj) == null ? Activator.CreateInstance(obj) : null;
        }

        /// <summary>
        /// Gets the hex code.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="includeAlpha">if set to <c>true</c> [include alpha].</param>
        /// <returns></returns>
        public static string GetHex(this Brush obj, bool includeAlpha = false)
        {
            return ((SolidBrush)obj).Color.GetHex(includeAlpha);
        }

        /// <summary>
        /// Converts to hex code.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="includeAlpha">if set to <c>true</c> [include alpha].</param>
        /// <returns></returns>
        public static string GetHex(this Color obj, bool includeAlpha = false)
        {
            if (includeAlpha)
                return Helper.ToHexCode(obj.R, obj.G, obj.B, obj.A);

            return Helper.ToHexCode(obj.R, obj.G, obj.B);
        }

        /// <summary>
        /// Converts to hex code.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="includeAlpha">if set to <c>true</c> [include alpha].</param>
        /// <returns></returns>
        public static string GetHex(this System.Windows.Media.Color obj, bool includeAlpha = false)
        {
            if (includeAlpha)
                return Helper.ToHexCode(obj.R, obj.G, obj.B, obj.A);

            return Helper.ToHexCode(obj.R, obj.G, obj.B);
        }

        /// <summary>
        /// Gets the key value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static KeyValuePair<string, bool?> GetKeyValue(this Enums.Dialog.Button obj)
        {
            switch (obj)
            {
                case Enums.Dialog.Button.None:
                    return new KeyValuePair<string, bool?>(null, null);

                case Enums.Dialog.Button.Yes:
                    return new KeyValuePair<string, bool?>(Localizer.String.Yes, true);

                case Enums.Dialog.Button.No:
                    return new KeyValuePair<string, bool?>(Localizer.String.No, false);

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Returns a compact, de-duplicated message across the exception chain.
        /// </summary>
        public static string GetMessage(this Exception obj)
        {
            if (obj == null)
                return null;

            var exception = obj;
            var messages = new List<string>();

            do
            {
                try
                {
                    var message = exception.Message;

                    if (!string.IsNullOrEmpty(message))
                        messages.Add(message.Trim());
                    else
                        messages.Add(exception.ToString());
                }
                catch
                {
                    messages.Add(exception.ToString());
                }

                exception = exception.InnerException;
            }
            while (exception != null);

            return string.Join(". ", messages.Distinct());
        }

        /// <summary>
        /// Gets the reason string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetString(this Enums.Memory.Optimization.Reason obj)
        {
            switch (obj)
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
        /// Determines whether the specified color is equals.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        ///   <c>true</c> if the specified color is equals; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEquals(this System.Windows.Media.Color obj, Color color)
        {
            if (obj == null)
                return false;

            return obj.A == color.A && obj.R == color.R && obj.G == color.G && obj.B == color.B;
        }

        /// <summary>
        /// Determines whether the specified value is a number.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is a number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumber(this object obj)
        {
            if (obj == null)
                return false;

            switch (Type.GetTypeCode(obj.GetType()))
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
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if the specified enum value is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this Enum obj)
        {
            if (obj == null)
                return false;

            var firstDigit = obj.ToString()[0];

            return !char.IsDigit(firstDigit) && firstDigit != '-';
        }

        /// <summary>
        /// Removes whitespaces.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string RemoveWhitespaces(this string obj)
        {
            return new string(obj.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        /// <summary>
        /// Converts to brush.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Brush ToBrush(this System.Windows.Media.Brush obj)
        {
            var mediaBrush = obj as System.Windows.Media.SolidColorBrush;

            if (mediaBrush == null)
                return null;

            return new SolidBrush(Color.FromArgb(mediaBrush.Color.A, mediaBrush.Color.R, mediaBrush.Color.G, mediaBrush.Color.B));
        }

        /// <summary>
        /// Converts to Brush.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="fallbackValue">The fallback value.</param>
        /// <returns></returns>
        public static Brush ToBrush(this string obj, Brush fallbackValue)
        {
            try
            {
                return new SolidBrush(ColorTranslator.FromHtml(obj));
            }
            catch
            {
                return fallbackValue;
            }
        }

        /// <summary>
        /// Convert Media Color (WPF) to Drawing Color (WinForm)
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Color ToColor(this System.Windows.Media.SolidColorBrush obj)
        {
            return Color.FromArgb(obj.Color.A, obj.Color.R, obj.Color.G, obj.Color.B);
        }

        /// <summary>
        /// Convert value to memory unit.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static KeyValuePair<double, Enums.Memory.Unit> ToMemoryUnit(this long obj)
        {
            if (obj < 1024)
                return new KeyValuePair<double, Enums.Memory.Unit>(obj, Enums.Memory.Unit.B);

            var mag = (int)Math.Log(obj, 1024);

            return new KeyValuePair<double, Enums.Memory.Unit>(obj / Math.Pow(1024, mag), (Enums.Memory.Unit)mag);
        }
    }
}