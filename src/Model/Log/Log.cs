using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Log Item
    /// </summary>
    public class Log : IJsonSerializable
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
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public ILogData Data { get; private set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public Enums.Log.Levels Level { get; private set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method { get; private set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// Converts the log to a JSON-serializable object.
        /// </summary>
        /// <returns>An anonymous object ready for JSON serialization.</returns>
        public object ToJson()
        {
            var jsonSerializable = Data as IJsonSerializable;
            var data = jsonSerializable != null ? jsonSerializable.ToJson() : null;

            var level = Level.ToString();
            var timestamp = Timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", CultureInfo.InvariantCulture);

            return data == null
                ? new { timestamp, level, method = Method, message = Message, data = (object)null }
                : (object)new { timestamp, level, method = Method, message = Message, data };
        }

        /// <summary>
        /// Returns a human-readable string representation for debugging.
        /// </summary>
        /// <returns>A formatted string showing timestamp, level, and message.</returns>
        public override string ToString()
        {
            return string.Format(Localizer.Culture, "[{0}] {1}: {2}", Timestamp.ToString("HH:mm:ss", Localizer.Culture), Level, Message);
        }
    }
}
