namespace InfraFlow.ExceptionLogs.Core.Models;

public class ValidationExceptionModel
{
    public string? Property { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}
