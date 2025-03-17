namespace InfraFlow.ExceptionLogs.HttpProblemDetails;

public class AppAuthenticationFailedProblemDetails : AppProblemDetail
{
    public AppAuthenticationFailedProblemDetails(string code, string message, string? detail, Guid? correlationId) : base(code, message, detail, correlationId)
    {
    }
}