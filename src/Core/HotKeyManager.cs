using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace WinMemoryCleaner
{
    internal sealed class HotKeyManager : IDisposable
    {
        private readonly bool _isSupported = Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6; // Minimum supported Windows Vista / Server 2003
        private readonly Dictionary<HotKey, Action> _registered = new Dictionary<HotKey, Action>();

        internal HotKeyManager()
        {
            if (!_isSupported)
                return;

            Keys = new List<Key>
            (
                Enum.GetValues(typeof(Key))
                    .Cast<Key>()
                    .Where(key => new Regex("^([A-Z]|F([1-9]|1[0-2]))$", RegexOptions.IgnoreCase) // (A-Z) (F1-F12)
                    .Match(key.ToString().ToUpper()).Success)
            );

            Modifiers = new Dictionary<ModifierKeys, string>
            {
                { ModifierKeys.Alt | ModifierKeys.Shift, "ALT + SHIFT" },
                { ModifierKeys.Control | ModifierKeys.Alt, "CTRL + ALT" },
                { ModifierKeys.Control | ModifierKeys.Shift, "CTRL + SHIFT" }
            };

            ComponentDispatcher.ThreadPreprocessMessage += OnThreadPreprocessMessage;
        }

        internal readonly List<Key> Keys;
        internal readonly Dictionary<ModifierKeys, string> Modifiers;

        public void Dispose()
        {
            try
            {
                var hotKeys = _registered.Keys.ToList();

                foreach (var hotKey in hotKeys)
                {
                    try
                    {
                        Unregister(hotKey);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private void OnThreadPreprocessMessage(ref MSG msg, ref bool handled)
        {
            if (msg.message != Constants.Windows.Keyboard.WmHotkey)
                return;

            Action action;
            HotKey hotKey = null;

            try
            {
                hotKey = new HotKey
                ( 
                    key: KeyInterop.KeyFromVirtualKey(((int)msg.lParam >> 16) & 0xFFFF),
                    modifiers: (ModifierKeys)((int)msg.lParam & 0xFFFF)
                );
            }
            catch (Exception e)
            {
                Logger.Debug(e.GetBaseException().Message);
            }

            if (hotKey != null && _registered.TryGetValue(hotKey, out action))
                action();
        }

        internal bool Register(HotKey hotkey, Action action)
        {
            bool result = false;

            try
            {
                if (!_isSupported || hotkey == null || action == null)
                    return false;

                Unregister(hotkey);

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    result = NativeMethods.RegisterHotKey(IntPtr.Zero, hotkey.GetHashCode(), (uint)hotkey.Modifiers, (uint)KeyInterop.VirtualKeyFromKey(hotkey.Key));
                }));

                if (!_registered.ContainsKey(hotkey))
                    _registered.Add(hotkey, action);
            }
            catch
            {
                // ignored
            }

            return result;
        }

        internal bool Unregister(HotKey hotkey)
        {
            bool result = false;

            try
            {
                if (!_isSupported || hotkey == null)
                    return false;

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    result = NativeMethods.UnregisterHotKey(IntPtr.Zero, hotkey.GetHashCode());
                }));

                if (_registered.ContainsKey(hotkey))
                    _registered.Remove(hotkey);
            }
            catch
            {
                // ignored
            }

            return result;
        }
    }
}