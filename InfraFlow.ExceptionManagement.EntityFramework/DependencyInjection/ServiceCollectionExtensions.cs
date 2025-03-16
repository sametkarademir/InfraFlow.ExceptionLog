using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using InfraFlow.ExceptionManagement.EntityFramework.Contexts;
using InfraFlow.ExceptionManagement.EntityFramework.Repositories;
using InfraFlow.ExceptionManagement.EntityFramework.Sinks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfraFlow.ExceptionManagement.EntityFramework.DependencyInjection;

/// <summary>
///
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Entity Framework ile Exception loglama servislerini ekler
    /// </summary>
    /// <param name="services">Servis koleksiyonu</param>
    /// <param name="optionsAction">DbContext yapılandırma seçenekleri</param>
    /// <returns>Servis koleksiyonu</returns>
    public static IServiceCollection AddExceptionLoggingEntityFramework(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsAction)
    {
        // DbContext ekle
        services.AddDbContext<ExceptionLogDbContext>(optionsAction);
            
        // Repository ekle
        services.AddScoped<IExceptionStorage, EfExceptionLogRepository>();
            
        // Database sink ekle
        services.AddScoped<ILogSink, DatabaseLogSink>();
            
        return services;
    }
        
    /// <summary>
    /// Var olan bir DbContext ile Entity Framework entegrasyonu ekler
    /// </summary>
    /// <typeparam name="TContext">Kullanılacak DbContext tipi</typeparam>
    /// <param name="services">Servis koleksiyonu</param>
    /// <returns>Servis koleksiyonu</returns>
    public static IServiceCollection AddExceptionLoggingWithExistingDbContext<TContext>(
        this IServiceCollection services)
        where TContext : DbContext
    {
        // Repository ekle (özel TContext için)
        services.AddScoped<IExceptionStorage, EfExceptionLogRepository>();
            
        // Database sink ekle (özel TContext için)
        services.AddScoped<ILogSink, DatabaseLogSink>();
            
        return services;
    }
}