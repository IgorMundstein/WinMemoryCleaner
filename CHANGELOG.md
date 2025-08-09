### 3.0.0

**2025-08-02**

- Added a help `?` menu
- Added a security check to verify the code certificate and warn if the user downloaded from an untrusted source. It's not bulletproof because the project is open source, but it makes it harder for people with bad intentions
- Added a setting to reduce or increase the font size. It's helpful for different screen sizes and resolutions
- Added an option to turn off the optimization hotkey ([#94](https://github.com/IgorMundstein/WinMemoryCleaner/issues/94))
- Added app to package managers: Chocolatey, Scoop, and Winget ([#89](https://github.com/IgorMundstein/WinMemoryCleaner/issues/89))
- Added code digital signature provided by SignPath.io to ensure authenticity and user safety
- Added donation options to the new help `?` menu and on GitHub
- Added GitHub workflows to enhance release delivery and trustworthiness on malicious scanner websites
- Added localization for Hebrew, Hungarian, Norwegian, and Thai languages
- Added optimization reason (Low Memory, Manual, or Schedule) on notifications and logs ([#110](https://github.com/IgorMundstein/WinMemoryCleaner/issues/110))
- Added reset to default settings feature to the new help `?` menu
- Added support to run as a Windows Service ([#96](https://github.com/IgorMundstein/WinMemoryCleaner/issues/96))
- Added two new memory areas: modified file cache and registry cache
- Enhanced `run on startup` feature ([#91](https://github.com/IgorMundstein/WinMemoryCleaner/issues/91)) ([#108](https://github.com/IgorMundstein/WinMemoryCleaner/issues/108))
- Enhanced text formats for better translations
- Enhanced tray icon customizations for memory usage ([#111](https://github.com/IgorMundstein/WinMemoryCleaner/issues/111)) ([#112](https://github.com/IgorMundstein/WinMemoryCleaner/issues/112))
- Improved code, documentation and user interface ([#92](https://github.com/IgorMundstein/WinMemoryCleaner/issues/92)) ([#103](https://github.com/IgorMundstein/WinMemoryCleaner/issues/103))
- Modified window event log messages to JSON format
- Moved the About this project link to the new help menu
- Moved Windows registry path from the current user to the local machine
- Renamed memory area system working set to system file cache
- Renamed memory processes working set to working set

**This project is celebrating its 6th year. So, nothing better than a major update release. Here are some critical notes.**

- It's the first release implementing the CI/CD pipelines to publish the app to Chocolatey, Scoop, and Winget. So, there may be a delay in being available on these package managers.
- If you find this app helpful, please consider donating. Your donation helps keep the project alive, optimized, and free for everyone.
- If you run the app with command-line arguments (no GUI), check the modified memory area parameter names.
- Many contributors often provide translation updates, as some texts might not be in the best format due to frequent translation revisions. After we introduce the CI/CD workflows, we expect to publish releases more often to update minor corrections quickly. We now use the 'major.minor.patch' format for the app version, which will allow us to launch patch releases for localizations.
- We prioritize transparency and user safety. Since version 3.0.0, we have been digitally signing our files through [SignPath.io](https://about.signpath.io/product/open-source) using a free certificate provided under the [SignPath Terms of Use](https://signpath.org/terms). The project received the certificate in recognition of its popularity and public value in the open-source community. This process ensures that we distribute authentic files that have not been tampered with. By doing this, we will build trust with Microsoft Defender SmartScreen over time, and maybe someday we will obliterate that "Windows protected your PC" warning.

### 2.8.0

**2023-12-24**

- Added optimization progress bar to the optimize button
- Improved auto-update task
- Improved code & UI
- Improved memory usage tray icon

### 2.7.0

**2023-12-22**

- Changed the error dialog to a warning log event when a firewall blocks the app
- Improved Chinese (Simplified/Traditional), French, Korean, and Serbian languages
- Improved code
- Improved memory usage tray icon

### 2.6.0

**2023-12-17**

- Added Albanian, Bulgarian, Irish, Persian, and Russian languages
- Added approximate memory released to optimization notification
- Added right-to-left (RTL) language and UI support
- Added run on low priority setting. If enabled, it limits the app resource usage by reducing the process priority and ensuring it runs efficiently. It might increase the optimization time, but it helps if your Windows freezes during it
- Added show virtual memory setting and memory usage view
- Added support for cultures' native formats, like decimal separators
- Added tray icon customization. Users can choose between the default app image or show physical memory usage with a background color based on the value. (0% - 79%) White | (80% - 89%) Orange | (90% - 100%) Red
- Improved code, documentation, and UI
- Improved Greek language
- Improved UI rendering when the start minimized setting is enabled

### 2.5.0

**2023-08-20**

- Added optimization runtime stats to the log
- Added **compact mode** view. Click the arrow at the top right of the screen to collapse or expand the window
- Changed app priority to low. Optimization may run a little slower, but it will reduce the chance of Windows freezing during the optimization
- Improved code and UI
- Improved Greek and Polish languages

### 2.4.0

**2023-08-07**

- Added Polish and Ukrainian languages
- Improved code and documentation

### 2.3.0

**2023-08-04**

- Added Korean and Serbian languages
- Improved Code & UI
- Improved Slovenian language
- Signed all executable versions using a personal self-code signing certificate. It reset the downloads counter

### 2.2.0

**2023-08-02**

- Added optimization hotkey setting
- Added Arabic, Indonesian, and Japanese languages
- Fixed bugs
- Improved Code & UI
- Improved German language

### 2.1.0

**2023-07-27**

- Added close after optimization setting
- Added support for the Chinese (Simplified), Chinese (Traditional), Dutch, French, German, Greek, Italian, Macedonian, Slovenian, Spanish, and Turkish languages
- Added the ability to read language JSON files at the exact executable location. That will help contributors to test the translation before submitting it
- Added global hotkey (CTRL + ALT + M) to optimize
- Code improvements and bug fixes
- Modified notify icon title to show the memory usage
- Modified the default window focus to the Optimize button. That will allow the user to press ENTER to run the optimization after the app starts

### 2.0.0

**2023-03-26**

-  Always on top
-  Auto clean (Interval & Usage)
-  Auto app update
-  Code cleaning & optimizations
-  Localization (English/Portuguese)
-  Minimize the app to the system tray when closed
-  New UI (Darker)
-  Processes the exclusion list
-  Run on startup
-  Show optimization notifications
-  Start minimized
-  System tray icon (Notifications/Optimize/Exit)
-  Windows Server 2003 and Windows XP 64-bit support

### 1.1.0

**2021-09-06**

* Initial release deprecated. Files updated to 2.0 because it has the auto-update feature
