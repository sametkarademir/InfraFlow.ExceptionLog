namespace InfraFlow.ExceptionLogs.Logging;

public interface IAppLogger<TClass> where TClass : class
{
    void LogVerbose(string message, Exception? exception = null);
    void LogInformation(string message, Exception? exception = null);
    void LogDebug(string message, Exception? exception = null);
    void LogWarning(string message, Exception? exception = null);
    void LogError(string message, Exception? exception = null);
    void LogFatal(string message, Exception? exception = null);
}