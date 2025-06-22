using System;
using System.Linq;
using System.Windows.Input;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Hotkey
    /// </summary>
    public class Hotkey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hotkey" /> class.
        /// </summary>
        /// <param name="modifiers">The modifiers.</param>
        /// <param name="key">The key.</param>
        public Hotkey(ModifierKeys modifiers, Key key)
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
        public bool Equals(Hotkey hotKey)
        {
            if (ReferenceEquals(null, hotKey))
                return false;

            if (ReferenceEquals(this, hotKey))
                return true;

            return Equals(hotKey.Key, Key) && Equals(hotKey.Modifiers, Modifiers);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == typeof(Hotkey) && Equals((Hotkey)obj);
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
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var modifiers = Enum.GetValues(typeof(ModifierKeys))
                .Cast<ModifierKeys>()
                .Where(flag => Modifiers.HasFlag(flag) && flag != 0)
                .OrderByDescending(flag => flag.ToString());

            return string.Format(Localizer.Culture, "{0} + {1}", string.Join(" + ", modifiers), Key).ToUpper(Localizer.Culture);
        }
    }
}
