using System.Globalization;
using System.Windows.Input;

namespace WinMemoryCleaner.Test
{
    /// <summary>
    /// Mocker - Helper class for creating test data and mock objects
    /// </summary>
    public static class Mocker
    {
        #region Memory Mock Data

        /// <summary>
        /// Creates a valid MemoryStatusEx struct for testing
        /// </summary>
        [System.CLSCompliant(false)]
        public static Structs.Windows.MemoryStatusEx CreateMemoryStatusEx
        (
            ulong totalPhysical = 8589934592, // 8 GB
            ulong availPhysical = 4294967296, // 4 GB
            ulong totalPageFile = 17179869184, // 16 GB
            ulong availPageFile = 12884901888, // 12 GB
            byte memoryLoad = 50
        )
        {
            return new Structs.Windows.MemoryStatusEx
            {
                MemoryLoad = memoryLoad,
                TotalPhys = (long)totalPhysical,
                AvailPhys = (long)availPhysical,
                TotalPageFile = (long)totalPageFile,
                AvailPageFile = (long)availPageFile,
                TotalVirtual = long.MaxValue,
                AvailVirtual = long.MaxValue,
                AvailExtendedVirtual = 0
            };
        }

        /// <summary>
        /// Creates a Memory object with high memory usage for testing
        /// </summary>
        /// <returns>Memory object with 80% usage</returns>
        public static Memory CreateHighMemoryUsage()
        {
            return new Memory(CreateMemoryStatusEx
            (
                totalPhysical: 8589934592,  // 8 GB
                availPhysical: 1717986918,  // ~1.6 GB (80% used)
                memoryLoad: 80
            ));
        }

        /// <summary>
        /// Creates a Memory object with low memory usage for testing
        /// </summary>
        /// <returns>Memory object with 20% usage</returns>
        public static Memory CreateLowMemoryUsage()
        {
            return new Memory(CreateMemoryStatusEx
            (
                totalPhysical: 8589934592,  // 8 GB
                availPhysical: 6871947674,  // ~6.4 GB (20% used)
                memoryLoad: 20
            ));
        }

        /// <summary>
        /// Creates a Memory object with critical memory usage for testing
        /// </summary>
        /// <returns>Memory object with 95% usage</returns>
        public static Memory CreateCriticalMemoryUsage()
        {
            return new Memory(CreateMemoryStatusEx
            (
                totalPhysical: 8589934592,  // 8 GB
                availPhysical: 429496730,   // ~400 MB (95% used)
                memoryLoad: 95
            ));
        }

        #endregion

        #region Operating System Mock Data

        /// <summary>
        /// Creates an OperatingSystem object for Windows 10 testing
        /// </summary>
        /// <returns>Windows 10 OperatingSystem object</returns>
        public static OperatingSystem CreateWindows10OS()
        {
            return new OperatingSystem
            {
                Is64Bit = true,
                IsWindows7OrGreater = true,
                IsWindows8OrGreater = true,
                IsWindows81OrGreater = true,
                IsWindowsVistaOrGreater = true,
                IsWindowsXpOrGreater = true
            };
        }

        /// <summary>
        /// Creates an OperatingSystem object for Windows 7 testing
        /// </summary>
        /// <returns>Windows 7 OperatingSystem object</returns>
        public static OperatingSystem CreateWindows7OS()
        {
            return new OperatingSystem
            {
                Is64Bit = true,
                IsWindows7OrGreater = true,
                IsWindows8OrGreater = false,
                IsWindows81OrGreater = false,
                IsWindowsVistaOrGreater = true,
                IsWindowsXpOrGreater = true
            };
        }

        /// <summary>
        /// Creates an OperatingSystem object for Windows Vista testing
        /// </summary>
        /// <returns>Windows Vista OperatingSystem object</returns>
        public static OperatingSystem CreateWindowsVistaOS()
        {
            return new OperatingSystem
            {
                Is64Bit = false,
                IsWindows7OrGreater = false,
                IsWindows8OrGreater = false,
                IsWindows81OrGreater = false,
                IsWindowsVistaOrGreater = true,
                IsWindowsXpOrGreater = true
            };
        }

        #endregion

        #region Computer Mock Data

        /// <summary>
        /// Creates a Computer object with typical configuration
        /// </summary>
        /// <returns>Computer object with typical memory and OS</returns>
        public static Computer CreateTypicalComputer()
        {
            return new Computer
            {
                Memory = new Memory(CreateMemoryStatusEx()),
                OperatingSystem = CreateWindows10OS()
            };
        }

        /// <summary>
        /// Creates a Computer object with high memory usage
        /// </summary>
        /// <returns>Computer object with high memory usage</returns>
        public static Computer CreateHighMemoryComputer()
        {
            return new Computer
            {
                Memory = CreateHighMemoryUsage(),
                OperatingSystem = CreateWindows10OS()
            };
        }

        #endregion

        #region JSON Mock Data

        /// <summary>
        /// Creates a test JSON string
        /// </summary>
        public static string CreateJsonString(string key, string value)
        {
            return string.Format(CultureInfo.InvariantCulture, "{{\"" + key + "\":\"" + value + "\"}}");
        }

        /// <summary>
        /// Creates a minified JSON object string
        /// </summary>
        public static string CreateMinifiedJson()
        {
            return "{\"name\":\"test\",\"value\":123,\"nested\":{\"key\":\"value\"}}";
        }

        /// <summary>
        /// Creates a formatted JSON object string
        /// </summary>
        public static string CreateFormattedJson()
        {
            return "{\r\n  \"name\": \"test\",\r\n  \"value\": 123,\r\n  \"nested\": {\r\n    \"key\": \"value\"\r\n  }\r\n}";
        }

        #endregion

        #region Log Mock Data

        /// <summary>
        /// Creates a test Log object
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="message">Log message</param>
        /// <returns>Log object</returns>
        public static Log CreateLog(Enums.Log.Levels level, string message)
        {
            return new Log(level, message);
        }

        /// <summary>
        /// Creates a test LogOptimizationData object
        /// </summary>
        /// <param name="reason">Optimization reason</param>
        /// <returns>LogOptimizationData object</returns>
        public static LogOptimizationData CreateLogOptimizationData(string reason = "Test")
        {
            var data = new LogOptimizationData
            {
                Reason = reason,
                Duration = "1.5 seconds"
            };

            data.MemoryAreas.Add(new LogOptimizationDataMemoryArea
            {
                Name = "Modified Page List",
                Duration = "0.5 seconds",
                Error = "Access Denied"
            });

            data.MemoryAreas.Add(new LogOptimizationDataMemoryArea
            {
                Name = "Standby List",
                Duration = "1.0 seconds"
            });

            data.MemoryAreas.Add(new LogOptimizationDataMemoryArea
            {
                Name = "Working Set",
                Duration = "0.5 seconds"
            });

            return data;
        }

        #endregion

        #region Hotkey Mock Data

        /// <summary>
        /// Creates a test Hotkey object
        /// </summary>
        /// <param name="modifiers">Modifier keys</param>
        /// <param name="key">Key</param>
        /// <returns>Hotkey object</returns>
        public static Hotkey CreateHotkey
        (
            ModifierKeys modifiers = ModifierKeys.Control | ModifierKeys.Alt,
            Key key = Key.M)
        {
            return new Hotkey(modifiers, key);
        }

        #endregion
    }
}
