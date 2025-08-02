using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Log Item
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Log" /> class.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="method">The method.</param>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        /// <param name="timestamp">The timestamp.</param>
        public Log(Enums.Log.Levels level, string message, ILogData data = null, [CallerMemberName] string method = null, DateTimeOffset? timestamp = null)
        {
            Data = data;
            Level = level;
            Method = method;
            Message = message;
            Timestamp = timestamp ?? DateTimeOffset.Now;
        }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public Enums.Log.Levels Level { get; private set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method { get; private set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public ILogData Data { get; private set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTimeOffset Timestamp { get; private set; }

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
            sb.Append(space).Append("\"timestamp\": \"").Append(Timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffK", Localizer.Culture)).Append("\",").Append(Environment.NewLine);
            sb.Append(space).Append("\"level\": \"").Append(Level.ToString()).Append("\",").Append(Environment.NewLine);
            sb.Append(space).Append("\"method\": \"").Append(Method).Append("\",").Append(Environment.NewLine);
            sb.Append(space).Append("\"message\": \"").Append(Message);

            if (Data != null)
                sb.Append("\",").Append(Environment.NewLine).Append(space).Append("\"data\": ").Append(Data.ToString()).Append(Environment.NewLine);
            else
                sb.Append('\"').Append(Environment.NewLine);

            sb.Append('}');

            return sb.ToString();
        }
    }
}
