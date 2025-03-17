using InfraFlow.ExceptionLogs.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace InfraFlow.ExceptionLogs.DependencyInjection;

public static class ValidationFilterExtensions
{
    public static IMvcBuilder AddGlobalValidationFilter(this IMvcBuilder builder)
    {
        builder.Services.AddScoped<ValidationFilter>();
        builder.AddMvcOptions(options => { options.Filters.Add<ValidationFilter>(); });

        return builder;
    }
}