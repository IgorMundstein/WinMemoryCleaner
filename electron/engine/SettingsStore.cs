using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WinMemoryCleaner.Engine
{
    /// <summary>
    /// Reads and writes settings to the SAME registry location used by the original
    /// WinMemoryCleaner (HKLM\SOFTWARE\WinMemoryCleaner). This preserves any configuration
    /// a user already had before migrating to the Electron edition.
    ///
    /// Value names match the original Core/Settings.cs where they overlap; a few new keys
    /// (Theme) are added alongside them for the redesigned UI.
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal static class SettingsStore
    {
        private const string SettingsKey = @"SOFTWARE\WinMemoryCleaner";
        private const string ExclusionKey = @"SOFTWARE\WinMemoryCleaner\ProcessExclusionList";

        // Defaults mirror the original Settings.Load().
        private static readonly Dictionary<string, object> Defaults = new()
        {
            ["AlwaysOnTop"] = false,
            ["AutoOptimizationInterval"] = 0,
            ["AutoOptimizationMemoryUsage"] = 0,
            ["AutoUpdate"] = true,
            ["CloseAfterOptimization"] = false,
            ["CloseToTheNotificationArea"] = false,
            ["CompactMode"] = false,
            ["CreateStartMenuShortcut"] = true,
            ["FontSize"] = 14.0,
            ["Language"] = "en",
            // CombinedPageList|ModifiedFileCache|ModifiedPageList|RegistryCache|StandbyList|SystemFileCache|WorkingSet
            ["MemoryAreas"] = (int)(MemoryAreas.CombinedPageList | MemoryAreas.ModifiedFileCache |
                                    MemoryAreas.ModifiedPageList | MemoryAreas.RegistryCache |
                                    MemoryAreas.StandbyList | MemoryAreas.SystemFileCache | MemoryAreas.WorkingSet),
            ["RunOnPriority"] = 0, // Low
            ["RunOnStartup"] = false,
            ["ShowOptimizationNotifications"] = true,
            ["ShowVirtualMemory"] = false,
            ["StartMinimized"] = false,
            ["UseHotkey"] = false,
            ["HotkeyModifiers"] = "Control+Shift",
            ["HotkeyKey"] = "M",
            ["Theme"] = "dark"
        };

        private static readonly HashSet<string> BoolKeys = new(StringComparer.Ordinal)
        {
            "AlwaysOnTop", "AutoUpdate", "CloseAfterOptimization", "CloseToTheNotificationArea",
            "CompactMode", "CreateStartMenuShortcut", "RunOnStartup", "ShowOptimizationNotifications",
            "ShowVirtualMemory", "StartMinimized", "UseHotkey"
        };

        private static readonly HashSet<string> IntKeys = new(StringComparer.Ordinal)
        {
            "AutoOptimizationInterval", "AutoOptimizationMemoryUsage", "MemoryAreas", "RunOnPriority"
        };

        private static readonly HashSet<string> DoubleKeys = new(StringComparer.Ordinal) { "FontSize" };

        public static JsonObject Read()
        {
            var result = new JsonObject();
            using (var key = Registry.LocalMachine.OpenSubKey(SettingsKey))
            {
                foreach (var kv in Defaults)
                {
                    var raw = key?.GetValue(kv.Key);
                    result[kv.Key] = ToNode(kv.Key, raw, kv.Value);
                }
            }
            result["ProcessExclusionList"] = ReadExclusionList();
            return result;
        }

        private static JsonArray ReadExclusionList()
        {
            var array = new JsonArray();
            using (var key = Registry.LocalMachine.OpenSubKey(ExclusionKey))
            {
                if (key != null)
                    foreach (var name in key.GetValueNames().OrderBy(n => n))
                        array.Add(name);
            }
            return array;
        }

        private static JsonNode ToNode(string name, object raw, object fallback)
        {
            try
            {
                if (BoolKeys.Contains(name))
                    return JsonValue.Create(raw == null ? (bool)fallback : Convert.ToInt32(raw, CultureInfo.InvariantCulture) != 0);
                if (IntKeys.Contains(name))
                    return JsonValue.Create(raw == null ? (int)fallback : Convert.ToInt32(raw, CultureInfo.InvariantCulture));
                if (DoubleKeys.Contains(name))
                    return JsonValue.Create(raw == null ? (double)fallback : Convert.ToDouble(raw, CultureInfo.InvariantCulture));
                return JsonValue.Create(raw?.ToString() ?? (string)fallback);
            }
            catch
            {
                return JsonValue.Create(fallback?.ToString());
            }
        }

        public static void Write(JsonObject settings)
        {
            using (var key = Registry.LocalMachine.CreateSubKey(SettingsKey))
            {
                if (key == null) return;

                foreach (var kv in Defaults)
                {
                    if (!settings.TryGetPropertyValue(kv.Key, out var node) || node == null)
                        continue;

                    if (BoolKeys.Contains(kv.Key))
                        key.SetValue(kv.Key, node.GetValue<bool>() ? 1 : 0, RegistryValueKind.DWord);
                    else if (IntKeys.Contains(kv.Key))
                        key.SetValue(kv.Key, node.GetValue<int>(), RegistryValueKind.DWord);
                    else if (DoubleKeys.Contains(kv.Key))
                        key.SetValue(kv.Key, node.GetValue<double>().ToString(CultureInfo.InvariantCulture), RegistryValueKind.String);
                    else
                        key.SetValue(kv.Key, node.ToString(), RegistryValueKind.String);
                }
            }

            if (settings.TryGetPropertyValue("ProcessExclusionList", out var listNode) && listNode is JsonArray list)
                WriteExclusionList(list);
        }

        private static void WriteExclusionList(JsonArray list)
        {
            Registry.LocalMachine.DeleteSubKey(ExclusionKey, false);
            if (list.Count == 0) return;

            using (var key = Registry.LocalMachine.CreateSubKey(ExclusionKey))
            {
                if (key == null) return;
                foreach (var item in list)
                {
                    var name = item?.ToString();
                    if (!string.IsNullOrWhiteSpace(name))
                        key.SetValue(name.Replace(".exe", string.Empty).Trim().ToLowerInvariant(), string.Empty, RegistryValueKind.String);
                }
            }
        }

        public static List<string> ReadExclusionNames()
        {
            var list = new List<string>();
            using (var key = Registry.LocalMachine.OpenSubKey(ExclusionKey))
            {
                if (key != null)
                    list.AddRange(key.GetValueNames());
            }
            return list;
        }
    }
}
