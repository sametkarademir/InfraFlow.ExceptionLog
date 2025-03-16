using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using InfraFlow.ExceptionManagement.Core.Configuration;
using InfraFlow.ExceptionManagement.Core.Services;
using InfraFlow.ExceptionManagement.Core.Sinks;
using Microsoft.Extensions.DependencyInjection;

namespace InfraFlow.ExceptionManagement.Core.DependencyInjection;

/// <summary>
///
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Core exception loglama hizmetlerini servis koleksiyonuna ekler
    /// </summary>
    /// <param name="services">Servis koleksiyonu</param>
    /// <param name="configureOptions">Yapılandırma seçenekleri</param>
    /// <returns>Servis koleksiyonu</returns>
    public static IServiceCollection AddExceptionLoggingCore(
        this IServiceCollection services,
        Action<ExceptionLoggingOptions> configureOptions = null)
    {
        // Yapılandırma
        var options = new ExceptionLoggingOptions();
        configureOptions?.Invoke(options);
        services.Configure<ExceptionLoggingOptions>(opt =>
        {
            opt.IncludeFullStackTrace = options.IncludeFullStackTrace;
            opt.CaptureInnerExceptions = options.CaptureInnerExceptions;
            opt.LogToStandardLogger = options.LogToStandardLogger;
            opt.MaxAdditionalInfoLength = options.MaxAdditionalInfoLength;
            opt.LogDestinations = options.LogDestinations;
            opt.LogFolder = options.LogFolder;
        });

        // Temel servisler
        services.AddSingleton<ILogSink, ConsoleSink>();
        services.AddSingleton<ILogSink, FileSink>();
        services.AddScoped<IExceptionLogger, ExceptionLogger>();

        // InMemory repository - veritabanı implementasyonu eklenene kadar
        if (!services.Any(s => s.ServiceType == typeof(IExceptionStorage)))
        {
            services.AddSingleton<IExceptionStorage, InMemoryExceptionLogRepository>();
        }

        return services;
    }
}