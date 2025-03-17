using System.Reflection;
using FluentValidation;
using InfraFlow.ExceptionLogs.Configurations;
using InfraFlow.ExceptionLogs.Infrastructure.Repositories;
using InfraFlow.ExceptionLogs.Infrastructure.Services;
using InfraFlow.ExceptionLogs.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfraFlow.ExceptionLogs.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfraFlowExceptionLogServices<TContext>(this IServiceCollection services, Action<ExceptionFlowOptions>? configure = null) where TContext : DbContext
    {
        var options = new ExceptionFlowOptions();
        configure?.Invoke(options);
        
        services.AddScoped<IAppExceptionLogRepository, AppExceptionLogRepository<TContext>>();
        services.AddScoped<IAppExceptionLogAppService, AppExceptionLogAppService>();
        services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
        
        services.AddHttpContextAccessor();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddScoped<ILogSink, DatabaseLogSink>();
        
        if (options.EnableConsoleLogging)
        {
            services.AddSingleton<ILogSink, ConsoleLogSink>();
        }
        
        if (options.EnableFileLogging)
        {
            services.AddSingleton<ILogSink, FileLogSink>();
        }
        
        return services;
    }
}