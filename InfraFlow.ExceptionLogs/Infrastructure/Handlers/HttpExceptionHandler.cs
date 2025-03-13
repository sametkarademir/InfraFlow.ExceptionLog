using InfraFlow.ExceptionLogs.Core.Extensions;
using InfraFlow.ExceptionLogs.Core.Types;
using InfraFlow.ExceptionLogs.Infrastructure.HttpProblemDetails;
using Microsoft.AspNetCore.Http;

namespace InfraFlow.ExceptionLogs.Infrastructure.Handlers;

public class HttpExceptionHandler : ExceptionHandler
{
    public HttpResponse Response
    {
        get => _response ?? throw new NullReferenceException(nameof(_response));
        set => _response = value;
    }
    private HttpResponse? _response;
    
    public override Task HandleException(AppAuthenticationFailedException exception)
    {
        var details = new AppAuthenticationFailedProblemDetails("401", exception.Message, exception.Detail).ToJson();
        
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Response.WriteAsync(details);
    }
    public override Task HandleException(AppUnauthorizedAccessException exception)
    {
        var details = new AppUnauthorizedAccessProblemDetails("403", exception.Message, exception.Detail).ToJson();
        
        Response.StatusCode = StatusCodes.Status403Forbidden;
        return Response.WriteAsync(details);
    }
    public override Task HandleException(AppEntityNotFoundException exception)
    {
        var details = new AppEntityNotFoundProblemDetails("404", exception.Message, exception.Detail).ToJson();
        
        Response.StatusCode = StatusCodes.Status404NotFound;
        return Response.WriteAsync(details);
    }

    public override Task HandleException(AppBusinessException exception)
    {
        var details = new AppBusinessProblemDetails(exception.Code, exception.Message, exception.Detail).ToJson();
        
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        return Response.WriteAsync(details);
    }

    public override Task HandleException(AppValidationException exception)
    {
        var details = new AppValidationProblemDetails(exception.Errors, "400", exception.Message, exception.Detail).ToJson();
        
        Response.StatusCode = StatusCodes.Status400BadRequest;
        return Response.WriteAsync(details);
    }

    public override Task HandleException(Exception exception)
    {
        var details = new AppInternalServerErrorProblemDetails("500", exception.Message, string.Empty).ToJson();
        
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        return Response.WriteAsync(details);
    }
}