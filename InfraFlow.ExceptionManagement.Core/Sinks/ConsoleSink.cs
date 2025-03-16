using InfraFlow.ExceptionManagement.Abstractions.Enums;
using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using InfraFlow.ExceptionManagement.Abstractions.Models;
using InfraFlow.ExceptionManagement.Core.Configuration;
using InfraFlow.ExceptionManagement.Core.Enums;
using Microsoft.Extensions.Options;

namespace InfraFlow.ExceptionManagement.Core.Sinks;

/// <summary>
/// Exception loglarını konsola yazan sink
/// </summary>
public class ConsoleSink : ILogSink
{
    private readonly ExceptionLoggingOptions _options;

    /// <summary>
    /// Constructor
    /// </summary>
    public ConsoleSink(IOptions<ExceptionLoggingOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Exception logunu konsola yazar
    /// </summary>
    public Task WriteAsync(ExceptionLog log)
    {
        // Konsol loglama etkin değilse çık
        if ((_options.LogDestinations & LogDestination.Console) != LogDestination.Console)
            return Task.CompletedTask;

        // Orijinal konsol rengini kaydet
        var originalColor = Console.ForegroundColor;

        try
        {
            // Başlık
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n--- EXCEPTION LOG: {log.Timestamp:yyyy-MM-dd HH:mm:ss} ---");

            // Temel bilgiler
            Console.ForegroundColor = GetSeverityColor(log.Severity);
            Console.WriteLine($"Severity: {log.Severity}");
            Console.WriteLine($"Type: {log.Type}");
            Console.WriteLine($"Message: {log.Message}");
            Console.WriteLine($"Source: {log.Source}");

            // Kod ve detay
            if (!string.IsNullOrEmpty(log.Code))
                Console.WriteLine($"Code: {log.Code}");

            if (!string.IsNullOrEmpty(log.Detail))
                Console.WriteLine($"Detail: {log.Detail}");

            // Kullanıcı
            if (!string.IsNullOrEmpty(log.UserId))
                Console.WriteLine($"User ID: {log.UserId}");

            // Stack trace
            if (!string.IsNullOrEmpty(log.StackTrace))
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Stack Trace:");
                Console.WriteLine(log.StackTrace);
            }

            // İç exception
            if (!string.IsNullOrEmpty(log.InnerExceptionMessage))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Inner Exception:");
                Console.WriteLine($"Message: {log.InnerExceptionMessage}");

                if (!string.IsNullOrEmpty(log.InnerExceptionStackTrace))
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Inner Stack Trace:");
                    Console.WriteLine(log.InnerExceptionStackTrace);
                }
            }

            // Alt çizgi
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-----------------------------------\n");
        }
        finally
        {
            // Orijinal rengi geri yükle
            Console.ForegroundColor = originalColor;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Ciddiyet seviyesine göre uygun rengi döndürür
    /// </summary>
    private static ConsoleColor GetSeverityColor(ExceptionSeverity severity)
    {
        return severity switch
        {
            ExceptionSeverity.Information => ConsoleColor.Blue,
            ExceptionSeverity.Warning => ConsoleColor.Yellow,
            ExceptionSeverity.Error => ConsoleColor.Red,
            ExceptionSeverity.Critical => ConsoleColor.Magenta,
            _ => ConsoleColor.White
        };
    }
}