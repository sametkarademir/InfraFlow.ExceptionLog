using InfraFlow.ExceptionManagement.Abstractions.Enums;
using InfraFlow.ExceptionManagement.Abstractions.Models;

namespace InfraFlow.ExceptionManagement.Abstractions.Exceptions;

/// <summary>
/// Giriş verisi doğrulama hataları için kullanılan exception
/// </summary>
public class ValidationException : AppExceptionBase
{
    /// <summary>
    /// Validasyon hatalarının listesi
    /// </summary>
    public IReadOnlyCollection<ValidationError> Errors { get; }
        
    /// <summary>
    /// Hata listesi alan constructor
    /// </summary>
    /// <param name="errors">Validasyon hataları listesi</param>
    public ValidationException(IEnumerable<ValidationError> errors) 
        : base("Validation failed", "VALIDATION_ERROR", string.Empty, 
            ExceptionSeverity.Warning)
    {
        // Null kontrolü yapıp listeyi saklama
        Errors = errors?.ToList() ?? new List<ValidationError>();
    }
        
    /// <summary>
    /// Tek bir hata için constructor
    /// </summary>
    /// <param name="propertyName">Hatalı property adı</param>
    /// <param name="errorMessage">Hata mesajı</param>
    public ValidationException(string propertyName, string errorMessage)
        : base("Validation failed", "VALIDATION_ERROR", string.Empty, 
            ExceptionSeverity.Warning)
    {
        Errors = new List<ValidationError> 
        { 
            new ValidationError(propertyName, errorMessage)
        };
    }
}