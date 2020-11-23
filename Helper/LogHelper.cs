using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using WinMemoryCleaner.Properties;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Log Helper
    /// </summary>
    internal static class LogHelper
    {
        #region Fields

        private static Enums.Log.Level _level = Enums.Log.Level.Debug | Enums.Log.Level.Info | Enums.Log.Level.Warning | Enums.Log.Level.Error;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Sets the log level.
        /// </summary>
        /// <value>
        /// The log level.
        /// </value>
        internal static Enums.Log.Level Level
        {
            set
            {
                switch (value)
                {
                    case Enums.Log.Level.Debug:
                        _level = Enums.Log.Level.Debug | Enums.Log.Level.Info | Enums.Log.Level.Warning | Enums.Log.Level.Error;
                        break;

                    case Enums.Log.Level.Info:
                        _level = Enums.Log.Level.Info | Enums.Log.Level.Warning | Enums.Log.Level.Error;
                        break;

                    case Enums.Log.Level.Warning:
                        _level = Enums.Log.Level.Warning | Enums.Log.Level.Error;
                        break;

                    case Enums.Log.Level.Error:
                        _level = Enums.Log.Level.Error;
                        break;
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="method">Method</param>
        internal static void Debug(string message, [CallerMemberName]string method = null)
        {
            Log(Enums.Log.Level.Debug, message, method);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="method">Method</param>
        internal static void Error(string message, [CallerMemberName]string method = null)
        {
            Log(Enums.Log.Level.Error, message, method);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="message">Custom message about the Exception</param>
        /// <param name="method">Method</param>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Log method")]
        internal static void Error(Exception exception, string message = null, [CallerMemberName]string method = null)
        {
            if (exception != null)
            {
                if (exception.InnerException != null)
                    Error(exception.InnerException, message, method);

                if (string.IsNullOrWhiteSpace(message))
                    message = exception.Message;

                if ((_level & Enums.Log.Level.Debug) != 0)
                {
                    try
                    {
                        StackTrace st = new StackTrace(exception, true);
                        StackFrame frame = st.GetFrame(st.FrameCount - 1);
                        MethodBase methodBase = frame.GetMethod();

                        if (methodBase.DeclaringType != null)
                            method = string.Format(CultureInfo.CurrentCulture, "{0} > LN: {1}", methodBase.DeclaringType.Name + "." + methodBase.Name, frame.GetFileLineNumber());
                    }
                    catch
                    {
                        // ignored
                    }
                }

                Log(Enums.Log.Level.Error, message, method);
            }
        }

        /// <summary>
        /// Windows Event
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="type">Type</param>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Log method")]
        private static void Event(string message, EventLogEntryType type = EventLogEntryType.Information)
        {
            try
            {
                EventLog.WriteEntry("Windows Memory Cleaner", message, type);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="method">Method</param>
        internal static void Info(string message, [CallerMemberName]string method = null)
        {
            Log(Enums.Log.Level.Info, message, method);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="level">Level</param>
        /// <param name="message">Message</param>
        /// <param name="method">Method</param>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Log method")]
        private static void Log(Enums.Log.Level level, string message, [CallerMemberName]string method = null)
        {
            try
            {
                message = string.Format(CultureInfo.CurrentCulture, "{0}\t{1}\t{2}",
                                        DateTime.Now.ToString(Constants.Log.DatetimeFormat, CultureInfo.CurrentCulture),
                                        level.ToString().ToUpper(CultureInfo.CurrentCulture),
                                        string.IsNullOrWhiteSpace(method) ? message : string.Format(CultureInfo.CurrentCulture, "[{0}] {1}", method, message));

                switch (level)
                {
                    case Enums.Log.Level.Debug:
                        if ((_level & Enums.Log.Level.Debug) != 0)
                            Trace.WriteLine(message);
                        break;

                    case Enums.Log.Level.Info:
                        if ((_level & Enums.Log.Level.Info) != 0)
                            Trace.TraceInformation(message);
                        break;

                    case Enums.Log.Level.Warning:
                        if ((_level & Enums.Log.Level.Warning) != 0)
                            Trace.TraceWarning(message);
                        break;

                    case Enums.Log.Level.Error:
                        if ((_level & Enums.Log.Level.Error) != 0)
                            Trace.TraceError(message);
                        break;
                }
            }
            catch (Exception e)
            {
                Event(string.Format(CultureInfo.CurrentCulture, Resources.LogHelperCanNotSaveLogException, message, e.Message), EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="method">Method</param>
        internal static void Warning(string message, [CallerMemberName]string method = null)
        {
            Log(Enums.Log.Level.Warning, message, method);
        }
    }

    #endregion Methods
}