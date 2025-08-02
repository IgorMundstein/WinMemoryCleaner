using System;
using System.Linq.Expressions;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Helper
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Gets the string name of a property or field.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">A lambda expression that accesses the member.</param>
        /// <returns>The string name of the member.</returns>
        public static string NameOf<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException("Expression must be a simple member access (e.g., () => myObject.MyProperty).");

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Converts to hexcode.
        /// </summary>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="alpha">The alpha.</param>
        /// <returns></returns>
        public static string ToHexCode(byte red, byte green, byte blue, byte? alpha = null)
        {
            if (alpha != null)
                return string.Format(Localizer.Culture, "#{0:X2}{1:X2}{2:X2}{3:X2}", alpha, red, green, blue);

            return string.Format(Localizer.Culture, "#{0:X2}{1:X2}{2:X2}", red, green, blue);
        }
    }
}
