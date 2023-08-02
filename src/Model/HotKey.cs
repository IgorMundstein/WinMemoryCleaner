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

        internal readonly Key Key;
        internal readonly ModifierKeys Modifiers;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == typeof(HotKey) && Equals((HotKey)obj);
        }

        internal bool Equals(HotKey hotKey)
        {
            if (ReferenceEquals(null, hotKey))
                return false;

            if (ReferenceEquals(this, hotKey))
                return true;

            return Equals(hotKey.Key, Key) && Equals(hotKey.Modifiers, Modifiers);
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
            return string.Format("{0}, {1}", Modifiers, Key);
        }
    }
}
