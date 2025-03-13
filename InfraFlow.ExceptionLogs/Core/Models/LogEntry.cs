using InfraFlow.ExceptionLogs.Domain.Enums;

namespace InfraFlow.ExceptionLogs.Core.Models;

public class LogEntry
{
    public DateTime Timestamp { get; set; }
    public AppLogLevel Level { get; set; }
    public string? Message { get; set; }
    public Exception? Exception { get; set; }
    public string Category { get; set; } = null!;
    
    public Guid? CreatorId { get; set; }
    public Guid? CorrelationId { get; set; }
    public Guid? SnapshotId { get; set; }
    public Guid? SessionId { get; set; }
}