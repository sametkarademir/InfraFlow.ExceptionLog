namespace InfraFlow.ExceptionLogs.Infrastructure.HttpProblemDetails;

public class AppInternalServerErrorProblemDetails : AppProblemDetail
{
    public AppInternalServerErrorProblemDetails(string code, string message, string? detail) : base(code, message, detail)
    {
    }
}