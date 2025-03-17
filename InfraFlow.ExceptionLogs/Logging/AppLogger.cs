using InfraFlow.Domain.Core.Extensions;
using InfraFlow.ExceptionLogs.Domain.Enums;
using InfraFlow.ExceptionLogs.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace InfraFlow.ExceptionLogs.Logging;

public class AppLogger<TClass>(IHttpContextAccessor httpContextAccessor, IEnumerable<ILogSink> sinks)
    : IAppLogger<TClass>
    where TClass : class
{
    #region Log Methods
    public void LogVerbose(string message, Exception? exception = null)
    {
        Log(AppLogLevel.Verbose, message, exception);
    }

    public void LogInformation(string message, Exception? exception = null)
    {
        Log(AppLogLevel.Information, message, exception);
    }

    public void LogDebug(string message, Exception? exception = null)
    {
        Log(AppLogLevel.Debug, message, exception);
    }

    public void LogWarning(string message, Exception? exception = null)
    {
        Log(AppLogLevel.Warning, message, exception);
    }

    public void LogError(string message, Exception? exception = null)
    {
        Log(AppLogLevel.Error, message, exception);
    }

    public void LogFatal(string message, Exception? exception = null)
    {
        Log(AppLogLevel.Fatal, message, exception);
    }
    #endregion
    
    private void Log(AppLogLevel appLogLevel, string message, Exception? exception = null)
    {
        var logEntry = new LogEntry
        {
            Timestamp = DateTime.UtcNow,
            Level = appLogLevel,
            Message = message,
            Exception = exception,
            Category = typeof(TClass).Name,
            CreatorId = httpContextAccessor.HttpContext.User.GetUserId(),
            CorrelationId = httpContextAccessor.HttpContext.Request.GetCorrelationId(),
            SnapshotId = httpContextAccessor.HttpContext.Request.GetAppSnapshotId(),
            SessionId = httpContextAccessor.HttpContext.Request.GetSessionId(),
        };
        
        foreach (var sink in sinks)
        {
            sink.Write(logEntry);
        }
    }
}