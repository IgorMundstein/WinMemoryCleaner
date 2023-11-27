using System.Windows.Input;

namespace WinMemoryCleaner
{
    internal class HotKey
    {
        internal HotKey(ModifierKeys modifiers, Key key)
        {
            Key = key;
            Modifiers = modifiers;
        }

        internal Key Key { get; private set; }

        internal ModifierKeys Modifiers { get; private set; }

        internal bool Equals(HotKey hotKey)
        {
            if (ReferenceEquals(null, hotKey))
                return false;

            if (ReferenceEquals(this, hotKey))
                return true;

            return Equals(hotKey.Key, Key) && Equals(hotKey.Modifiers, Modifiers);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == typeof(HotKey) && Equals((HotKey)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Key.GetHashCode() * 397) ^ Modifiers.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Format(Localizer.Culture, "{0}, {1}", Modifiers, Key);
        }
    }
}
