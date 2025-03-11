using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RaceManagementApp.UI.Utility.Logger
{
    public interface ILoggerService<T>
    {
        void LogError(string message, Exception ex,
                      [CallerMemberName] string caller = "",
                      [CallerFilePath] string file = "",
                      [CallerLineNumber] int line = 0);

        void LogInfo(string message,
                     [CallerMemberName] string caller = "",
                     [CallerFilePath] string file = "",
                     [CallerLineNumber] int line = 0);

        void LogDebug(string message,
                      [CallerMemberName] string caller = "",
                      [CallerFilePath] string file = "",
                      [CallerLineNumber] int line = 0);

        void LogWarning(string message,
                        [CallerMemberName] string caller = "",
                        [CallerFilePath] string file = "",
                        [CallerLineNumber] int line = 0);

        void LogTrace(string message,
                      [CallerMemberName] string caller = "",
                      [CallerFilePath] string file = "",
                      [CallerLineNumber] int line = 0);

        void LogCritical(string message, Exception ex,
                         [CallerMemberName] string caller = "",
                         [CallerFilePath] string file = "",
                         [CallerLineNumber] int line = 0);
    }
}
