using System.Text.Json;
using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InfraFlow.ExceptionLogs.AspNetCore.Filters;

/// <summary>
/// MVC Controller'ları için exception loglama filtresi
/// </summary>
public class ExceptionLoggingFilter : IExceptionFilter
{
    private readonly IExceptionLogger _exceptionLogger;

    /// <summary>
    /// Constructor
    /// </summary>
    public ExceptionLoggingFilter(IExceptionLogger exceptionLogger)
    {
        _exceptionLogger = exceptionLogger ?? throw new ArgumentNullException(nameof(exceptionLogger));
    }

    /// <summary>
    /// Exception oluştuğunda çalışır
    /// </summary>
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var httpContext = context.HttpContext;

        // Controller ve action bilgilerini al
        var controllerName = context.RouteData.Values["controller"]?.ToString();
        var actionName = context.RouteData.Values["action"]?.ToString();
        var source = $"{controllerName}/{actionName}";

        // Kullanıcı bilgisini al
        var userId = httpContext.User?.Identity?.IsAuthenticated == true
            ? httpContext.User.Identity.Name
            : null;

        // Ek bilgileri topla
        var additionalInfo = CollectContextInfo(context);

        // Exception'ı logla (asenkron metodu senkron şekilde çağır)
        _exceptionLogger.LogExceptionAsync(
                exception,
                source,
                userId,
                additionalInfo)
            .GetAwaiter()
            .GetResult();

        // Exception'ın normal şekilde işlenmesine izin ver
        // MVC'nin kendi exception handling mekanizması çalışacak
    }

    /// <summary>
    /// İstek bağlamından bilgileri toplar
    /// </summary>
    private string CollectContextInfo(ExceptionContext context)
    {
        var httpContext = context.HttpContext;

        var info = new Dictionary<string, object>
        {
            ["Controller"] = context.RouteData.Values["controller"],
            ["Action"] = context.RouteData.Values["action"],
            ["RequestPath"] = httpContext.Request.Path.ToString(),
            ["RequestMethod"] = httpContext.Request.Method,
            ["RequestQueryString"] = httpContext.Request.QueryString.ToString(),
            ["RequestIpAddress"] = httpContext.Connection.RemoteIpAddress?.ToString(),
            ["RequestUserAgent"] = httpContext.Request.Headers.UserAgent.ToString(),
            ["TraceIdentifier"] = httpContext.TraceIdentifier
        };

        // Model state hatalarını ekle
        if (!context.ModelState.IsValid)
        {
            var modelStateErrors = context.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            info["ModelStateErrors"] = modelStateErrors;
        }

        return JsonSerializer.Serialize(info);
    }
}