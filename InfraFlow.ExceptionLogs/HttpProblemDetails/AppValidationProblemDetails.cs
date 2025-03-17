using InfraFlow.Domain.Core.Exceptions;

namespace InfraFlow.ExceptionLogs.HttpProblemDetails;

public class AppValidationProblemDetails : AppProblemDetail
{
    public IEnumerable<ValidationExceptionModel> Errors { get; init; }
    
    public AppValidationProblemDetails(IEnumerable<ValidationExceptionModel> errors, string code, string message, string? detail, Guid? correlationId) : base(code, message, detail, correlationId)
    {
        Errors = errors;
    }
}