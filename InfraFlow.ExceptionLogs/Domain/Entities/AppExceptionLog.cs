using InfraFlow.Domain.Core.Aggregates.CreationAuditedAggregateRoots;
using InfraFlow.Domain.Core.Aggregates.Entities;

namespace InfraFlow.ExceptionLogs.Domain.Entities;

public class AppExceptionLog : CreationAuditedAggregateRoot<Guid>, IAppSnapshotEntity, ICorrelationEntity, ISessionEntity
{
    public string? Type { get; set; }
    public string? Message { get; set; }
    public string? Source { get; set; }
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
    
    public Guid? AppSnapshotId { get; set; }
    public Guid? CorrelationId { get; set; }
    public Guid? SessionId { get; set; }
}