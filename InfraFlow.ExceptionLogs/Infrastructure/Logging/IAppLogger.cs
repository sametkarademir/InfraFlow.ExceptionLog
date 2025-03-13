namespace InfraFlow.ExceptionLogs.Infrastructure.Logging;

public interface IAppLogger<TClass> where TClass : class
{
    Task LogVerboseAsync(string message, Exception? exception = null);
    Task LogInformationAsync(string message, Exception? exception = null);
    Task LogDebugAsync(string message, Exception? exception = null);
    Task LogWarningAsync(string message, Exception? exception = null);
    Task LogErrorAsync(string message, Exception? exception = null);
    Task LogFatalAsync(string message, Exception? exception = null);
}