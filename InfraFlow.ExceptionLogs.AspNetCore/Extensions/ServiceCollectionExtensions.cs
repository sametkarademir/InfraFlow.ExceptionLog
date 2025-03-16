using InfraFlow.ExceptionLogs.AspNetCore.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace InfraFlow.ExceptionLogs.AspNetCore.Extensions;

/// <summary>
/// IServiceCollection için extension metotları
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Exception loglama web entegrasyonunu ekler
    /// </summary>
    public static IServiceCollection AddExceptionLoggingWeb(this IServiceCollection services)
    {
        // MVC filtresi ekle
        services.AddScoped<ExceptionLoggingFilter>();
            
        // Filtre global olarak eklensin mi?
        services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionLoggingFilter>();
        });
            
        return services;
    }
}