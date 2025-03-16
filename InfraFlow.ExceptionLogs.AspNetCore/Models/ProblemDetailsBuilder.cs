using InfraFlow.ExceptionManagement.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfraFlow.ExceptionLogs.AspNetCore.Models;

public static class ProblemDetailsBuilder
{
    /// <summary>
    /// Exception'dan ProblemDetails nesnesi oluşturur
    /// </summary>
    public static ProblemDetails CreateFromException(
        Exception exception, 
        int statusCode = StatusCodes.Status500InternalServerError,
        string instance = null)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = GetTitle(exception),
            Detail = GetDetail(exception),
            Instance = instance,
            Type = GetType(exception, statusCode)
        };

        // Dictionary'yi açıkça çıkar
        var extensions = problemDetails.Extensions;

        // Özel exception tiplerini işle
        if (exception is AppExceptionBase appException)
        {
            extensions["code"] = appException.Code;
            extensions["severity"] = appException.Severity.ToString();
        }

        // ValidationException özel işleme
        if (exception is ValidationException validationException)
        {
            extensions["errors"] = validationException.Errors;
        }

        // EntityNotFoundException özel işleme
        if (exception is EntityNotFoundException notFoundException)
        {
            extensions["entityType"] = notFoundException.EntityType;
            extensions["entityId"] = notFoundException.EntityId;
        }

        return problemDetails;
    }

    /// <summary>
    /// Exception tipine göre başlık döndürür
    /// </summary>
    private static string GetTitle(Exception exception)
    {
        return exception switch
        {
            ValidationException => "Validation Error",
            BusinessException => "Business Rule Violation",
            EntityNotFoundException => "Resource Not Found",
            UnauthorizedAccessException => "Unauthorized",
            _ => "An error occurred"
        };
    }

    /// <summary>
    /// Exception tipine göre detay döndürür
    /// </summary>
    private static string GetDetail(Exception exception)
    {
        // Geliştirme dışı ortamlarda hassas hata mesajlarını gizleyebiliriz
        return exception.Message;
    }

    /// <summary>
    /// Exception tipine göre problem tipini (URI) döndürür
    /// </summary>
    private static string GetType(Exception exception, int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => "https://infraflow.org/errors/bad-request",
            StatusCodes.Status401Unauthorized => "https://infraflow.org/errors/unauthorized",
            StatusCodes.Status403Forbidden => "https://infraflow.org/errors/forbidden",
            StatusCodes.Status404NotFound => "https://infraflow.org/errors/not-found",
            StatusCodes.Status422UnprocessableEntity => "https://infraflow.org/errors/validation",
            _ => "https://infraflow.org/errors/server-error"
        };
    }
}