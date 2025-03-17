using InfraFlow.Domain.Core.Exceptions;

namespace InfraFlow.ExceptionLogs.Handlers;

public abstract class ExceptionHandler
{
    public abstract Task HandleException(AppAuthenticationFailedException exception, Guid? correlationId = null);
    public abstract Task HandleException(AppUnauthorizedAccessException exception, Guid? correlationId = null);
    public abstract Task HandleException(AppEntityNotFoundException exception, Guid? correlationId = null);
    public abstract Task HandleException(AppBusinessException exception, Guid? correlationId = null);
    public abstract Task HandleException(AppValidationException exception, Guid? correlationId = null);
    public abstract Task HandleException(Exception exception, Guid? correlationId = null);
}