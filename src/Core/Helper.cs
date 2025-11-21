using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Web.Script.Serialization;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Helper
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Appends indentation to the StringBuilder based on the current nesting level.
        /// </summary>
        /// <param name="sb">The StringBuilder to append to.</param>
        /// <param name="level">The current indentation level.</param>
        /// <param name="indent">The string to use for each indentation level.</param>
        private static void AppendIndent(StringBuilder sb, int level, string indent)
        {
            for (var i = 0; i < level; i++)
                sb.Append(indent);
        }

        /// <summary>
        /// Creates a shortcut (.lnk) at the specified path pointing to the target executable.
        /// </summary>
        /// <param name="targetExePath">The full path to the .exe file.</param>
        /// <param name="destinationLinkPath">The full path where the .lnk file will be saved.</param>
        /// <param name="description">The tooltip description for the shortcut.</param>
        public static void CreateShortcut(string targetExePath, string destinationLinkPath, string description)
        {
            var link = (ShellInterop.IShellLink)new ShellInterop.ShellLink();

            link.SetDescription(description);
            link.SetPath(targetExePath);
            link.SetWorkingDirectory(Path.GetDirectoryName(targetExePath));

            IPersistFile file = (IPersistFile)link;

            file.Save(destinationLinkPath, false);
        }

        /// <summary>
        /// Converts the specified JSON string to an object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string obj)
        {
            return new JavaScriptSerializer().Deserialize<T>(obj);
        }

        /// <summary>
        /// Formats a minified JSON string into a pretty-printed format with proper indentation and line breaks.
        /// </summary>
        /// <param name="json">The minified JSON string to format.</param>
        /// <returns>A formatted JSON string with indentation and line breaks.</returns>
        private static string FormatJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            var sb = new StringBuilder(json.Length * 2);
            var indent = "  ";
            var level = 0;
            var inString = false;
            var escapeNext = false;

            for (var i = 0; i < json.Length; i++)
            {
                var c = json[i];

                if (escapeNext)
                {
                    sb.Append(c);
                    escapeNext = false;
                    continue;
                }

                if (c == '\\')
                {
                    sb.Append(c);
                    escapeNext = true;
                    continue;
                }

                if (c == '"')
                {
                    sb.Append(c);
                    inString = !inString;
                    continue;
                }

                if (inString)
                {
                    sb.Append(c);
                    continue;
                }

                switch (c)
                {
                    case '{':
                    case '[':
                        sb.Append(c);
                        sb.Append(Environment.NewLine);
                        level++;
                        AppendIndent(sb, level, indent);
                        break;

                    case '}':
                    case ']':
                        sb.Append(Environment.NewLine);
                        level--;
                        AppendIndent(sb, level, indent);
                        sb.Append(c);
                        break;

                    case ',':
                        sb.Append(c);
                        sb.Append(Environment.NewLine);
                        AppendIndent(sb, level, indent);
                        break;

                    case ':':
                        sb.Append(c);
                        sb.Append(' ');
                        break;

                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        // Skip whitespace outside strings
                        break;

                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns the executable application's path
        /// </summary>
        public static string GetExecutablePath()
        {
            try
            {
                var path = Process.GetCurrentProcess().MainModule.FileName;

                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    return path;
            }
            catch 
            {
                // ignored
            }

            try
            {
                var entry = Assembly.GetEntryAssembly();

                if (entry != null && !string.IsNullOrEmpty(entry.Location))
                    return entry.Location;
            }
            catch
            {
                // ignored
            }

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName);
        }

        /// <summary>
        /// Gets the application's assembly version
        /// </summary>
        public static Version GetVersion()
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
        /// Converts the specified object to a JSON string
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="minified">If true, produces compact JSON without formatting; otherwise, formats with indentation for readability. Default is false.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string Serialize(IJsonSerializable obj, bool minified = false)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            var json = new JavaScriptSerializer().Serialize(obj.ToJson());

            return minified ? json : FormatJson(json);
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