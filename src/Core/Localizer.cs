using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace WinMemoryCleaner
{
    internal static class Localizer
    {
        #region Events

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        #endregion

        #region Fields

        private static Enums.Culture _culture;

        #endregion

        #region Constructors

        static Localizer()
        {
            String = new Localization();
            Culture = Settings.Culture;
        }

        #endregion

        #region Properties

        internal static Enums.Culture Culture
        {
            get { return _culture; }
            set
            {
                if (_culture == value)
                    return;

                Load(value);

                _culture = value;

                RaiseStaticPropertyChanged("Culture");
                RaiseStaticPropertyChanged("String");
            }
        }

        public static Localization String { get; private set; }

        #endregion

        #region Methods

        private static void Load(Enums.Culture culture)
        {
            Localization localization;
            var stringsResource = string.Format("{0}.{1}.json", Constants.App.LocalizationResourcePath, culture);

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(stringsResource))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Localization));

                if (stream == null)
                    throw new Exception(string.Format("{0} is invalid", stringsResource));

                localization = (Localization)serializer.ReadObject(stream);

                var nullOrEmptyStrings = localization
                    .GetType()
                    .GetProperties()
                    .Where(pi => pi.PropertyType == typeof(string) && string.IsNullOrWhiteSpace((string)pi.GetValue(localization, null)))
                    .Select(pi => pi.Name)
                    .ToList();

                if (nullOrEmptyStrings.Any())
                    throw new Exception(string.Format("{0} is invalid. Missing Values: {1}", stringsResource, string.Join(", ", nullOrEmptyStrings)));
            }

            CultureInfo cultureInfo = new CultureInfo((int)culture);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

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
