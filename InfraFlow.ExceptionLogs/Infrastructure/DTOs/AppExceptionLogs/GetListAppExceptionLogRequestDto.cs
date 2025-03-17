using FluentValidation;

namespace InfraFlow.ExceptionLogs.Infrastructure.DTOs.AppExceptionLogs;

public class GetListAppExceptionLogRequestDto
{
    public Guid? CorrelationId { get; set; }
    public Guid? AppSnapshotId { get; set; }
    public Guid? SessionId { get; set; }
    
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 25;

    public string? Search { get; set; } = null;
}

public class GetListAppExceptionLogRequestDtoValidator : AbstractValidator<GetListAppExceptionLogRequestDto>
{
    public GetListAppExceptionLogRequestDtoValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        
        RuleFor(x => x.Search).MaximumLength(20);

        RuleFor(x => x.CorrelationId).NotEqual(Guid.Empty);
        RuleFor(x => x.AppSnapshotId).NotEqual(Guid.Empty);
        RuleFor(x => x.SessionId).NotEqual(Guid.Empty);
    }
}