using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinMemoryCleaner
{
    /// <summary>  
    /// Log Optimization Data
    /// </summary>  
    public class LogOptimizationData : ILogData
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="LogOptimizationData"/> class.  
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
        /// Converts to string.  
        /// </summary>  
        /// <returns>  
        /// A <see cref="string" /> that represents this instance.  
        /// </returns>  
        public override string ToString()
        {
            var sb = new StringBuilder();
            var space = "  ";

            sb.Append('{').Append(Environment.NewLine);
            sb.Append(space).Append(space).Append("\"reason\": \"").Append(Reason).Append("\",").Append(Environment.NewLine);
            sb.Append(space).Append(space).Append("\"duration\": \"").Append(Duration).Append("\",").Append(Environment.NewLine);
            sb.Append(space).Append(space).Append("\"memoryAreas\": [").Append(Environment.NewLine);

            foreach (var memoryArea in MemoryAreas.OrderBy(memoryArea => memoryArea.Name))
            {
                sb.Append(space).Append(space).Append(space).Append("{ ");
                sb.Append("\"name\": \"").Append(memoryArea.Name);
                sb.Append("\", \"duration\": \"").Append(memoryArea.Duration);

                if (memoryArea.Error != null)
                    sb.Append("\", \"error\": \"").Append(memoryArea.Error);

                sb.Append("\" }").Append(',').Append(Environment.NewLine);
            }

            if (MemoryAreas.Count > 0)
                sb.Length -= (1 + Environment.NewLine.Length);

            sb.Append(Environment.NewLine);
            sb.Append(space).Append(space).Append(']').Append(Environment.NewLine);
            sb.Append(space).Append('}');

            return sb.ToString();
        }
    }
}
