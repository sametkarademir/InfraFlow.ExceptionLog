using FluentValidation;
using InfraFlow.EntityFramework.Core.Models;

namespace InfraFlow.ExceptionLogs.Infrastructure.DTOs.AppExceptionLogs;

public class GetListAppExceptionLogRequestDto
{
    public Guid? CorrelationId { get; set; }
    public Guid? AppSnapshotId { get; set; }
    public Guid? SessionId { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 25;

    public string? Search { get; set; } = null;

    public List<Sort>? Sorts { get; set; } = null;
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

        RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate);

        RuleForEach(x => x.Sorts).ChildRules(sort =>
        {
            sort.RuleFor(x => x.Field).NotEmpty();
            sort.RuleFor(x => x.SortType).IsInEnum();

            sort.RuleFor(x => x.Field).Must(x =>
                x == "CorrelationId" || x == "AppSnapshotId" || x == "SessionId" || x == "StartDate" ||
                x == "EndDate" || x == "Type" || x == "Message" || x == "Source" || x == "StackTrace" ||
                x == "InnerException");
        });
    }
}