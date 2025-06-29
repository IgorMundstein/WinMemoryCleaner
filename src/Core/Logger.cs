using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Logger
    /// </summary>
    public static class Logger
    {
        #region Fields

        private static Enums.Log.Levels _level = Enums.Log.Levels.Debug | Enums.Log.Levels.Information | Enums.Log.Levels.Warning | Enums.Log.Levels.Error;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes the <see cref="Logger"/> class.
        /// </summary>
        static Logger()
        {
            try
            {
                Trace.AutoFlush = true;

                Trace.Listeners.Clear();
                Trace.Listeners.AddRange(new TraceListener[]
                {
                    new ConsoleTraceListener(false) { Name = Constants.App.Title },
                    new EventLogTraceListener(Constants.App.Title)
                });
            }
            catch
            {
                try
                {
                    if (Trace.Listeners.Count == 0)
                        Trace.Listeners.Add(new DefaultTraceListener() { Name = Constants.App.Title });
                }
                catch (Exception e)
                {
                    Event(e.GetMessage(), EventLogEntryType.Error);
                }
            }
        }

        #endregion

        #region Properties

        private static bool IsDebugEnabled { get { return (_level & Enums.Log.Levels.Debug) != 0; } }

        private static bool IsErrorEnabled { get { return (_level & Enums.Log.Levels.Error) != 0; } }

        private static bool IsInformationEnabled { get { return (_level & Enums.Log.Levels.Information) != 0; } }

        private static bool IsWarningEnabled { get { return (_level & Enums.Log.Levels.Warning) != 0; } }

        /// <summary>
        /// Sets the log level.
        /// </summary>
        /// <value>
        /// The log level.
        /// </value>
        public static Enums.Log.Levels Level
        {
            get { return _level; }
            set
            {
                switch (value)
                {
                    case Enums.Log.Levels.Debug:
                        _level = Enums.Log.Levels.Debug | Enums.Log.Levels.Information | Enums.Log.Levels.Warning | Enums.Log.Levels.Error;
                        break;

                    case Enums.Log.Levels.Information:
                        _level = Enums.Log.Levels.Information | Enums.Log.Levels.Warning | Enums.Log.Levels.Error;
                        break;

                    case Enums.Log.Levels.Warning:
                        _level = Enums.Log.Levels.Warning | Enums.Log.Levels.Error;
                        break;

                    case Enums.Log.Levels.Error:
                        _level = Enums.Log.Levels.Error;
                        break;
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="message">Custom message about the Exception</param>
        /// <param name="method">Method</param>
        public static void Debug(Exception exception, string message = null, [CallerMemberName] string method = null)
        {
            Exception(Enums.Log.Levels.Debug, exception, message, method);
        }

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="method">Method</param>
        public static void Debug(string message, [CallerMemberName] string method = null)
        {
            Log(new Log(Enums.Log.Levels.Debug, message, null, method));
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="message">Custom message about the Exception</param>
        /// <param name="method">Method</param>
        public static void Error(Exception exception, string message = null, [CallerMemberName] string method = null)
        {
            Exception(Enums.Log.Levels.Error, exception, message, method);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="method">Method</param>
        public static void Error(string message, [CallerMemberName] string method = null)
        {
            Log(new Log(Enums.Log.Levels.Error, message, null, method));
        }

        /// <summary>
        /// Windows Event
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="type">Type</param>
        private static void Event(string message, EventLogEntryType type = EventLogEntryType.Information)
        {
            try
            {
                EventLog.WriteEntry(Constants.App.Title, message, type);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Exception
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        private static void Exception(Enums.Log.Levels level, Exception exception, string message = null, [CallerMemberName] string method = null)
        {
            if (IsDebugEnabled)
            {
                try
                {
                    var stackTrace = new StackTrace(exception, true);
                    var frame = stackTrace.GetFrame(stackTrace.FrameCount - 1);
                    var methodBase = frame.GetMethod();

                    if (methodBase.DeclaringType != null)
                        method = string.Format(Localizer.Culture, "{0}.{1}", methodBase.DeclaringType.Name, methodBase.Name);
                }
                catch
                {
                    // ignored
                }
            }

            if (string.IsNullOrWhiteSpace(message) && exception != null)
                message = exception.GetMessage();

            Log(new Log(level, message, null, method));
        }

        /// <summary>
        /// Information
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="method">Method</param>
        public static void Information(string message, [CallerMemberName] string method = null)
        {
            Log(new Log(Enums.Log.Levels.Information, message, null, method));
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="level">Levels</param>
        /// <param name="message">Message</param>
        private static void Log(Enums.Log.Levels level, string message)
        {
            try
            {
                switch (level)
                {
                    case Enums.Log.Levels.Debug:
                        if (IsDebugEnabled)
                            Trace.WriteLine(message);
                        break;

                    case Enums.Log.Levels.Information:
                        if (IsInformationEnabled)
                            Trace.TraceInformation(message);
                        break;

                    case Enums.Log.Levels.Warning:
                        if (IsWarningEnabled)
                            Trace.TraceWarning(message);
                        break;

                    case Enums.Log.Levels.Error:
                        if (IsErrorEnabled)
                            Trace.TraceError(message);
                        break;
                }
            }
            catch (Exception e)
            {
                Event(string.Format(Localizer.Culture, Localizer.String.ErrorCanNotSaveLog, message, e.GetMessage()), EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="log">The log.</param>
        public static void Log(Log log)
        {
            try
            {
                if (log == null)
                    throw new ArgumentNullException("log");

                Log(log.Level, log.ToString());
            }
            catch (Exception e)
            {
                Event(string.Format(Localizer.Culture, Localizer.String.ErrorCanNotSaveLog, log != null ? log.Message : "NULL", e.GetMessage()), EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="method">Method</param>
        public static void Warning(string message, [CallerMemberName] string method = null)
        {
            Log(new Log(Enums.Log.Levels.Warning, message, null, method));
        }
    }

    #endregion Methods
}
