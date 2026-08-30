# Contributing to WinMemoryCleaner

Thank you for your interest in contributing! This document outlines the process and standards for contributing to this project.

## Getting Started

1. **Fork** the repository on GitHub
2. **Clone** your fork locally
3. **Add upstream remote**: `git remote add upstream https://github.com/IgorMundstein/WinMemoryCleaner.git`
4. **Create a branch**: `git checkout -b feature/your-feature-name` or `fix/issue-number-description`

## Development Setup

### Prerequisites
- Windows 7 SP1 / Server 2012+
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Administrator privileges (required to run optimizations)

### Build & Run
```bash
# Restore dependencies
dotnet restore src/WinMemoryCleaner.csproj

# Build (Release)
dotnet build src/WinMemoryCleaner.csproj -c Release

# Run (framework-dependent)
dotnet run --project src/WinMemoryCleaner.csproj -c Release

# Or publish single-file
dotnet publish src/WinMemoryCleaner.csproj -c Release -o publish
```

## Code Style & Conventions

### C# Style
- **Language Version**: Latest (`LangVersion latest` in csproj)
- **Project Format**: SDK-style with `PackageReference`
- **Nullable**: Disabled (`<Nullable>disable</Nullable>`)
- **Implicit Usings**: Disabled
- **Formatting**: Follow existing code style (4-space indent, braces on new lines)

### Naming Conventions
- **Classes/Interfaces**: PascalCase (`ComputerService`, `IComputerService`)
- **Methods/Properties**: PascalCase (`Optimize`, `MemoryAreas`)
- **Fields**: `_camelCase` (`_memory`, `_cancellationTokenSource`)
- **Parameters/Locals**: camelCase (`processName`, `isOptimizing`)
- **Constants**: PascalCase (`AutoUpdateInterval`)

### Architecture Patterns
- **MVVM**: ViewModels in `ViewModel/`, Views in `View/`
- **Dependency Injection**: `DependencyInjection.Container.Register<TInterface, TImplementation>()`
- **Interfaces**: Prefix with `I` (`INotificationService`)
- **Async**: Prefer `async`/`await` over `.Result`/`.Wait()`

### Resource Management
- Implement `IDisposable` for classes holding unmanaged resources
- Use `Marshal.AllocHGlobal`/`FreeHGlobal` instead of `GCHandle.Alloc` for native interop
- Always dispose `IDisposable` in `finally` blocks or `using` statements

### Exception Handling
- **Never** use empty `catch { }` blocks
- Log exceptions with context: `Logger.Debug("Operation failed: " + ex.Message)`
- Use `ArgumentOutOfRangeException` for invalid enum values (not `NotImplementedException`)
- Implement `ConvertBack` for all `IValueConverter` implementations

## Pull Request Process

### Before Submitting
- [ ] Code builds clean: `dotnet build -c Release` (0 errors, 0 new warnings)
- [ ] Tested manually (both admin and non-admin scenarios)
- [ ] No breaking changes without discussion
- [ ] Updated `CHANGELOG.md` if user-facing changes
- [ ] Translations: **lowercase only** (app handles capitalization)

### PR Title Format
- `feat: Add new feature description` (new functionality)
- `fix: Resolve issue #XXX - brief description` (bug fixes)
- `refactor: Improve X without behavior change` (code improvements)
- `perf: Optimize X for better performance` (performance)
- `docs: Update documentation for X` (documentation)

### PR Description Template
```markdown
## Summary
Brief description of changes

## Related Issues
Fixes #XXX
Relates to #YYY

## Changes
- List of specific changes
- Another change

## Testing
- [ ] Build passes
- [ ] Tested as admin (optimizations work)
- [ ] Tested non-admin (graceful errors)
- [ ] Single-file publish works
- [ ] Localization loads correctly

## Screenshots (if UI changes)
```

## Translation Contributions

