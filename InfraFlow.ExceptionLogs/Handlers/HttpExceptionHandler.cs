using InfraFlow.Domain.Core.Exceptions;
using InfraFlow.ExceptionLogs.Extensions;
using InfraFlow.ExceptionLogs.HttpProblemDetails;
using Microsoft.AspNetCore.Http;

namespace InfraFlow.ExceptionLogs.Handlers;

public class HttpExceptionHandler : ExceptionHandler
{
    public HttpResponse Response
    {
        get => _response ?? throw new NullReferenceException(nameof(_response));
        set => _response = value;
    }
    private HttpResponse? _response;
    
    public override Task HandleException(AppAuthenticationFailedException exception, Guid? correlationId = null)
    {
        var details = new AppAuthenticationFailedProblemDetails("401", exception.Message, exception.Detail, correlationId).ToJson();
        
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Response.WriteAsync(details);
    }
    public override Task HandleException(AppUnauthorizedAccessException exception, Guid? correlationId = null)
    {
        var details = new AppUnauthorizedAccessProblemDetails("403", exception.Message, exception.Detail, correlationId).ToJson();
        
        Response.StatusCode = StatusCodes.Status403Forbidden;
        return Response.WriteAsync(details);
    }
    public override Task HandleException(AppEntityNotFoundException exception, Guid? correlationId = null)
    {
        var details = new AppEntityNotFoundProblemDetails("404", exception.Message, exception.Detail, correlationId).ToJson();
        
        Response.StatusCode = StatusCodes.Status404NotFound;
        return Response.WriteAsync(details);
    }

    public override Task HandleException(AppBusinessException exception, Guid? correlationId = null)
    {
        var details = new AppBusinessProblemDetails(exception.Code, exception.Message, exception.Detail, correlationId).ToJson();
        
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        return Response.WriteAsync(details);
    }

    public override Task HandleException(AppValidationException exception, Guid? correlationId = null)
    {
        var details = new AppValidationProblemDetails(exception.Errors, "400", exception.Message, exception.Detail, correlationId).ToJson();
        
        Response.StatusCode = StatusCodes.Status400BadRequest;
        return Response.WriteAsync(details);
    }

    public override Task HandleException(Exception exception, Guid? correlationId = null)
    {
        var details = new AppInternalServerErrorProblemDetails("500", exception.Message, string.Empty, correlationId).ToJson();
        
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        return Response.WriteAsync(details);
    }
}