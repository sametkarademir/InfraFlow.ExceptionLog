namespace InfraFlow.ExceptionLogs.Core.Types;

public class AppAuthenticationFailedException : AppException
{
    public AppAuthenticationFailedException() : base("Authentication failed.")
    {
    }

    public AppAuthenticationFailedException(string message) : base(message)
    {
    }
    
    public AppAuthenticationFailedException(string message, string detail) : base(message, detail)
    {
    }
}