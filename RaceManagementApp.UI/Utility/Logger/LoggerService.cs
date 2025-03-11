using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RaceManagementApp.UI.Utility.Logger
{
    public class LoggerService<T> : ILoggerService<T>
    {
        private static readonly ILogger _logger = LogManager.GetLogger(typeof(T).FullName);
        public LoggerService()
        {
            LogManager.LoadConfiguration("nlog.config");
        }
        public void LogError(string message, Exception ex,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
        {
            _logger.Error(ex, $"{message} | Caller: {caller} | File: {file} | Line: {line}");
        }

        public void LogInfo(string message,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
        {
            _logger.Info($"{message} | Caller: {caller} | File: {file} | Line: {line}");
        }

        public void LogDebug(string message,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
        {
            _logger.Debug($"{message} | Caller: {caller} | File: {file} | Line: {line}");
        }

        public void LogWarning(string message,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
        {
            _logger.Warn($"{message} | Caller: {caller} | File: {file} | Line: {line}");
        }

        public void LogTrace(string message,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
        {
            _logger.Trace($"{message} | Caller: {caller} | File: {file} | Line: {line}");
        }

        public void LogCritical(string message, Exception ex,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
        {
            _logger.Fatal(ex, $"{message} | Caller: {caller} | File: {file} | Line: {line}");
        }

    }
}
