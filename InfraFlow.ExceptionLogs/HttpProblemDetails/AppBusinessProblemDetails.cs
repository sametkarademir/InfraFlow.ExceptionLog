namespace InfraFlow.ExceptionLogs.HttpProblemDetails;

public class AppBusinessProblemDetails : AppProblemDetail
{
    public AppBusinessProblemDetails(string code, string message, string? detail, Guid? correlationId) : base(code, message, detail, correlationId)
    {
    }
}