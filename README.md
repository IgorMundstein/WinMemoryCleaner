<div align="center">
  <h1>Windows Memory Cleaner</h1>

  [![](https://img.shields.io/badge/Windows-XP%20%7C%20Vista%20%7C%207%20%7C%208%20%7C%2010%20%7C%2011-blue?style=for-the-badge)](#windows-memory-cleaner)
  [![](https://img.shields.io/badge/Server-2003%20%7C%202008%20%7C%202012%20%7C%202016%20%7C%202019%20%7C%202022-blue?style=for-the-badge)](#windows-memory-cleaner)

  [![](https://img.shields.io/github/license/IgorMundstein/WinMemoryCleaner?style=for-the-badge)](/LICENSE)
  [![](https://img.shields.io/github/downloads/IgorMundstein/WinMemoryCleaner/total?style=for-the-badge)](https://github.com/IgorMundstein/WinMemoryCleaner/releases/latest/)

  [![](/.github/images/main-window.png)](#windows-memory-cleaner)

  <p align="justify">
    This free RAM cleaner uses native Windows features to clear memory areas. Sometimes, programs do not release the allocated memory, making the computer slow. That is when you use Windows Memory Cleaner to optimize the memory, so you can keep working without wasting time restarting your system. 
  </p>

  <p align="justify">
    It's portable, so you do not need installation or configuration. Download and open the executable to get started. The app requires administrator privileges and has a minimalistic interface and smart features.
  </p>

  [![Download)](/.github/images/download-button.png)](https://github.com/IgorMundstein/WinMemoryCleaner/releases/latest/download/WinMemoryCleaner.exe)
</div>

## 🚀 Features

### Auto optimization

- `Every X hours` - The optimization will run by period. 0h disables it.
- `When free memory is below X percent` - The optimization will run if free memory is below the specified percentage. 0% disables it.

### Memory Areas

- `Clean Combined Page List` - Flushes blocks from the combined page list. Effective only when page combining is enabled.
- `Clean Modified Page List` - Flushes memory from the Modified page list, writing unsaved data to disk and moving the pages to the Standby list.
- `Clean Processes Working Set` - Removes memory from all user-mode and system working sets and moves it to the Standby or Modified page lists. Note that by the time, processes that run any code will necessarily populate their working sets to do so.
- `Clean Standby List` - Flushes pages from all Standby list to the Free list.
- `Clean Standby List (Low Priority)` - Flushes pages from the lowest-priority Standby list to the Free list.
- `Clean System Working Set` - Removes memory from the system cache working set.

### Multi-Language

- `English` `Portuguese`

### Processes excluded from optimization

- Allows the user to create a list of processes that will be ignored when memory is optimized.

### Settings

- `Always on top` - Pins the window to the top of all your windows.
- `Auto update` - Keeps the app up to date.
- `Minimize to the tray when closed` - Minimize the app to the system tray when clicking the close (X) button.
- `Run on startup` - Runs the app after the system boots up.
- `Show optimization notifications` - Sends a message to the notification area after optimization.
- `Start minimized` - The app will start minimized to the system tray. Single/Double click on the icon to restore.

## 🖥️ Command arguments (NO GUI)

The arguments below can be used to run the program silently.

- `/CombinedPageList`
- `/ModifiedPageList`
- `/ProcessesWorkingSet`
- `/StandbyList` OR `/StandbyListLowPriority`
- `/SystemWorkingSet`

Example

`WinMemoryCleaner.exe /ModifiedPageList /ProcessesWorkingSet /StandbyList /SystemWorkingSet`

## 📖 Logs

Logs are saved on windows event.

1. Press **Win + R** to open the Run command dialog box.
2. Type **eventvwr** and press **Enter** to open the Event Viewer.

[![](/.github/images/windows-event-log.png)](#-logs)

## 📝 Development notes

Project requirements.

- Microsoft.NET 4 framework version for Windows retro compatibility
- Minimalistic user interface
- Model-View-ViewModel (MVVM) design pattern
- No third library or DLL dependencies
- Portable (Single .exe file)
- S.O.L.I.D. Principles of Object-Oriented
- Use of Windows native methods for memory management
- Windows Presentation Foundation (WPF) for user interface
- Windows Registry to save user config

## 🌐 Translation

If you are a native speaker of any language other than English, you can contribute by translating the file: [English.json](/src/Resources/Localization/English.json)
