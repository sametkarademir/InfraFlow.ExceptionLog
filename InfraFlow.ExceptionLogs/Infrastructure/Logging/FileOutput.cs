using System.Text;
using InfraFlow.ExceptionLogs.Core.Models;

namespace InfraFlow.ExceptionLogs.Infrastructure.Logging;

public class FileOutput : ILogSink
{
    private readonly string _logDirectory;

    public FileOutput(string logDirectory)
    {
        _logDirectory = logDirectory;
        
        if (!Directory.Exists(_logDirectory))
        {
            Directory.CreateDirectory(_logDirectory);
        }
    }

    public void Write(LogEntry logEntry)
    {
        string logFileName = Path.Combine(_logDirectory, $"{DateTime.UtcNow:yyyy-MM-dd}.log");

        var logBuilder = new StringBuilder();
        logBuilder.AppendLine("--------------------------------------------------");
        logBuilder.AppendLine($"📅 Timestamp   : {logEntry.Timestamp:yyyy-MM-dd HH:mm:ss}");
        logBuilder.AppendLine($"⚡ Level       : {logEntry.Level}");
        logBuilder.AppendLine($"📁 Category    : {logEntry.Category}");
        
        if (logEntry.SnapshotId.HasValue)
            logBuilder.AppendLine($"📝 SnapshotId  : {logEntry.SnapshotId}");

        if (logEntry.CorrelationId.HasValue)
            logBuilder.AppendLine($"🔗 CorrelationId: {logEntry.CorrelationId}");

        if (logEntry.SessionId.HasValue)
            logBuilder.AppendLine($"🎭 SessionId   : {logEntry.SessionId}");

        if (logEntry.CreatorId.HasValue)
            logBuilder.AppendLine($"👤 CreatorId   : {logEntry.CreatorId}");

        logBuilder.AppendLine($"📝 Message     : {logEntry.Message}");

        if (logEntry.Exception != null)
        {
            logBuilder.AppendLine($"❌ Exception   : {logEntry.Exception.Message}");
            logBuilder.AppendLine("🔍 StackTrace  :");
            logBuilder.AppendLine(logEntry.Exception.StackTrace);
        }

        logBuilder.AppendLine("--------------------------------------------------");
        
        File.AppendAllText(logFileName, logBuilder.ToString() + Environment.NewLine);
    }
}
