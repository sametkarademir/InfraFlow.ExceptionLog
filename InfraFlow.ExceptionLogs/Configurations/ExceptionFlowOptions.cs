namespace InfraFlow.ExceptionLogs.Configurations;

public class ExceptionFlowOptions
{
    public bool EnableConsoleLogging { get; set; } = true;
    public bool EnableFileLogging { get; set; } = true;
    public string LogDirectory { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
}