using System.Text.Json;
using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InfraFlow.ExceptionLogs.AspNetCore.DependencyInjection;

/// <summary>
/// Exception'ları yakalayıp loglayan ASP.NET Core middleware
/// </summary>
public class ExceptionLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IExceptionLogger _exceptionLogger;
    private readonly ILogger<ExceptionLoggingMiddleware> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public ExceptionLoggingMiddleware(
        RequestDelegate next,
        IExceptionLogger exceptionLogger,
        ILogger<ExceptionLoggingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _exceptionLogger = exceptionLogger ?? throw new ArgumentNullException(nameof(exceptionLogger));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Middleware'i çalıştırır ve exception'ları yakalar
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // İsteği devam ettir
            await _next(context);
        }
        catch (Exception ex)
        {
            // Exception'ı işle
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Exception'ı işler ve loglar
    /// </summary>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Exception'ı logla
        string additionalInfo = CollectRequestInfo(context);
        string userId = context.User?.Identity?.IsAuthenticated == true ? context.User.Identity.Name : null;
        string source = $"{context.Request.Method} {context.Request.Path}";

        await _exceptionLogger.LogExceptionAsync(exception, source, userId, additionalInfo);

        // Yanıt hazırla
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        // ProblemDetails yanıtı oluştur
        var problemDetails = ProblemDetailsBuilder.CreateFromException(
            exception,
            context.Response.StatusCode,
            context.Request.Path);

        // Yanıtı yaz
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await JsonSerializer.SerializeAsync(
            context.Response.Body,
            problemDetails,
            options);
    }

    /// <summary>
    /// İstek bilgilerini toplar
    /// </summary>
    private string CollectRequestInfo(HttpContext context)
    {
        var info = new Dictionary<string, object>
        {
            ["RequestHost"] = context.Request.Host.ToString(),
            ["RequestProtocol"] = context.Request.Protocol,
            ["RequestMethod"] = context.Request.Method,
            ["RequestPath"] = context.Request.Path.ToString(),
            ["RequestQueryString"] = context.Request.QueryString.ToString(),
            ["RequestHeaders"] = context.Request.Headers
                .Where(h => !h.Key.Contains("Authorization",
                    StringComparison.OrdinalIgnoreCase)) // Hassas bilgileri filtrele
                .ToDictionary(h => h.Key, h => h.Value.ToString()),
            ["RequestIpAddress"] = context.Connection.RemoteIpAddress?.ToString(),
            ["RequestUserAgent"] = context.Request.Headers.UserAgent.ToString(),
            ["RequestIsHttps"] = context.Request.IsHttps,
            ["TraceIdentifier"] = context.TraceIdentifier
        };

        return JsonSerializer.Serialize(info);
    }
}