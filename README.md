# Windows Memory Cleaner
[![](https://img.shields.io/badge/Windows-XP%20%7C%20Vista%20%7C%207%20%7C%208%20%7C%2010%20%7C%2011-blue?style=for-the-badge)](#)
[![](https://img.shields.io/badge/Windows%20Server-2003%20%7C%202008%20%7C%202012%20%7C%202016%20%7C%202019%20%7C%202022-blue?style=for-the-badge)](#)
[![](https://img.shields.io/github/license/IgorMundstein/WinMemoryCleaner?style=for-the-badge)](#) 
[![](https://img.shields.io/github/downloads/IgorMundstein/WinMemoryCleaner/total?style=for-the-badge)](#) 

The app is a free RAM cleaner. There are times when programs do not release the memory they used, making the machine slow, but you don’t want to restart the system to get the used memory back. That is where you use Windows Memory Cleaner to clean your memory, so you can carry on working without wasting time restarting your system. 

[![Download)](https://img.shields.io/github/v/release/IgorMundstein/WinMemoryCleaner?color=red&label=DOWNLOAD&logo=windows)](https://github.com/IgorMundstein/WinMemoryCleaner/releases/download/1.1/WinMemoryCleaner.zip)

## 🚀 How it works
![](https://raw.githubusercontent.com/IgorMundstein/WinMemoryCleaner/master/docs/main-window.png)

It's portable, so you do not have to bother with installation or configuration. Download and open the executable to get started. The app requires **administrator** privileges to run and comes with a minimalistic interface; before you clean up memory, you should go through the list and check the areas you want the app to analyze.

It gives you the ability to clean up the memory in 6 different ways:

- `Clean Combined Page List` - Flushes blocks from the combined page list.
- `Clean Modified Page List` - Flushes memory from the Modified page list, writing unsaved data to disk and moving the pages to the Standby list.
- `Clean Processes Working Set` - Removes memory from all user-mode and system working sets and moves it to the Standby or Modified page lists. Note that by the time, processes that run any code will necessarily populate their working sets to do so.
- `Clean Standby List`* - Discards pages from all Standby lists, and moves them to the Free list.
- `Clean Standby List (Low Priority)` - Flushes pages from the lowest-priority Standby list to the Free list.
- `Clean System Working Set` - Removes memory from the system cache working set.

## 🖥️ Command arguments (NO GUI)
The arguments below can be used to run the program silently.

`/CombinedPageList` `/ModifiedPageList` `/ProcessesWorkingSet` `/StandbyList` `/StandbyListLowPriority` `/SystemWorkingSet`

![](https://raw.githubusercontent.com/IgorMundstein/WinMemoryCleaner/master/docs/shortcut-command-arguments.png)

## 📖 Logs
Logs are saved on windows event.

![](https://raw.githubusercontent.com/IgorMundstein/WinMemoryCleaner/master/docs/windows-event-log.png)

## ❤️ Donate
If you have found the app helpful and want to support me.

<a href="https://www.buymeacoffee.com/mundstein" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" alt="Buy Me A Coffee" style="height: auto !important; width: 250px !important;" ></a>
