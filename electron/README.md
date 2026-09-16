# Windows Memory Cleaner — Electron edition

A modern **Electron** desktop front-end for [WinMemoryCleaner](https://github.com/IgorMundstein/WinMemoryCleaner),
with a redesigned "Liquid Glass" interface. All the original kernel-level memory work is
**preserved unchanged** — it runs in a native C# helper process that Electron drives over a
JSON protocol.

## Why this architecture

WinMemoryCleaner's core is not web code: it calls Windows kernel APIs via P/Invoke
(`EmptyWorkingSet`, `NtSetSystemInformation`, `SetSystemFileCacheSize`, `DeviceIoControl`, …),
which Node.js/Chromium cannot do. So the app is split cleanly:

```
┌─────────────────────────────┐   newline-delimited JSON over stdio   ┌──────────────────────────┐
│  Electron (renderer + main) │  ───────────────────────────────────▶ │  wmc-engine.exe (C#)     │
│  UI · tray · hotkey · sched │  ◀─────────────────────────────────── │  P/Invoke · admin        │
└─────────────────────────────┘        requests / events              └───────────┬──────────────┘
                                                                                   │
                                                                          Windows kernel APIs
```

| Layer | Path | Responsibility |
|-------|------|----------------|
| **Engine** | `engine/` | Memory stats, OS capability detection, the 8 optimization routines (ported 1:1 from `ComputerService`), process list, and settings persistence to the **original** `HKLM\SOFTWARE\WinMemoryCleaner` registry keys. |
| **Main** | `main/main.js`, `main/engine.js` | Window lifecycle, tray, global optimize hotkey, auto-optimization scheduler (interval + low-memory threshold), notifications, run-on-startup, engine spawn + protocol. |
| **Preload** | `preload/preload.js` | The only bridge to the renderer — a minimal `window.wmc` API via `contextBridge`. `contextIsolation` on, `nodeIntegration` off. |
| **Renderer** | `renderer/` | The redesigned UI (vanilla HTML/CSS/JS, no bundler). |

### Data compatibility
The engine reads and writes the **same registry location** the original app uses, so any
existing configuration (memory areas, language, auto-optimization, exclusions) carries over
untouched.

## Requirements
- Windows 10/11
- [.NET SDK 8+](https://dotnet.microsoft.com/) (built and tested with SDK 10)
- [Node.js 18+](https://nodejs.org/)

## Develop / run

```sh
cd electron
npm install
npm run build:engine     # compiles wmc-engine (dotnet)
npm start                # launches Electron
```

The engine binary is manifested `requireAdministrator`, so **full optimization requires the
app to run elevated** (right-click → Run as administrator, or launch from an elevated shell).
For convenience during development, if the app is not elevated the bridge automatically falls
back to launching the engine via `dotnet <dll>` (which bypasses the manifest). In that mode the
UI, live memory and settings all work; the privileged optimizations simply report a per-area
error instead of succeeding — exactly like the original app when not elevated.

Force the dotnet fallback explicitly:

```sh
WMC_ENGINE_VIA_DOTNET=1 npm start
```

## Package

```sh
npm run build:engine
npm run dist             # electron-builder → NSIS installer, requestedExecutionLevel=requireAdministrator
```

`electron-builder` bundles the compiled engine from `engine/bin/Release/net10.0-windows`
into the installer's `resources/engine` folder.

## JSON protocol (engine)

Requests are newline-delimited JSON on stdin; responses/events on stdout.

| Command | Args | Result |
|---------|------|--------|
| `ping` | – | `{ pong, version, elevated }` |
| `getState` | – | `{ memory, os, settings, elevated }` |
| `getMemory` | – | `{ physical, virtual }` (bytes + percentages) |
| `getProcesses` | – | `string[]` of process names |
| `getSettings` / `saveSettings` | (settings) | reads/writes the original registry keys |
| `setPriority` | `{ priority: "low"\|"normal" }` | sets the engine process priority |
| `optimize` | `{ areas:int, processExclusion:[], reason }` | streams `optimizeStart`/`progress` events, returns `{ before, after, released, results[] }` |

## License
GPL-3.0, same as the original project.
