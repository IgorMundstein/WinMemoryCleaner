using System;
using System.Globalization;
using System.Windows;

namespace WinMemoryCleaner
{
    internal class Language
    {
        internal Language(CultureInfo culture)
        {
            if (culture == null)
                throw new ArgumentNullException("culture");

            Direction = culture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            EnglishName = culture.TextInfo.ToTitleCase(culture.EnglishName);
            Name = culture.Name;
            NativeName = culture.TextInfo.ToTitleCase(culture.NativeName);
        }

        public FlowDirection Direction { get; private set; }

        public string EnglishName { get; private set; }

        public string Name { get; private set; }

        public string NativeName { get; private set; }

        internal bool Equals(Language language)
        {
            if (ReferenceEquals(null, language))
                return false;

            if (ReferenceEquals(this, language))
                return true;

            return Equals(language.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == typeof(Language) && Equals((Language)obj);
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return EnglishName == NativeName ? EnglishName : string.Format(Localizer.Culture, "{0} ({1})", EnglishName, NativeName);
        }
    }
}
