namespace InfraFlow.ExceptionLogs.HttpProblemDetails;

public abstract class AppProblemDetail
{
    public string? Code { get; set; }
    public string? Message { get; set; }
    public string? Detail { get; set; }
    public Guid? CorrelationId { get; set; }
    
    protected AppProblemDetail(string? code, string? message, string? detail, Guid? correlationId)
    {
        Code = code;
        Message = message;
        Detail = detail;
        CorrelationId = correlationId;
    }
}