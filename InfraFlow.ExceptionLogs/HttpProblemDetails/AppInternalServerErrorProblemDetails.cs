namespace InfraFlow.ExceptionLogs.HttpProblemDetails;

public class AppInternalServerErrorProblemDetails : AppProblemDetail
{
    public AppInternalServerErrorProblemDetails(string code, string message, string? detail, Guid? correlationId) : base(code, message, detail, correlationId)
    {
    }
}