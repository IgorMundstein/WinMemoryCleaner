using System.Windows.Input;

namespace WinMemoryCleaner
{
    internal class HotKey
    {
        internal Key Key { get; set; }

        internal ModifierKeys ModifierKeys { get; set; }

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

            return Equals(hotKey.Key, Key) && Equals(hotKey.ModifierKeys, ModifierKeys);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Key.GetHashCode() * 397) ^ ModifierKeys.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", ModifierKeys, Key);
        }
    }
}
