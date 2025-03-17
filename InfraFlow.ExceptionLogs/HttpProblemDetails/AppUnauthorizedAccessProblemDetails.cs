namespace InfraFlow.ExceptionLogs.HttpProblemDetails;

public class AppUnauthorizedAccessProblemDetails : AppProblemDetail
{
    public AppUnauthorizedAccessProblemDetails(string code, string message, string? detail, Guid? correlationId) : base(code, message, detail, correlationId)
    {
    }
}