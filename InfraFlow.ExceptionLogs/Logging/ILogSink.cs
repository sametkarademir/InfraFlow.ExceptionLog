using InfraFlow.ExceptionLogs.Domain.Models;

namespace InfraFlow.ExceptionLogs.Logging;

public interface ILogSink
{
    void Write(LogEntry logEntry);
}