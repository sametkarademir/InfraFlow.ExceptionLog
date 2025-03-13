namespace InfraFlow.ExceptionLogs.Infrastructure.HttpProblemDetails;

public class AppAuthenticationFailedProblemDetails : AppProblemDetail
{
    public AppAuthenticationFailedProblemDetails(string code, string message, string? detail) : base(code, message, detail)
    {
    }
}