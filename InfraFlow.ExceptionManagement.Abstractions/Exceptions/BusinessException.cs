using InfraFlow.ExceptionManagement.Abstractions.Enums;

namespace InfraFlow.ExceptionManagement.Abstractions.Exceptions;

/// <summary>
/// İş kuralı ihlalleri için kullanılan exception
/// </summary>
public class BusinessException : AppExceptionBase
{
    /// <summary>
    /// Temel constructor
    /// </summary>
    /// <param name="message">Hata mesajı</param>
    /// <param name="code">Hata kodu (Opsiyonel)</param>
    public BusinessException(string message, string code = "BUSINESS_ERROR") 
        : base(message, code, string.Empty, ExceptionSeverity.Warning)
    {
    }
        
    /// <summary>
    /// Tüm detayları alan constructor
    /// </summary>
    /// <param name="message">Hata mesajı</param>
    /// <param name="code">Hata kodu</param>
    /// <param name="detail">Detaylı açıklama</param>
    public BusinessException(string message, string code, string detail) 
        : base(message, code, detail, ExceptionSeverity.Warning)
    {
    }
}