### Adding a New Language
1. Copy `src/Resources/Localization/English.json`
2. Rename to `{Locale-Description}.json` (e.g., `Slovenian.json`)
3. Translate **all values to lowercase** (app auto-capitalizes)
4. Save as UTF-8
5. Test: Place file next to `WinMemoryCleaner.exe` and launch
6. Submit PR or use [Translation Request template](https://github.com/IgorMundstein/WinMemoryCleaner/issues/new?template=translation_request.yml)

### Updating Existing Translations
- Only modify values, never keys
- Keep lowercase format
- Maintain placeholder formatting: `{0}`, `{1}`, etc.

## Issue Reporting

### Bug Reports
Use the [Bug Report template](https://github.com/IgorMundstein/WinMemoryCleaner/issues/new?template=bug_report.yml) with:
- OS version (`winver`)
- .NET version (`dotnet --version`)
- Steps to reproduce
- Expected vs actual behavior
- Event Viewer logs (source: "Windows Memory Cleaner")

### Feature Requests
Use the [Feature Request template](https://github.com/IgorMundstein/WinMemoryCleaner/issues/new?template=feature_request.yml) with:
- Use case description
- Proposed solution
- Alternatives considered

## Testing Guidelines

### Manual Testing Checklist
- [ ] App launches without errors (admin + non-admin)
- [ ] All 32+ languages load correctly
- [ ] Tray icon appears and updates
- [ ] Optimizations run and log to Event Viewer
- [ ] Settings persist to registry (HKLM)
- [ ] Service install/uninstall works
- [ ] Auto-update check doesn't crash
- [ ] Single-file publish runs (`PublishSingleFile=true`)
- [ ] Compact mode toggles
- [ ] Hotkey registration works

### Automated Tests
- Run: `dotnet test src/WinMemoryCleaner.csproj -c Release`
- Add tests for new functionality in `src/Test/`

## Architecture Overview

```
src/
├── App.xaml.cs                 # Application entry, lifecycle, single-instance
├── Core/
│   ├── Localizer.cs           # Localization (lazy init, cached, fallback)
│   ├── Settings.cs            # Registry persistence (thread-safe, ConcurrentDictionary)
│   ├── Logger.cs              # Structured logging (EventLog, console, trace)
│   ├── Updater.cs             # GitHub API, HttpClient, atomic update
│   ├── ThemeManager.cs        # Theme loading, brush caching
│   └── ComputerService.cs     # Native memory optimization (Marshal.AllocHGlobal)
├── Service/
│   ├── NotificationService.cs # Tray icon, memory usage rendering
│   ├── HotKeyService.cs       # Global hotkeys (ConcurrentDictionary)
│   └── ComputerService.cs     # IComputerService implementation
├── WindowsService/
│   ├── WinService.cs          # Background service (Interlocked guard)
│   └── WinServiceInstaller.cs # sc.exe-based installer
├── ViewModel/
│   ├── MainViewModel.cs       # Main UI logic, commands
│   └── Base/ViewModel.cs      # Base VM with IsBusy, Navigation
├── Model/
│   ├── Localization.cs        # 74 localized strings (public setters)
│   ├── Memory/*.cs            # Memory stats structures
│   └── OperatingSystem.cs     # OS version detection (fixed)
├── Interop/
│   ├── NativeMethods.cs       # P/Invoke signatures (SupportedOSPlatform)
│   └── ShellInterop.cs        # Shell links (IPersistFile)
└── Test/                      # Unit/Integration tests
```

## Release Process (Maintainers Only)

1. Update version in `src/WinMemoryCleaner.csproj` and `src/Properties/AssemblyInfo.cs`
2. Update `CHANGELOG.md`
3. Create GitHub Release with `dotnet publish` artifacts
4. SignPath.io handles code signing automatically via CI/CD

## Questions?

Open a [Discussion](https://github.com/IgorMundstein/WinMemoryCleaner/discussions) or check existing [Issues](https://github.com/IgorMundstein/WinMemoryCleaner/issues).