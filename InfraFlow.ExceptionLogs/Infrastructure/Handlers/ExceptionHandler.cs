using InfraFlow.Domain.Core.Exceptions;

namespace InfraFlow.ExceptionLogs.Infrastructure.Handlers;

public abstract class ExceptionHandler
{
    public abstract Task HandleException(AppAuthenticationFailedException exception);
    public abstract Task HandleException(AppUnauthorizedAccessException exception);
    public abstract Task HandleException(AppEntityNotFoundException exception);
    public abstract Task HandleException(AppBusinessException exception);
    public abstract Task HandleException(AppValidationException exception);
    public abstract Task HandleException(Exception exception);
}