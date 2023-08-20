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
    The app has a minimalistic interface and smart features. It's portable, and you do not need to install it, but it requires administrator privileges to run. Click on the download button below and run the executable to get started.
  </p>

  [![Download)](/.github/images/download-button.png)](https://github.com/IgorMundstein/WinMemoryCleaner/releases/latest/download/WinMemoryCleaner.exe)

</div>

## 🚀 Features

### Auto optimization

- `Every X hours` - The optimization will run by period
- `When free memory is below X percent` - The optimization will run if free memory is below the specified percentage

### Compact Mode

- Arrow (Up/Down) next to the minimize button to collapse and extend the window

[![](/.github/images/main-window-compact.png)](#compact-mode)

### Memory Areas

- `Clean Combined Page List` - Flushes the blocks from the combined page list effectively only when page combining is enabled
- `Clean Modified Page List` - Flushes memory from the modified page list, writing unsaved data to disk and moving the pages to the standby list
- `Clean Processes Working Set` - Removes memory from all user-mode and system working sets and moves it to the standby or modified page lists. Note that by the time processes run, any code will necessarily populate their working sets to do so
- `Clean Standby List` - Flushes pages from all standby lists to the free list
- `Clean Standby List (Low Priority)` - Flushes pages from the lowest-priority standby list to the free list
- `Clean System Working Set` - Removes memory from the system cache working set

### Multi-Language

- `Arabic` `Chinese` `Dutch` `English` `French` `German` `Greek` `Indonesian` `Italian` `Japanese` `Korean` `Macedonian` `Polish` `Portuguese` `Serbian` `Slovenian` `Spanish` `Turkish` `Ukrainian`

### Notifications

[![](/.github/images/notification.png)](#notifications)

### Processes excluded from optimization

- You can build a list of processes to ignore when memory is optimized

### Optimization hotkey (Global)

- `CTRL + ALT + M (Customizable)` - Optimize

### Settings

- `Always on top` - Pins the window to the top of all your windows
- `Auto update` - Keeps the app up to date
- `Close after optimization` - Closes the app after optimization
- `Close to the notification area` - Minimize the app to the system tray when clicking the close (X) button
- `Run on startup` - Runs the app after the system boots up
- `Show optimization notifications` - Sends a message to the notification area after optimization
- `Start minimized` - The app will start minimized to the system tray. Single/Double click on the icon to restore

### System tray (Notification area)

[![](/.github/images/system-tray.png)](#system-tray)

## 🖥️ Command arguments (NO GUI)

You can use the following arguments to run the app silently.

- `/CombinedPageList`
- `/ModifiedPageList`
- `/ProcessesWorkingSet`
- `/StandbyList` OR `/StandbyListLowPriority`
- `/SystemWorkingSet`

Example

`WinMemoryCleaner.exe /ModifiedPageList /ProcessesWorkingSet /StandbyList /SystemWorkingSet`

## 📖 Logs

The app generates logs in the Windows event

1. Press **Win + R** to open the Run command dialog box
2. Type **eventvwr** and press **Enter** to open the Event Viewer

[![](/.github/images/windows-event-log.png)](#-logs)

## 📝 Project notes

- Microsoft.NET 4 framework version for Windows retro compatibility
- Minimalistic user interface
- Model-View-ViewModel (MVVM) design pattern
- No third library or DLL dependencies
- Portable (Single .exe file)
- S.O.L.I.D. Principles of Object-Oriented
- Use of Windows native methods for memory management
- Windows Event to save logs
- Windows Presentation Foundation (WPF) for user interface
- Windows Registry to save user config

## 🌐 Translation

If you are a native speaker of any language other than English, you can contribute by translating the file: [English.json](/src/Resources/Localization/English.json)

💡 You can test any translation by creating a file alongside the executable
1. Visit [https://ss64.com/locale.html](https://ss64.com/locale.html) to get the **locale description** of the language
2. Save it as **{locale-description}.json** using **UTF-8** as character encoding
3. Launch the application. If successful, the new language and changes will be visible
4. Either submit a pull request or upload the file to the [translation discussion](https://github.com/IgorMundstein/WinMemoryCleaner/discussions/14)

💡 If you are a .NET developer

1. You can add the new file to the Resources\Localization folder
2. Change the file build action property to Embedded Resource
3. Rebuild and run the WinMemoryCleaner project

❤️ Contributors

- `Arabic (اللغة العربية)` [Abdulmajeed-Alrajhi](https://github.com/Abdulmajeed-Alrajhi)
- `Chinese - Simplified (中文(简体))` [Kun Zhao](https://github.com/kzhdev) | [raydenake22](https://github.com/raydenake22)
- `Chinese - Traditional (中文(繁體))` [raydenake22](https://github.com/raydenake22) | [rtyrtyrtyqw](https://github.com/rtyrtyrtyqw)
- `Dutch (Nederlands)` [hax4dazy](https://github.com/hax4dazy)
- `French (Français)` [William VINCENT](https://github.com/wixaw)
- `German (Deutsch)` [Calvin](https://github.com/Slluxx)
- `Greek (Ελληνικά)` [tkatsageorgis](https://github.com/tkatsageorgis)
- `Indonesian (Bahasa Indonesia)` [Eskey](https://github.com/Eskeyz)
- `Italian (Italiano)` [wintrymichi](https://github.com/wintrymichi)
- `Japanese (日本語)` [dai](https://github.com/dai)
- `Korean (한국어)` [VenusGirl](https://github.com/VenusGirl)
- `Macedonian (Македонски)` [Dimitrij Gjorgji](https://github.com/Cathadox)
- `Polish (Polski)` [Fresta56](https://github.com/Fresta56)
- `Serbian (Srpski)` [DragorMilos](https://github.com/DragorMilos)
- `Slovenian (Slovenščina)` [Jadran Rudec](https://github.com/JadranR)
- `Spanish (Español)` [Ajneb Al Revés](https://github.com/AjnebAlReves) | [Nekrodamus](https://github.com/FrannDzs)
- `Turkish (Türkçe)` [Viollje](https://github.com/Viollje)
- `Ukrainian (Українська)` [Oleksander](https://github.com/Mariachi1231)
