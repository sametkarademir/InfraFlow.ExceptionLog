namespace InfraFlow.ExceptionLogs.Infrastructure.HttpProblemDetails;

public class AppUnauthorizedAccessProblemDetails : AppProblemDetail
{
    public AppUnauthorizedAccessProblemDetails(string code, string message, string? detail) : base(code, message, detail)
    {
    }
}