using InfraFlow.ExceptionLogs.Domain.Models;

namespace InfraFlow.ExceptionLogs.Logging;

public class DatabaseLogSink : ILogSink
{
    public void Write(LogEntry logEntry)
    {
        throw new NotImplementedException();
    }
}