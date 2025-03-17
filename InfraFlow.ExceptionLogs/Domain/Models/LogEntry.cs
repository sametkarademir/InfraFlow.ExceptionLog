using InfraFlow.ExceptionLogs.Domain.Enums;

namespace InfraFlow.ExceptionLogs.Domain.Models;

public class LogEntry
{
    public DateTime Timestamp { get; set; }
    public AppLogLevel Level { get; set; }
    public string? Message { get; set; }
    public Exception? Exception { get; set; }
    public string? Category { get; set; }
    
    public Guid? CreatorId { get; set; }
    public Guid? CorrelationId { get; set; }
    public Guid? SnapshotId { get; set; }
    public Guid? SessionId { get; set; }
    
    public LogEntry()
    {
        Timestamp = DateTime.UtcNow;
    }
    
    public LogEntry(AppLogLevel level, string message, Exception? exception = null, string? category = null, Guid? creatorId = null, Guid? correlationId = null, Guid? snapshotId = null, Guid? sessionId = null)
    {
        Timestamp = DateTime.UtcNow;
        Level = level;
        Message = message;
        Exception = exception;
        Category = category;
        CreatorId = creatorId;
        CorrelationId = correlationId;
        SnapshotId = snapshotId;
        SessionId = sessionId;
    }
}