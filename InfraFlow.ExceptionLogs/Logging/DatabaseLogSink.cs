using InfraFlow.ExceptionLogs.Domain.Entities;
using InfraFlow.ExceptionLogs.Domain.Models;
using InfraFlow.ExceptionLogs.Infrastructure.Repositories;

namespace InfraFlow.ExceptionLogs.Logging;

public class DatabaseLogSink : ILogSink
{
    private readonly IAppExceptionLogRepository _appExceptionLogRepository;

    public DatabaseLogSink(IAppExceptionLogRepository appExceptionLogRepository)
    {
        _appExceptionLogRepository = appExceptionLogRepository;
    }

    public async Task Write(LogEntry logEntry)
    {
        var appExceptionLog = new AppExceptionLog
        {
            CorrelationId = logEntry.CorrelationId,
            AppSnapshotId = logEntry.SnapshotId,
            SessionId = logEntry.SessionId,
            Type = logEntry.Exception?.GetType().Name,
            Message = logEntry.Message,
            Source = logEntry.Exception?.Source,
            StackTrace = logEntry.Exception?.StackTrace,
            InnerException = logEntry.Exception?.InnerException?.StackTrace
        };
        
        await _appExceptionLogRepository.AddAsync(appExceptionLog, true);
    }
}