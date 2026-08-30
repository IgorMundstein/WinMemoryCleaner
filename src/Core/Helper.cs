using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace WinMemoryCleaner
{
    public static class Helper
    {
        public static bool DeleteFile(string path, bool throwOnException = false)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
            }
            catch
            {
                Logger.Debug("Failed to delete file: " + path);

                if (throwOnException)
                    throw;
            }

            return false;
        }

        public static T Deserialize<T>(string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(obj, JsonOptions);
            }
            catch
            {
                return default;
            }
        }

        public static string FormatJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            try
            {
                using var document = JsonDocument.Parse(json);
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                return JsonSerializer.Serialize(document, options);
            }
            catch
            {
                return json;
            }
        }

        public static string GetExecutablePath()
        {
            try
            {
                var path = Process.GetCurrentProcess().MainModule?.FileName;

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

        public static string NameOf<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (expression.Body is MemberExpression memberExpression)
                return memberExpression.Member.Name;

            throw new ArgumentException("Expression must be a simple member access (e.g., () => myObject.MyProperty).");
        }

        public static T ReadEmbeddedResource<T>(string name)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            if (stream == null)
                return default;

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            return Deserialize<T>(json);
        }

        public static string Serialize<T>(T obj, bool minified = false) where T : IJsonSerializable
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var json = JsonSerializer.Serialize(obj.ToJson(), minified ? JsonOptionsMinified : JsonOptions);
            return json;
        }

        public static void StartMenuShortcut(bool create)
        {
            try
            {
                var shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), Constants.App.Shortcut);

                if (create)
                {
                    var link = (ShellInterop.IShellLink)new ShellInterop.ShellLink();

                    link.SetDescription(Constants.App.Title);
                    link.SetPath(App.Path);
                    link.SetWorkingDirectory(Path.GetDirectoryName(App.Path));

                    var file = (ShellInterop.IPersistFile)link;
                    file.Save(shortcutPath, false);
                }
                else
                {
                    DeleteFile(shortcutPath);
                }
            }
            catch (Exception e)
            {
                Logger.Debug("Failed to " + (create ? "create" : "delete") + " Start Menu shortcut: " + e.GetMessage());
            }
        }

        public static string ToHexCode(byte red, byte green, byte blue, byte? alpha = null)
        {
            if (alpha != null)
                return string.Format(Localizer.Culture, "#{0:X2}{1:X2}{2:X2}{3:X2}", alpha, red, green, blue);

            return string.Format(Localizer.Culture, "#{0:X2}{1:X2}{2:X2}", red, green, blue);
        }

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        private static readonly JsonSerializerOptions JsonOptionsMinified = new()
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };
    }
}