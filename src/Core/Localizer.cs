using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;

namespace WinMemoryCleaner
{
    internal static class Localizer
    {
        #region Events

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        #endregion

        #region Fields

        private static string _language;

        #endregion

        #region Constructors

        static Localizer()
        {
            String = new Localization();
            Language = Settings.Language;
        }

        #endregion

        #region Properties

        public static string Test
        {
            get { return "test {0}"; }
        }

        public static string Language
        {
            get { return _language; }
            set
            {
                if (_language == value)
                    return;

                try
                {
                    Load(value);
                }
                catch
                {
                    Settings.Language = Constants.App.Language;
                    Settings.Save();

                    throw;
                }

                _language = value;

                RaiseStaticPropertyChanged("Language");
                RaiseStaticPropertyChanged("String");
            }
        }

        public static Dictionary<string, string> Languages
        {
            get
            {
                try
                {
                    var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                        .Where(file => file.StartsWith(Constants.App.LocalizationResourcePath) && file.EndsWith(Constants.App.LocalizationResourceExtension))
                        .Select(file => file.Replace(Constants.App.LocalizationResourcePath, string.Empty).Replace(Constants.App.LocalizationResourceExtension, string.Empty))
                        .OrderBy(file => file)
                        .ToList();

                    try
                    {
                        var localResources = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, string.Format("*{0}", Constants.App.LocalizationResourceExtension), SearchOption.TopDirectoryOnly)
                            .Select(Path.GetFileNameWithoutExtension)
                            .ToList();

                        if (localResources.Any())
                            resourceNames.AddRange(localResources);
                    }
                    catch
                    {
                        // ignored
                    }

                    return CultureInfo.GetCultures(CultureTypes.AllCultures)
                        .Where(culture => resourceNames.Contains(culture.EnglishName, StringComparer.OrdinalIgnoreCase))
                        .OrderBy(culture => culture.NativeName)
                        .ToDictionary(culture => culture.EnglishName, culture => culture.NativeName.ToTitleCase());
                }
                catch (Exception e)
                {
                    Logger.Error(e);

                    return new Dictionary<string, string> { { Constants.App.Language, Constants.App.Language } };
                }
            }
        }

        public static Localization String { get; private set; }

        #endregion

        #region Methods

        private static void Load(string language)
        {
            Localization localization;

            var localResource = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("{0}{1}", language, Constants.App.LocalizationResourceExtension));
            var resource = string.Format("{0}{1}{2}", Constants.App.LocalizationResourcePath, language, Constants.App.LocalizationResourceExtension);

            using (Stream stream = File.Exists(localResource) ? File.OpenRead(localResource) : Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Localization));

                if (stream == null)
                    throw new Exception(string.Format("The {0} language file is missing or invalid.", language));

                localization = (Localization)serializer.ReadObject(stream);
            }

            var nullOrEmptyStrings = localization
                .GetType()
                .GetProperties()
                .Where(pi => pi.PropertyType == typeof(string) && string.IsNullOrWhiteSpace((string)pi.GetValue(localization, null)))
                .Select(pi => pi.Name)
                .ToList();

            if (nullOrEmptyStrings.Any())
                throw new Exception(string.Format("The {0} language file is invalid. Missing Values: {1}", language, string.Join(", ", nullOrEmptyStrings)));

            String = localization;
        }

        private static void RaiseStaticPropertyChanged(string propertyName = null)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
