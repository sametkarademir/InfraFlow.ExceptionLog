using InfraFlow.ExceptionLogs.Domain.Models;

namespace InfraFlow.ExceptionLogs.Logging;

public interface ILogSink
{
    Task Write(LogEntry logEntry);
}