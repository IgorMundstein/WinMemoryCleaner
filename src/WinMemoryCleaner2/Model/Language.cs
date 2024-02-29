using System;
using System.Globalization;
using System.Windows;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Language
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Language" /> class.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <exception cref="ArgumentNullException">culture</exception>
        public Language(CultureInfo culture)
        {
            if (culture == null)
                throw new ArgumentNullException("culture");

            Direction = culture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            EnglishName = culture.TextInfo.ToTitleCase(culture.EnglishName);
            Name = culture.Name;
            NativeName = culture.TextInfo.ToTitleCase(culture.NativeName);
        }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public FlowDirection Direction { get; private set; }

        /// <summary>
        /// Gets the english name.
        /// </summary>
        /// <value>
        /// The english name.
        /// </value>
        public string EnglishName { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the name of the native.
        /// </summary>
        /// <value>
        /// The name of the native.
        /// </value>
        public string NativeName { get; private set; }

        /// <summary>
        /// Equalses the specified language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public bool Equals(Language language)
        {
            if (ReferenceEquals(null, language))
                return false;

            if (ReferenceEquals(this, language))
                return true;

            return Equals(language.Name, Name);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == typeof(Language) && Equals((Language)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return EnglishName == NativeName ? EnglishName : string.Format(Localizer.Culture, "{0} ({1})", EnglishName, NativeName);
        }
    }
}
