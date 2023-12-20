<div align="center">
  <h1>Windows Memory Cleaner</h1>

  [![](https://img.shields.io/badge/Windows-XP%20%7C%20Vista%20%7C%207%20%7C%208%20%7C%2010%20%7C%2011-blue?style=for-the-badge)](#windows-memory-cleaner)
  [![](https://img.shields.io/badge/Server-2003%20%7C%202008%20%7C%202012%20%7C%202016%20%7C%202019%20%7C%202022-blue?style=for-the-badge)](#windows-memory-cleaner)

  [![](https://img.shields.io/github/license/IgorMundstein/WinMemoryCleaner?style=for-the-badge)](/LICENSE)
  [![](https://img.shields.io/github/downloads/IgorMundstein/WinMemoryCleaner/total?style=for-the-badge)](https://github.com/IgorMundstein/WinMemoryCleaner/releases/latest/)

  [![](/.github/images/main-window.png)](#windows-memory-cleaner)

  <p align="justify">
    This free RAM cleaner uses native Windows features to clear memory areas. Sometimes, programs do not release the allocated memory, making the computer slow. That is when you use Windows Memory Cleaner to optimize the memory so you can keep working without wasting time restarting your system. 
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

- `Combined Page List` - Flushes the blocks from the combined page list effectively only when page combining is enabled
- `Modified Page List` - Flushes memory from the modified page list, writing unsaved data to disk and moving the pages to the standby list
- `Processes Working Set` - Removes memory from all user-mode and system working sets and moves it to the standby or modified page lists. Note that by the time processes run, any code will necessarily populate their working sets to do so
- `Standby List` - Flushes pages from all standby lists to the free list
- `Standby List (Low Priority)` - Flushes pages from the lowest-priority standby list to the free list
- `System Working Set` - Removes memory from the system cache working set

### Multi-Language

- `Albanian` `Arabic` `Bulgarian` `Chinese` `Dutch` `English` `French` `German` `Greek` `Indonesian` `Irish` `Italian` `Japanese` `Korean` `Macedonian` `Persian` `Polish` `Portuguese` `Russian` `Serbian` `Slovenian` `Spanish` `Turkish` `Ukrainian`

### Processes excluded from optimization

- You can build a list of processes to ignore when memory is optimized

### Optimization hotkey (Global)

- `CTRL + ALT + M (Customizable)` - Optimize

### Settings

- `Always on top` - Pins the window to the top of all your windows
- `Auto update` - Keeps the app up to date
- `Close after optimization` - Closes the app after optimization
- `Close to the notification area` - Minimize the app to the system tray when clicking the close (X) button
- `Run on low priority` - It limits the app resource usage by reducing the process priority and ensuring it runs efficiently. It might increase the optimization time, but it helps if your Windows freezes during it
- `Run on startup` - Runs the app after the system boots up. It creates an entry on Windows **Task Scheduler** and Windows Registry path **SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run**
- `Show optimization notifications` - Sends a message to the notification area after optimization. It includes the approximate memory released
- `Show virtual memory` - It also monitors the virtual memory usage
- `Start minimized` - The app will start minimized to the system tray. Single-click on the icon to restore

### System tray (Notification area)

- Menu

[![](/.github/images/system-tray.png)](#system-tray)

- Notification

[![](/.github/images/notification.png)](#notifications)

### Tray icon

- `Image` - Show app icon
- `Memory usage` - Show physical memory usage with a background color based on the value
  - [![](/.github/images/memory-usage.png)](#tray-icon) `(0% - 79%)` Black
  - [![](/.github/images/memory-usage-warning.png)](#tray-icon) `(80% - 89%)` Orange
  - [![](/.github/images/memory-usage-danger.png)](#tray-icon) `(90% - 100%)` Red

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
- Right-to-left language support and bidirectional text
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

💡 When a new version requires translated text changes, Google Translate will be used for translation by the developer. The contributor's efforts to submit updates will always be appreciated.

❤️ Contributors

- `Albanian (Shqip)` [Omer Rustemi](https://github.com/omerrustemicode)
- `Arabic (اللغة العربية)` [Abdulmajeed-Alrajhi](https://github.com/Abdulmajeed-Alrajhi)
- `Bulgarian (български)` [Konstantin](https://github.com/constantinejc)
- `Chinese (Simplified) (中文(简体))` [KaiHuaDou](https://github.com/KaiHuaDou) | [Kun Zhao](https://github.com/kzhdev) | [raydenake22](https://github.com/raydenake22)
- `Chinese (Traditional) (中文(繁體))` [raydenake22](https://github.com/raydenake22) | [rtyrtyrtyqw](https://github.com/rtyrtyrtyqw)
- `Dutch (Nederlands)` [hax4dazy](https://github.com/hax4dazy)
- `French (Français)` [William VINCENT](https://github.com/wixaw)
- `German (Deutsch)` [Calvin](https://github.com/Slluxx)
- `Greek (Ελληνικά)` [tkatsageorgis](https://github.com/tkatsageorgis)
- `Indonesian (Indonesia)` [Eskey](https://github.com/Eskeyz)
- `Irish (Gaeilge)` [Happygolucky254](https://github.com/Happygolucky254)
- `Italian (Italiano)` [wintrymichi](https://github.com/wintrymichi)
- `Japanese (日本語)` [dai](https://github.com/dai)
- `Korean (한국어)` [VenusGirl](https://github.com/VenusGirl)
- `Macedonian (Македонски)` [Dimitrij Gjorgji](https://github.com/Cathadox)
- `Persian (فارسی)` [KavianK](https://github.com/KavianK)
- `Polish (Polski)` [Fresta56](https://github.com/Fresta56)
- `Russian (Русский)` [ruslooob](https://github.com/ruslooob)
- `Serbian (Srpski)` [DragorMilos](https://github.com/DragorMilos)
- `Slovenian (Slovenščina)` [Jadran Rudec](https://github.com/JadranR)
- `Spanish (Español)` [Ajneb Al Revés](https://github.com/AjnebAlReves) | [Nekrodamus](https://github.com/FrannDzs)
- `Turkish (Türkçe)` [Viollje](https://github.com/Viollje)
- `Ukrainian (Українська)` [RieBi](https://github.com/RieBi) | [Oleksander](https://github.com/Mariachi1231)

## ❔ Frequently Asked Questions (FAQ)

### Where does the app save the user configuration?

Each user setting is saved in the Windows registry path **Computer\HKEY_CURRENT_USER\Software\WinMemoryCleaner**

### Why has the app been flagged as a Virus/Trojan and blocked by Windows Defender or Antivirus?

One of the reasons for this **false alarm** is that the application adds entries to the registry and task scheduler to run the application at startup. Windows doesn't “like” applications with administrator privileges running at startup. I understand that, but this is the way to do it. I apologize, but the application cannot deep clean memory without administrator privileges.

That's a common issue that persists every time a new app version is released. I constantly submit the executable to Microsoft. Usually, it takes up to 72 hours for Microsoft to remove the detection.
It helps if more users [submit the app for malware analysis](https://www.microsoft.com/en-us/wdsi/filesubmission)

Meanwhile, as a workaround, you can [add an exclusion to Windows Security](https://support.microsoft.com/en-us/windows/add-an-exclusion-to-windows-security-811816c0-4dfd-af4a-47e4-c301afe13b26)
