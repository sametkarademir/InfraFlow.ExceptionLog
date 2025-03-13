using InfraFlow.Domain.Core.DTOs;

namespace InfraFlow.ExceptionLogs.Infrastructure.DTOs.AppExceptionLogs;

public class AppExceptionLogResponseDto : CreationAuditedEntityDto<Guid>
{
    public string? Type { get; set; }
    public string? Message { get; set; }
    public string? Source { get; set; }
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
}