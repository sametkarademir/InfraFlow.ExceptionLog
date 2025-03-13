namespace InfraFlow.ExceptionLogs.Core.Types;

public class AppEntityNotFoundException : AppException
{
    public AppEntityNotFoundException(string message) : base(message)
    {
    }
    
    public AppEntityNotFoundException(string message, string detail) : base(message, detail)
    {
    }

    public AppEntityNotFoundException(string type, object id) : base($"Entity of type {type} with id {id} not found.")
    {
    }
    
    public AppEntityNotFoundException(string type, object id, string detail) : base($"Entity of type {type} with id {id} not found.", detail)
    {
    }
}