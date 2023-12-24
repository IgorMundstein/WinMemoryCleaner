using System.Windows.Input;

namespace WinMemoryCleaner
{
    /// <summary>
    /// HotKey
    /// </summary>
    public class HotKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HotKey" /> class.
        /// </summary>
        /// <param name="modifiers">The modifiers.</param>
        /// <param name="key">The key.</param>
        public HotKey(ModifierKeys modifiers, Key key)
        {
            Key = key;
            Modifiers = modifiers;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public Key Key { get; private set; }

        /// <summary>
        /// Gets the modifiers.
        /// </summary>
        /// <value>
        /// The modifiers.
        /// </value>
        public ModifierKeys Modifiers { get; private set; }

        /// <summary>
        /// Equalses the specified hotkey.
        /// </summary>
        /// <param name="hotKey">The hotkey.</param>
        /// <returns></returns>
        public bool Equals(HotKey hotKey)
        {
            if (ReferenceEquals(null, hotKey))
                return false;

            if (ReferenceEquals(this, hotKey))
                return true;

            return Equals(hotKey.Key, Key) && Equals(hotKey.Modifiers, Modifiers);
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

            return obj.GetType() == typeof(HotKey) && Equals((HotKey)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Key.GetHashCode() * 397) ^ Modifiers.GetHashCode();
            }
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(Localizer.Culture, "{0}, {1}", Modifiers, Key);
        }
    }
}
