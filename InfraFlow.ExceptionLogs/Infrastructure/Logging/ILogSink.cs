using InfraFlow.ExceptionLogs.Core.Models;

namespace InfraFlow.ExceptionLogs.Infrastructure.Logging;

public interface ILogSink
{
    void Write(LogEntry logEntry);
}