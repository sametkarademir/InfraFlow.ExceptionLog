namespace InfraFlow.ExceptionLogs.Core.Types;

public class AppException : Exception
{
    public string? Detail { get; set; }
    
    public AppException() : base("An error occurred.")
    {
    }
    
    public AppException(string message) : base(message)
    {
    }
    
    public AppException(string message, string detail) : base(message)
    {
        Detail = detail;
    }
}