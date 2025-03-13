namespace InfraFlow.ExceptionLogs.Core.Types;

public class AppUnauthorizedAccessException : AppException
{
    public AppUnauthorizedAccessException() : base("Unauthorized access.")
    {
    }

    public AppUnauthorizedAccessException(string message) : base(message)
    {
    }
    
    public AppUnauthorizedAccessException(string message, string detail) : base(message, detail)
    {
    }
}