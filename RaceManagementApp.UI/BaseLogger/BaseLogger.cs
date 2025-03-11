using Microsoft.Extensions.DependencyInjection;
using RaceManagementApp.UI;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.Utility.Logger;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public class BaseLogger
{
    private readonly ILoggerService<BaseLogger> _logger;

    public event PropertyChangedEventHandler PropertyChanged;
    private readonly UserActionLogService _userActionLogService;
    private readonly SystemActionLogService _systemActionLogService;

    public BaseLogger(ILoggerService<BaseLogger> logger)
    {
        _logger = logger;
        _logger?.LogInfo($"{GetType().Name} initialized");
    }
    protected void LogError(string message, Exception ex,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
    {
        _logger?.LogError(message, ex, caller, file, line);
    }

    protected void LogInfo(string message,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
    {
        _logger?.LogInfo(message, caller, file, line);
    }

    protected void LogDebug(string message,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
    {
        _logger?.LogDebug(message, caller, file, line);
    }

    protected void LogWarning(string message,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
    {
        _logger?.LogWarning(message, caller, file, line);
    }

    protected void LogTrace(string message,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
    {
        _logger?.LogTrace(message, caller, file, line);
    }

    protected void LogCritical(string message, Exception ex,[CallerMemberName] string caller = "",[CallerFilePath] string file = "",[CallerLineNumber] int line = 0)
    {
        _logger?.LogCritical(message, ex, caller, file, line);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        _logger?.LogDebug($"PropertyChanged: {propertyName}");
    }
}
