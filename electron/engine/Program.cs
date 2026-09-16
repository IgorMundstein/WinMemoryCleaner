using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace WinMemoryCleaner.Engine
{
    /// <summary>
    /// Headless memory-cleaning engine. Speaks a newline-delimited JSON protocol over
    /// stdin/stdout so the Electron front-end (or any host) can drive it.
    ///
    ///   Request : {"id":1,"cmd":"optimize","args":{...}}\n
    ///   Response: {"id":1,"ok":true,"result":{...}}\n
    ///   Event   : {"type":"event","id":1,"event":"progress","data":{...}}\n
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal static class Program
    {
        private const string Version = "1.0.0";

        private static readonly object _stdoutLock = new();
        private static readonly SemaphoreSlim _optimizeGate = new(1, 1);
        private static readonly OsInfo _os = new();

        private static int Main(string[] args)
        {
            // Force invariant, UTF-8, no buffering surprises.
            var stdout = new StreamWriter(Console.OpenStandardOutput(), new UTF8Encoding(false)) { AutoFlush = true };
            Console.SetOut(stdout);

            // Announce readiness so the host knows the pipe is live.
            Emit(new JsonObject
            {
                ["type"] = "ready",
                ["version"] = Version,
                ["elevated"] = IsElevated(),
                ["pid"] = Environment.ProcessId
            });

            using var stdin = new StreamReader(Console.OpenStandardInput(), Encoding.UTF8);
            var inFlight = new List<Task>();
            string line;
            while ((line = stdin.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Dispatch each request independently so cheap reads aren't blocked by a long optimize.
                var captured = line;
                lock (inFlight)
                {
                    inFlight.RemoveAll(t => t.IsCompleted);
                    inFlight.Add(Task.Run(() => Dispatch(captured)));
                }
            }

            // stdin closed: drain outstanding requests before exiting so no response is lost.
            Task[] pending;
            lock (inFlight) { pending = inFlight.ToArray(); }
            try { Task.WaitAll(pending, TimeSpan.FromSeconds(30)); } catch { /* ignored */ }

            return 0;
        }

        private static void Dispatch(string line)
        {
            JsonNode request;
            try
            {
                request = JsonNode.Parse(line);
            }
            catch (Exception e)
            {
                Emit(new JsonObject { ["ok"] = false, ["error"] = "Invalid JSON: " + e.Message });
                return;
            }

            var id = request?["id"]?.GetValue<int?>() ?? 0;
            var cmd = request?["cmd"]?.ToString();
            var argsNode = request?["args"] as JsonObject ?? new JsonObject();

            try
            {
                JsonNode result = cmd switch
                {
                    "ping" => Ping(),
                    "getState" => GetState(),
                    "getMemory" => MemoryToJson(MemorySnapshot.Read()),
                    "getProcesses" => GetProcesses(),
                    "getSettings" => SettingsStore.Read(),
                    "saveSettings" => SaveSettings(argsNode),
                    "setPriority" => SetPriority(argsNode),
                    "optimize" => Optimize(id, argsNode),
                    _ => throw new Exception("Unknown command: " + cmd)
                };

                Respond(id, result);
            }
            catch (Exception e)
            {
                Emit(new JsonObject { ["id"] = id, ["ok"] = false, ["error"] = Optimizer.GetMessage(e) });
            }
        }

        #region Commands

        private static JsonObject Ping() => new()
        {
            ["pong"] = true,
            ["version"] = Version,
            ["elevated"] = IsElevated()
        };

        private static JsonObject GetState() => new()
        {
            ["memory"] = MemoryToJson(MemorySnapshot.Read()),
            ["os"] = OsToJson(),
            ["settings"] = SettingsStore.Read(),
            ["elevated"] = IsElevated()
        };

        private static JsonObject SaveSettings(JsonObject args)
        {
            SettingsStore.Write(args);
            return new JsonObject { ["saved"] = true };
        }

        private static JsonObject SetPriority(JsonObject args)
        {
            var priority = args["priority"]?.ToString()?.ToLowerInvariant();
            try
            {
                using var self = Process.GetCurrentProcess();
                self.PriorityClass = priority == "normal"
                    ? ProcessPriorityClass.Normal
                    : ProcessPriorityClass.Idle; // "low"
            }
            catch { /* best effort */ }
            return new JsonObject { ["priority"] = priority };
        }

        private static JsonArray GetProcesses()
        {
            var names = Process.GetProcesses()
                .Where(p => p != null)
                .Select(p =>
                {
                    try { return p.ProcessName; } catch { return null; }
                    finally { try { p.Dispose(); } catch { } }
                })
                .Where(n => !string.IsNullOrWhiteSpace(n) &&
                            !n.Equals("wmc-engine", StringComparison.OrdinalIgnoreCase))
                .Select(n => n.ToLowerInvariant().Replace(".exe", string.Empty))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(n => n, StringComparer.OrdinalIgnoreCase);

            var array = new JsonArray();
            foreach (var n in names) array.Add(n);
            return array;
        }

        private static JsonObject Optimize(int id, JsonObject args)
        {
            var areas = (MemoryAreas)(args["areas"]?.GetValue<int?>() ?? 0);
            // Never attempt an area the OS doesn't support.
            areas &= _os.SupportedAreas;

            var exclusions = (args["processExclusion"] as JsonArray)?
                .Select(n => n?.ToString())
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList() ?? new List<string>();

            var reason = args["reason"]?.ToString() ?? "Manual";

            // Only one optimization at a time.
            _optimizeGate.Wait();
            try
            {
                var before = MemorySnapshot.Read();

                var totalSteps = CountBits((int)areas) + 1; // +1 for the GC step
                Emit(new JsonObject
                {
                    ["type"] = "event",
                    ["id"] = id,
                    ["event"] = "optimizeStart",
                    ["data"] = new JsonObject { ["total"] = totalSteps, ["reason"] = reason }
                });

                var optimizer = new Optimizer(_os, exclusions);
                optimizer.OnProgress += (step, name) => Emit(new JsonObject
                {
                    ["type"] = "event",
                    ["id"] = id,
                    ["event"] = "progress",
                    ["data"] = new JsonObject
                    {
                        ["step"] = step,
                        ["total"] = totalSteps,
                        ["percent"] = (int)(step * 100.0 / totalSteps),
                        ["name"] = name
                    }
                });

                var results = optimizer.Optimize(areas);
                var after = MemorySnapshot.Read();

                var resultArray = new JsonArray();
                foreach (var r in results)
                {
                    resultArray.Add(new JsonObject
                    {
                        ["name"] = r.Name,
                        ["durationSeconds"] = Math.Round(r.DurationSeconds, 2),
                        ["success"] = r.Success,
                        ["error"] = r.Error
                    });
                }

                return new JsonObject
                {
                    ["reason"] = reason,
                    ["areas"] = (int)areas,
                    ["before"] = MemoryToJson(before),
                    ["after"] = MemoryToJson(after),
                    ["released"] = new JsonObject
                    {
                        ["physical"] = Math.Max(0, after.PhysicalFree - before.PhysicalFree),
                        ["virtual"] = Math.Max(0, after.VirtualFree - before.VirtualFree)
                    },
                    ["results"] = resultArray
                };
            }
            finally
            {
                _optimizeGate.Release();
            }
        }

        #endregion

        #region JSON helpers

        private static JsonObject MemoryToJson(MemorySnapshot m) => new()
        {
            ["physical"] = new JsonObject
            {
                ["total"] = m.PhysicalTotal,
                ["free"] = m.PhysicalFree,
                ["used"] = m.PhysicalUsed,
                ["usedPercent"] = m.PhysicalUsedPercent,
                ["freePercent"] = 100 - m.PhysicalUsedPercent
            },
            ["virtual"] = new JsonObject
            {
                ["total"] = m.VirtualTotal,
                ["free"] = m.VirtualFree,
                ["used"] = m.VirtualUsed,
                ["usedPercent"] = m.VirtualUsedPercent,
                ["freePercent"] = 100 - m.VirtualUsedPercent
            }
        };

        private static JsonObject OsToJson() => new()
        {
            ["is64Bit"] = _os.Is64Bit,
            ["supportedAreas"] = (int)_os.SupportedAreas,
            ["areas"] = new JsonObject
            {
                ["WorkingSet"] = _os.HasWorkingSet,
                ["SystemFileCache"] = _os.HasSystemFileCache,
                ["ModifiedFileCache"] = _os.HasModifiedFileCache,
                ["ModifiedPageList"] = _os.HasModifiedPageList,
                ["StandbyList"] = _os.HasStandbyList,
                ["StandbyListLowPriority"] = _os.HasStandbyList,
                ["CombinedPageList"] = _os.HasCombinedPageList,
                ["RegistryCache"] = _os.HasRegistryHive
            }
        };

        private static void Respond(int id, JsonNode result)
            => Emit(new JsonObject { ["id"] = id, ["ok"] = true, ["result"] = result });

        private static void Emit(JsonObject payload)
        {
            var json = payload.ToJsonString(new JsonSerializerOptions { WriteIndented = false });
            lock (_stdoutLock)
            {
                Console.Out.WriteLine(json);
            }
        }

        #endregion

        private static bool IsElevated()
        {
            try
            {
                using var identity = WindowsIdentity.GetCurrent();
                return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch { return false; }
        }

        private static int CountBits(int value)
        {
            var count = 0;
            while (value != 0) { count += value & 1; value >>= 1; }
            return count;
        }
    }
}
