using InfraFlow.Domain.Core.Extensions;
using InfraFlow.ExceptionLogs.Core.Models;
using InfraFlow.ExceptionLogs.Domain.Entities;
using InfraFlow.ExceptionLogs.Domain.Enums;
using InfraFlow.ExceptionLogs.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;

namespace InfraFlow.ExceptionLogs.Infrastructure.Logging;

public class AppLogger<TClass> : IAppLogger<TClass> where TClass : class
{
    private readonly string _logDirectory;
    private readonly IAppExceptionLogRepository _appExceptionLogRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppLogger(IAppExceptionLogRepository appExceptionLogRepository, IHttpContextAccessor httpContextAccessor)
    {
        _appExceptionLogRepository = appExceptionLogRepository;
        _httpContextAccessor = httpContextAccessor;
        _logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

        if (!Directory.Exists(_logDirectory))
            Directory.CreateDirectory(_logDirectory);
    }
    
    public async Task LogVerboseAsync(string message, Exception? exception = null)
    {
        await Log(AppLogLevel.Verbose, message, exception);
    }

    public async Task LogInformationAsync(string message, Exception? exception = null)
    {
        await Log(AppLogLevel.Information, message, exception);
    }

    public async Task LogDebugAsync(string message, Exception? exception = null)
    {
        await Log(AppLogLevel.Debug, message, exception);
    }

    public async Task LogWarningAsync(string message, Exception? exception = null)
    {
        await Log(AppLogLevel.Warning, message, exception);
    }

    public async Task LogErrorAsync(string message, Exception? exception = null)
    {
        await Log(AppLogLevel.Error, message, exception);
    }

    public async Task LogFatalAsync(string message, Exception? exception = null)
    {
        await Log(AppLogLevel.Fatal, message, exception);
    }
    
    private async Task Log(AppLogLevel appLogLevel, string message, Exception? exception = null)
    {
        var consoleOutput = new ConsoleOutput();
        var fileOutput = new FileOutput(_logDirectory);
        
        var logEntry = new LogEntry
        {
            Timestamp = DateTime.UtcNow,
            Level = appLogLevel,
            Message = message,
            Exception = exception,
            Category = typeof(TClass).Name,
            CreatorId = _httpContextAccessor.HttpContext.User.GetUserId(),
            CorrelationId = _httpContextAccessor.HttpContext.Request.GetCorrelationId(),
            SnapshotId = _httpContextAccessor.HttpContext.Request.GetAppSnapshotId(),
            SessionId = _httpContextAccessor.HttpContext.Request.GetSessionId(),
        };
        
        consoleOutput.Write(logEntry);
        fileOutput.Write(logEntry);

        var appExceptionLog = new AppExceptionLog
        {
            CorrelationId = logEntry.CorrelationId,
            AppSnapshotId = logEntry.SnapshotId,
            SessionId = logEntry.SessionId,
            Type = exception?.GetType().Name,
            Message = logEntry.Message,
            Source = exception?.Source,
            StackTrace = exception?.StackTrace,
            InnerException = exception?.InnerException?.StackTrace
        };
        await _appExceptionLogRepository.AddAsync(appExceptionLog, true);
    }
}