using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace WinMemoryCleaner
{
    internal sealed class HotKeyManager : IDisposable
    {
        private readonly Dictionary<HotKey, Action> _registered = new Dictionary<HotKey, Action>();

        internal HotKeyManager()
        {
            ComponentDispatcher.ThreadPreprocessMessage += OnThreadPreprocessMessage;
        }

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
            var hotKey = new HotKey
            {
                Key = KeyInterop.KeyFromVirtualKey(((int)msg.lParam >> 16) & 0xFFFF),
                ModifierKeys = (ModifierKeys)((int)msg.lParam & 0xFFFF)
            };

            if (_registered.TryGetValue(hotKey, out action))
                action();
        }

        internal void Register(HotKey hotkey, Action action)
        {
            if (_registered.ContainsKey(hotkey))
                Unregister(hotkey);

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (!NativeMethods.RegisterHotKey(IntPtr.Zero, hotkey.GetHashCode(), (uint)hotkey.ModifierKeys, (uint)KeyInterop.VirtualKeyFromKey(hotkey.Key)))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }));

            _registered.Add(hotkey, action);
        }

        internal void Unregister(HotKey hotkey)
        {
            if (!_registered.ContainsKey(hotkey))
                return;

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (!NativeMethods.UnregisterHotKey(IntPtr.Zero, hotkey.GetHashCode()))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }));

            _registered.Remove(hotkey);
        }
    }
}