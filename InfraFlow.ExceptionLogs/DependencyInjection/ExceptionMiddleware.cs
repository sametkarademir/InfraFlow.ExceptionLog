using System.Net.Mime;
using InfraFlow.ExceptionLogs.Infrastructure.Handlers;
using InfraFlow.ExceptionLogs.Infrastructure.Logging;
using Microsoft.AspNetCore.Http;

namespace InfraFlow.ExceptionLogs.DependencyInjection;

public class ExceptionMiddleware(RequestDelegate next, IAppLogger<ExceptionMiddleware> appLogger)
{
    private readonly HttpExceptionHandler _httpExceptionHandler = new();
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context.Response, exception);
            await appLogger.LogErrorAsync(exception.Message, exception);
        }
    }
    
    protected virtual Task HandleExceptionAsync(HttpResponse response, dynamic exception)
    {
        response.ContentType = MediaTypeNames.Application.Json;
        _httpExceptionHandler.Response = response;
        
        return _httpExceptionHandler.HandleException(exception);
    }
}