using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Script.Serialization;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Helper
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Determines if the current Windows version supports updates via GitHub TLS/SNI.
        /// Returns false for legacy Windows versions (XP/2003) that cannot reach GitHub.
        /// </summary>
        /// <returns>True if updates are supported; otherwise, false.</returns>
        public static bool IsAutoUpdateSupported
        {
            get
            {
                try
                {
                    var os = Environment.OSVersion;

                    if (os.Version != null && os.Version.Major < 6)
                        return false; // Windows XP/2003 and earlier
                }
                catch
                {
                }

                return true;
            }
        }

        /// <summary>
        /// Converts the specified JSON string to an object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string input)
        {
            return new JavaScriptSerializer().Deserialize<T>(input);
        }

        /// <summary>
        /// Creates a directory exists. Ignores failures.
        /// </summary>
        public static void CreateDirectory(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Gets the current application's assembly version.
        /// </summary>
        public static Version GetCurrentVersion()
        {
            try
            {
                return (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).GetName().Version ?? new Version(0, 0, 0, 0);
            }
            catch
            {
                return new Version(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Returns the executable path of the current process.
        /// </summary>
        public static string GetExecutablePath()
        {
            try
            {
                var entry = Assembly.GetEntryAssembly();

                if (entry != null && !string.IsNullOrEmpty(entry.Location))
                    return entry.Location;
            }
            catch
            {
            }

            try
            {
                return Process.GetCurrentProcess().MainModule.FileName;
            }
            catch
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName);
            }
        }

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
        /// Reads the embedded resource.
        /// </summary>
        /// <param name="name">Name of the resource.</param>
        /// <returns></returns>
        public static T ReadEmbeddedResource<T>(string name)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            using (var reader = new StreamReader(stream))
            {
                return Deserialize<T>(reader.ReadToEnd());
            }
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