namespace InfraFlow.ExceptionLogs.Infrastructure.HttpProblemDetails;

public class AppBusinessProblemDetails : AppProblemDetail
{
    public AppBusinessProblemDetails(string code, string message, string? detail) : base(code, message, detail)
    {
    }
}