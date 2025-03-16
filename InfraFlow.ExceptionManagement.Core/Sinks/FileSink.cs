using System.Text;
using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using InfraFlow.ExceptionManagement.Abstractions.Models;
using InfraFlow.ExceptionManagement.Core.Configuration;
using InfraFlow.ExceptionManagement.Core.Enums;
using Microsoft.Extensions.Options;

namespace InfraFlow.ExceptionManagement.Core.Sinks;

/// <summary>
/// Exception loglarını dosyaya yazan sink
/// </summary>
public class FileSink : ILogSink
{
    private readonly ExceptionLoggingOptions _options;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    /// <summary>
    /// Constructor
    /// </summary>
    public FileSink(IOptions<ExceptionLoggingOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

        // Log klasörünün varlığını kontrol et, yoksa oluştur
        if (!Directory.Exists(_options.LogFolder))
        {
            Directory.CreateDirectory(_options.LogFolder);
        }
    }

    /// <summary>
    /// Exception logunu dosyaya yazar
    /// </summary>
    public async Task WriteAsync(ExceptionLog log)
    {
        // Dosya loglama etkin değilse çık
        if ((_options.LogDestinations & LogDestination.File) != LogDestination.File)
            return;

        var logFileName = Path.Combine(_options.LogFolder, $"exceptions-{DateTime.UtcNow:yyyy-MM-dd}.log");
        var logContent = FormatLogEntry(log);

        // Thread-safe dosya yazma için semafor kullan
        await _semaphore.WaitAsync();
        try
        {
            await File.AppendAllTextAsync(logFileName, logContent);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Log kaydını dosya formatında biçimlendirir
    /// </summary>
    private string FormatLogEntry(ExceptionLog log)
    {
        var sb = new StringBuilder();

        sb.AppendLine("========================================================");
        sb.AppendLine($"Timestamp: {log.Timestamp:yyyy-MM-dd HH:mm:ss.fff} UTC");
        sb.AppendLine($"ID: {log.Id}");
        sb.AppendLine($"Severity: {log.Severity}");
        sb.AppendLine($"Type: {log.Type}");
        sb.AppendLine($"Message: {log.Message}");
        sb.AppendLine($"Source: {log.Source}");

        if (!string.IsNullOrEmpty(log.Code))
            sb.AppendLine($"Code: {log.Code}");

        if (!string.IsNullOrEmpty(log.Detail))
            sb.AppendLine($"Detail: {log.Detail}");

        if (!string.IsNullOrEmpty(log.UserId))
            sb.AppendLine($"User ID: {log.UserId}");

        if (!string.IsNullOrEmpty(log.AdditionalInfo))
        {
            sb.AppendLine("Additional Info:");
            sb.AppendLine(log.AdditionalInfo);
        }

        if (!string.IsNullOrEmpty(log.StackTrace))
        {
            sb.AppendLine("Stack Trace:");
            sb.AppendLine(log.StackTrace);
        }

        if (!string.IsNullOrEmpty(log.InnerExceptionMessage))
        {
            sb.AppendLine("Inner Exception:");
            sb.AppendLine($"Message: {log.InnerExceptionMessage}");

            if (!string.IsNullOrEmpty(log.InnerExceptionStackTrace))
            {
                sb.AppendLine("Inner Stack Trace:");
                sb.AppendLine(log.InnerExceptionStackTrace);
            }
        }

        sb.AppendLine("========================================================");
        sb.AppendLine(); // Boş satır ekle

        return sb.ToString();
    }
}