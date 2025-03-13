using System.Reflection;
using FluentValidation;
using InfraFlow.ExceptionLogs.Infrastructure.Logging;
using InfraFlow.ExceptionLogs.Infrastructure.Repositories;
using InfraFlow.ExceptionLogs.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfraFlow.ExceptionLogs.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfraFlowExceptionLogServices<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.AddScoped<IAppExceptionLogRepository, AppExceptionLogRepository<TContext>>();
        services.AddScoped<IAppExceptionLogAppService, AppExceptionLogAppService>();
        services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}