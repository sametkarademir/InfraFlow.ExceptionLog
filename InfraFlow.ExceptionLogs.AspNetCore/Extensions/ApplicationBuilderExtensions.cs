using InfraFlow.ExceptionLogs.AspNetCore.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace InfraFlow.ExceptionLogs.AspNetCore.Extensions;

/// <summary>
/// IApplicationBuilder için extension metotları
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Exception loglama middleware'ini ekler
    /// </summary>
    public static IApplicationBuilder UseExceptionLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionLoggingMiddleware>();
    }
}