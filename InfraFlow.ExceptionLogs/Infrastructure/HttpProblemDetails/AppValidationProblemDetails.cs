using InfraFlow.ExceptionLogs.Core.Models;

namespace InfraFlow.ExceptionLogs.Infrastructure.HttpProblemDetails;

public class AppValidationProblemDetails : AppProblemDetail
{
    public IEnumerable<ValidationExceptionModel> Errors { get; init; }
    
    public AppValidationProblemDetails(IEnumerable<ValidationExceptionModel> errors, string code, string message, string? detail) : base(code, message, detail)
    {
        Errors = errors;
    }
}