using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using InfraFlow.ExceptionManagement.Abstractions.Models;
using InfraFlow.ExceptionManagement.Core.Configuration;
using InfraFlow.ExceptionManagement.Core.Enums;
using InfraFlow.ExceptionManagement.EntityFramework.Contexts;
using InfraFlow.ExceptionManagement.EntityFramework.Entities;
using Microsoft.Extensions.Options;

namespace InfraFlow.ExceptionManagement.EntityFramework.Sinks;

/// <summary>
/// Exception loglarını doğrudan veritabanına yazan sink
/// </summary>
public class DatabaseLogSink : ILogSink
{
    private readonly ExceptionLogDbContext _dbContext;
    private readonly ExceptionLoggingOptions _options;

    /// <summary>
    /// Constructor
    /// </summary>
    public DatabaseLogSink(
        ExceptionLogDbContext dbContext,
        IOptions<ExceptionLoggingOptions> options)
    {
        _dbContext = dbContext;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Exception logunu veritabanına yazar
    /// </summary>
    public async Task WriteAsync(ExceptionLog log)
    {
        // Veritabanı loglama etkin değilse çık
        if ((_options.LogDestinations & LogDestination.Database) != LogDestination.Database)
            return;

        try
        {
            // Model'i entity'ye dönüştür
            var entity = new ExceptionLogEntity
            {
                Id = string.IsNullOrEmpty(log.Id) ? Guid.NewGuid().ToString() : log.Id,
                Timestamp = log.Timestamp,
                Type = log.Type,
                Message = log.Message,
                StackTrace = log.StackTrace,
                Source = log.Source,
                InnerExceptionMessage = log.InnerExceptionMessage,
                InnerExceptionStackTrace = log.InnerExceptionStackTrace,
                Severity = log.Severity,
                Code = log.Code,
                Detail = log.Detail,
                UserId = log.UserId,
                AdditionalInfo = log.AdditionalInfo,
                CreatedAt = DateTime.UtcNow
            };

            // Veritabanına ekle
            await _dbContext.ExceptionLogs.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            // Loglama hatası uygulamayı etkilemesin
            // Loglama mekanizması başka hatalara neden olmamalı
        }
    }
}