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
    /// <summary>
    /// Localizer
    /// </summary>
    public static class Localizer
    {
        #region Events

        /// <summary>
        /// Occurs when [static property changed].
        /// </summary>
        public static event PropertyChangedEventHandler StaticPropertyChanged;

        #endregion

        #region Fields

        private static CultureInfo _culture;
        private static Language _language;

        #endregion

        #region Constructors

        static Localizer()
        {
            String = new Localization();

            try
            {
                Culture = new CultureInfo(Settings.Language);
            }
            catch
            {
                Culture = new CultureInfo(Constants.Windows.Locale.Name.English);
            }

            Language = new Language(Culture);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        public static CultureInfo Culture
        {
            get { return _culture; }
            private set
            {
                _culture = value;

                RaiseStaticPropertyChanged("Culture");
            }
        }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public static Language Language
        {
            get { return _language; }
            set
            {
                if (_language != null && _language.Equals(value))
                    return;

                try
                {
                    if (value == null)
                        throw new ArgumentNullException("value");

                    Load(value);

                    Settings.Language = value.Name;
                    Settings.Save();
                }
                catch
                {
                    Settings.Language = Constants.Windows.Locale.Name.English;
                    Settings.Save();

                    throw;
                }

                _language = value;

                RaiseStaticPropertyChanged("Language");
                RaiseStaticPropertyChanged("String");

                App.ReleaseMemory();
            }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        public static List<Language> Languages
        {
            get
            {
                try
                {
                    var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                        .Where(file => file.StartsWith(Constants.App.LocalizationResourcePath, StringComparison.OrdinalIgnoreCase) && file.EndsWith(Constants.App.LocalizationResourceExtension, StringComparison.OrdinalIgnoreCase))
                        .Select(file => file.Replace(Constants.App.LocalizationResourcePath, string.Empty).Replace(Constants.App.LocalizationResourceExtension, string.Empty))
                        .OrderBy(file => file)
                        .ToList();

                    try
                    {
                        var localResources = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, string.Format(Culture, "*{0}", Constants.App.LocalizationResourceExtension), SearchOption.TopDirectoryOnly)
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
                        .OrderBy(culture => culture.EnglishName, StringComparer.InvariantCultureIgnoreCase)
                        .Select(culture => new Language(culture))
                        .ToList();
                }
                catch (Exception e)
                {
                    Logger.Error(e);

                    return new List<Language> { new Language(new CultureInfo(Constants.Windows.Locale.Name.English)) };
                }
            }
        }

        /// <summary>
        /// Gets the string
        /// </summary>
        public static Localization String { get; private set; }

        #endregion

        #region Methods

        private static void Load(Language language)
        {
            Localization localization;

            var localResource = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(Culture, "{0}{1}", language.EnglishName, Constants.App.LocalizationResourceExtension));
            var resource = string.Format(Culture, "{0}{1}{2}", Constants.App.LocalizationResourcePath, language.EnglishName, Constants.App.LocalizationResourceExtension);

            using (var stream = File.Exists(localResource) ? File.OpenRead(localResource) : Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            {
                try
                {
                    if (stream == null)
                        throw new NullReferenceException();

                    var serializer = new DataContractJsonSerializer(typeof(Localization));

                    localization = (Localization)serializer.ReadObject(stream);
                }
                catch
                {
                    throw new Exception(string.Format(Culture, "The {0} language file is missing or invalid.", language.EnglishName));
                }
            }

            var nullOrEmptyStrings = localization
                .GetType()
                .GetProperties()
                .Where(pi => pi.PropertyType == typeof(string) && string.IsNullOrWhiteSpace((string)pi.GetValue(localization, null)))
                .Select(pi => pi.Name)
                .ToList();

            if (nullOrEmptyStrings.Any())
                throw new Exception(string.Format(Culture, "The {0} language file is invalid. Missing Values: {1}", language.EnglishName, string.Join(", ", nullOrEmptyStrings)));

            Culture = new CultureInfo(language.Name);
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
