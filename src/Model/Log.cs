using System;
using System.Drawing;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Log Item
    /// </summary>
    public class Log : Model
    {
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color
        {
            get
            {
                switch (Level)
                {
                    case Enums.Log.Level.Info:
                        return Color.Green;

                    case Enums.Log.Level.Warning:
                        return Color.Orange;

                    case Enums.Log.Level.Error:
                        return Color.Red;

                    default:
                        return Color.Black;
                }
            }
        }

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public Enums.Log.Level Level { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0}] {1}", Level, Message);
        }
    }
}
