using InfraFlow.ExceptionLogs.Core.Models;
using InfraFlow.ExceptionLogs.Domain.Enums;

namespace InfraFlow.ExceptionLogs.Infrastructure.Logging;

public class ConsoleOutput : ILogSink
{
    public void Write(LogEntry logEntry)
    {
        Console.ForegroundColor = GetColorForLogLevel(logEntry.Level);

        Console.WriteLine($"[{logEntry.Timestamp:yyyy-MM-dd HH:mm:ss}] [{logEntry.Level}] [{logEntry.Category}]");

        if (logEntry.SnapshotId.HasValue)
            Console.WriteLine($"  📝 SnapshotId: {logEntry.SnapshotId}");

        if (logEntry.CorrelationId.HasValue)
            Console.WriteLine($"  🔗 CorrelationId: {logEntry.CorrelationId}");

        if (logEntry.SessionId.HasValue)
            Console.WriteLine($"  🎭 SessionId: {logEntry.SessionId}");

        if (logEntry.CreatorId.HasValue)
            Console.WriteLine($"  👤 CreatorId: {logEntry.CreatorId}");

        Console.WriteLine($"  📝 Message: {logEntry.Message}");

        if (logEntry.Exception != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ❌ Exception: {logEntry.Exception.Message}");
            Console.WriteLine($"     StackTrace: {logEntry.Exception.StackTrace}");
        }

        Console.ResetColor();
    }
    
    private ConsoleColor GetColorForLogLevel(AppLogLevel level)
    {
        return level switch
        {
            AppLogLevel.Verbose => ConsoleColor.Gray,
            AppLogLevel.Debug => ConsoleColor.Blue,
            AppLogLevel.Information => ConsoleColor.Green,
            AppLogLevel.Warning => ConsoleColor.Yellow,
            AppLogLevel.Error => ConsoleColor.Red,
            AppLogLevel.Fatal => ConsoleColor.Magenta,
            _ => ConsoleColor.White,
        };
    }
}