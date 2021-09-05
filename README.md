# Windows Memory Cleaner
This is a RAM cleaner. There are times when programs do not release the memory they used, making the machine slow, but you don’t want to restart the system to get the used memory back. This is where you use Windows Memory Cleaner to clean your memory, so you can carry on working without wasting time restarting your Windows.

[Download](https://github.com/IgorMundstein/WinMemoryCleaner/releases/download/1.1/WinMemoryCleaner.exe)

# How it works
![alt text](https://github.com/IgorMundstein/WinMemoryCleaner/blob/master/screenshot.png?raw=true)

It gives you the ability to clean up the memory in 6 different ways:

- **Clean Combined Page List** - Flushes blocks from the combined page list to the combine Free list.
- **Clean Modified Page List** - Flushes memory from the Modified page list, writing unsaved data to disk and moving the pages to the Standby list.
- **Clean Processes Working Set** - Removes memory from all user-mode and system working sets and moves it to the Standby or Modified page lists. Note that by the time, processes that run any code will necessarily populate their working sets to do so.
- **Clean Standby List** - Discards pages from all Standby lists, and moves them to the Free list.
- **Clean Standby List (Low Priority)** - Flushes pages from the lowest-priority Standby list to the Free list.
- **Clean System Working Set** - Removes memory from the system cache working set.
