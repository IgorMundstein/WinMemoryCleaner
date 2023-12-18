﻿using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WinMemoryCleaner
{
    /// <summary>
    /// IHotKeyService
    /// </summary>
    public interface IHotKeyService : IDisposable
    {
        /// <summary>
        /// Gets the keys.
        /// </summary>
        List<Key> Keys { get; }

        /// <summary>
        /// Gets the modifiers.
        /// </summary>
        Dictionary<ModifierKeys, string> Modifiers { get; }

        /// <summary>
        /// Registers the specified hotkey.
        /// </summary>
        /// <param name="hotkey">The hotkey.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        bool Register(HotKey hotkey, Action action);

        /// <summary>
        /// Unregisters the specified hotkey.
        /// </summary>
        /// <param name="hotkey">The hotkey.</param>
        /// <returns></returns>
        bool Unregister(HotKey hotkey);
    }
}
