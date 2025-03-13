namespace InfraFlow.ExceptionLogs.Infrastructure.HttpProblemDetails;

public abstract class AppProblemDetail
{
    public string? Code { get; set; }
    public string? Message { get; set; }
    public string? Detail { get; set; }
    
    protected AppProblemDetail(string? code, string? message, string? detail)
    {
        Code = code;
        Message = message;
        Detail = detail;
    }
}