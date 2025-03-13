namespace InfraFlow.ExceptionLogs.Infrastructure.HttpProblemDetails;

public class AppEntityNotFoundProblemDetails : AppProblemDetail
{
    public AppEntityNotFoundProblemDetails(string code, string message, string? detail) : base(code, message, detail)
    {
    }
}