using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace WinMemoryCleaner
{
    internal interface ILogger
    {
        Enums.Log.Level Level { set; }

        ReadOnlyObservableCollection<Log> Logs { get; }

        void Debug(string message, [CallerMemberName] string method = null);

        void Error(Exception exception, string message = null, [CallerMemberName] string method = null);

        void Error(string message, [CallerMemberName] string method = null);

        void Flush();

        void Information(string message, [CallerMemberName] string method = null);

        void Warning(string message, [CallerMemberName] string method = null);
    }
}
