namespace InfraFlow.ExceptionLogs.HttpProblemDetails;

public class AppEntityNotFoundProblemDetails : AppProblemDetail
{
    public AppEntityNotFoundProblemDetails(string code, string message, string? detail, Guid? correlationId) : base(code, message, detail, correlationId)
    {
    }
}