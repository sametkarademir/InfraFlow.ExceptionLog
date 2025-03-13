using Microsoft.AspNetCore.Builder;

namespace InfraFlow.ExceptionLogs.DependencyInjection;

public static class ApplicationBuilderExceptionMiddlewareExtensions
{
    public static void ConfigureInfraFlowExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
