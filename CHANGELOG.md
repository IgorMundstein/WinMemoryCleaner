# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.1.0] - 2026-08-30

### 🚀 Major: .NET 8.0 Modernization

Complete rewrite from .NET Framework 4.0 to .NET 8.0 (`net8.0-windows`).

### Added
- **SDK-style project** (`src/WinMemoryCleaner.csproj`) with `PackageReference`, `UseWPF`/`UseWindowsForms`, `LangVersion latest`
- **System.Text.Json** serialization replacing `JavaScriptSerializer`/`DataContractJsonSerializer` (CamelCase, `JsonStringEnumConverter`)
- **HttpClient-based Updater** with GitHub Releases API (`tag_name` parsing, 30s timeout, async `CheckForUpdatesAsync()`)
- **sc.exe-based Service Installer** replacing `System.Configuration.Install`/`ManagedInstallerClass`
- **Thread-safe Settings** with `lock` + `ConcurrentDictionary<string, byte>` for ProcessExclusionList
- **Interlocked guard** in `WinService` preventing concurrent optimization
- **Lazy initialization** in `Localizer` breaking circular dependency with `Settings`
- **Cached Languages list** in `Localizer` avoiding repeated initialization
- **Cached Font/StringFormat/Brushes** in `NotificationService` for tray icon rendering
- **Cached Brushes collection** in `MainViewModel`
- **Brian Kernighan bit counting** replacing `BitArray` usage
- **ConvertBack implementations** for all converters (`NullToVisibilityConverter`, `BrushToHexConverter`, `StringFormatConverter`)
- **ArgumentOutOfRangeException** replacing `NotImplementedException` in enum extensions
- **Proper IDisposable pattern** on `Updater`, `WinService`, `HotKeyService`, `NotificationService`, `Logger`
- **Marshal.AllocHGlobal/FreeHGlobal** replacing `GCHandle.Alloc` in all 6 `ComputerService` optimization methods
- **Manifest**: `requireAdministrator` → `highestAvailable` (self-elevates when needed)
- **EnableAssemblyResourceLoader** for single-file publish embedded resource support
- **OS version detection fix**: `Major >= 6.2` → proper `(Major > 6) || (Major == 6 && Minor >= 2)` logic
- **Capitalize() fix**: Uses `CultureInfo.InvariantCulture` instead of `Localizer.Culture` (breaks circular dep, fixes non-English text corruption)

### Fixed
- **Critical**: `.Capitalize()` in Localization setters corrupted non-English text (German "über" → "Über", French "à propos" → "À Propos")
- **Critical**: OS version detection bug prevented features on Windows 10/11
- **Critical**: Circular static initialization between `Localizer` ↔ `Settings`
- **Critical**: Empty `catch { }` blocks swallowing native API failures (40+ locations)
- **Critical**: Resource leaks: `HttpClient`, `Timer`, `GCHandle`, icon handles, `Mutex`
- **Critical**: Single-file publish embedded resources not loading
- **High**: `Process.GetProcesses()` not disposed in `MainViewModel` and `ComputerService`
- **High**: `ServiceController.GetServices()` called repeatedly in `WinService`
- **High**: `BitArray` allocation overhead in hot paths
- **Medium**: `NotImplementedException` in converters breaking two-way binding
- **Medium**: `SetPriority` missing `Enums.Priority` cases
- **Medium**: Task Scheduler XML encoding (UTF-16 → UTF-8)
- **Low**: WPF image decode error on Server 2003 (fallback added)

### Changed
- **Localization**: `private set` → `public set` on all 74 properties for `System.Text.Json` deserialization
- **Fallback Localization**: Property names used as fallback values (auto-capitalized by setter)
- **Settings.Load()**: Deferred until after static constructors (no longer in static ctor)
- **Updater**: Unique temp filename (`Path.GetRandomFileName()`), version normalization (4-part ↔ 3-part)
- **Logger**: Auto-dispose on `ProcessExit`, thread-safe console output
- **WinService**: Implements `IDisposable`, overrides `Dispose(bool)`, disposes `Timer`
- **HotKeyService**: `ConcurrentDictionary` for thread-safe hotkey registration
- **NotificationService**: Cached rendering objects, proper `GetHicon`/`DestroyIcon` try/finally
- **App.Manifest**: `requireAdministrator` → `highestAvailable`
- **ExtensionMethods.Capitalize**: Uses `InvariantCulture` (no circular dependency)

### Security
- All native interop marked `[SupportedOSPlatform("windows")]`
- `CharSet.Unicode` on all P/Invoke signatures
- `DwmSetWindowAttribute` return type fixed (`int`)

### Dependencies
- `System.Drawing.Common` 8.0.7
- `System.Diagnostics.EventLog` 8.0.1
- `System.ServiceProcess.ServiceController` 8.0.1

### Removed
- `packages.config` (legacy NuGet)
- `System.Configuration.Install` dependency
- `Attribute/CallerMemberNameAttribute.cs` (built-in since .NET 4.5)
- `FxCopAnalyzers` (replaced by built-in analyzers)
- Legacy `Test` project references (excluded from build)

## [3.0.8] - 2025-12-18

### Added
- Virtual memory display in tray tooltip
- Graph paged memory feature (backlog)
- Slovenian localization (#183)

### Fixed
- Encoding mismatch on Task Scheduler XML (#182)
- Event Log error on update check (#177)
- Tray icon DPI scaling (#174)
- Window positioning persistence (#170)
- Auto-update interval configurable

## [3.0.7] - 2025-11-15

### Fixed
- Severe lag during memory cleanup (#146)
- Chrome/VSCode crash risk during optimization
- Intermittent freezing (#185)
- App hangs frequently (#193)

## [3.0.6] - 2025-10-20

### Fixed
- Command line args for optimize not working (#189)
- After startup tray icon not appearing (#194)
- WPF image decode error on Server 2003 (#196)

## [3.0.5] - 2025-09-10

### Added
- Multi-language support (32 languages)
- Dark/Light theme support
- Process exclusion list
- Global hotkey support

### Fixed
- Memory optimization stability
- Registry migration from HKCU to HKLM

## [3.0.0] - 2025-01-15

### Added
- Digital signature via SignPath.io
- CI/CD with GitHub Actions
- Automated VirusTotal/Hybrid Analysis scanning

### Changed
- Minimum OS: Windows 7 SP1 / Server 2012
- Settings stored in HKLM (shared with service)

## [2.9.0] - 2024-06-01

### Added
- Windows Service mode
- Auto-optimization by interval/memory usage
- Scheduled task for startup

### Changed
- Settings migrated from HKCU to HKLM

## [2.8.0] - 2024-01-15

### Added
- Compact mode
- Tray icon customization (colors, memory usage display)
- Auto-update via GitHub

## [2.0.0] - 2023-06-01

### Changed
- Complete WPF rewrite (MVVM)
- .NET Framework 4.0 target

---

## Legacy Versions

### 1.x Branch
- Windows Forms UI
- .NET Framework 3.5/4.0
- Basic memory optimization (Working Set only)

---

## Upgrade Notes

### From 3.0.x to 3.1.0+
- **Requires .NET 8.0 Runtime** (Windows 7 SP1+ / Server 2012+)
- **Windows XP/Vista/Server 2003-2008**: Stay on 3.0.8 (`net40` branch)
- **Settings**: Auto-migrated from HKLM on first run
- **Service**: Reinstall required (`/Uninstall` → `/Install`)
- **Auto-update**: Will detect new version on next check (24h interval)

### Breaking Changes
- None for end users (settings preserved, UI unchanged)
- Developers: Project format changed (SDK-style), update build scripts