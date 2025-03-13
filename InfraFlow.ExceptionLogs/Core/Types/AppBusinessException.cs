namespace InfraFlow.ExceptionLogs.Core.Types;

public class AppBusinessException : AppException
{
    public string Code { get; set; }
    
    public AppBusinessException() : base("An error occurred.")
    {
        Code = "500";
    }

    public AppBusinessException(string message) : base(message)
    {
        Code = "500";
    }
    
    public AppBusinessException(string message, string code) : base(message)
    {
        Code = code;
    }
    
    public AppBusinessException(string message, string code, string detail) : base(message, detail)
    {
        Code = code;
    }
}