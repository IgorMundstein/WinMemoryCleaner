using System.Collections.Generic;
using System.Linq;

namespace WinMemoryCleaner
{
    /// <summary>  
    /// Log Optimization Data
    /// </summary>  
    public class LogOptimizationData : ILogData, IJsonSerializable
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="LogOptimizationData" /> class.  
        /// </summary>  
        public LogOptimizationData()
        {
            MemoryAreas = new List<LogOptimizationDataMemoryArea>();
        }

        /// <summary>  
        /// Gets or sets the duration.  
        /// </summary>  
        /// <value>  
        /// The duration.  
        /// </value>  
        public string Duration { get; set; }

        /// <summary>  
        /// Gets or sets the memory.  
        /// </summary>  
        /// <value>  
        /// The memory.  
        /// </value>  
        public List<LogOptimizationDataMemoryArea> MemoryAreas { get; private set; }

        /// <summary>  
        /// Gets or sets the reason.  
        /// </summary>  
        /// <value>  
        /// The reason.  
        /// </value>  
        public string Reason { get; set; }

        /// <summary>
        /// Converts the log optimization data to a JSON-serializable object.
        /// </summary>
        /// <returns>An anonymous object ready for JSON serialization.</returns>
        public object ToJson()
        {
            var memoryAreas = MemoryAreas
                .OrderBy(m => m.Name)
                .Select(m => string.IsNullOrEmpty(m.Error)
                    ? new { name = m.Name, duration = m.Duration }
                    : (object)new { name = m.Name, duration = m.Duration, error = m.Error })
                .ToList();

            return new
            {
                reason = Reason,
                duration = Duration,
                memoryAreas
            };
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(Localizer.Culture, "{0} ({1}) - {2} area(s)", Reason, Duration, MemoryAreas != null ? MemoryAreas.Count : 0);
        }
    }
}
