using InfraFlow.ExceptionManagement.Abstractions.Enums;

namespace InfraFlow.ExceptionManagement.Abstractions.Exceptions;

/// <summary>
/// Tüm uygulama özel exception'larının temel sınıfı
/// </summary>
public abstract class AppExceptionBase : Exception
{
    /// <summary>
    /// Hatanın benzersiz kodu
    /// </summary>
    public string Code { get; }
        
    /// <summary>
    /// Hata hakkında ek detaylar
    /// </summary>
    public string Detail { get; }
        
    /// <summary>
    /// Hatanın ciddiyet seviyesi
    /// </summary>
    public ExceptionSeverity Severity { get; }
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Hata mesajı</param>
    /// <param name="code">Hata kodu</param>
    /// <param name="detail">Detaylı açıklama</param>
    /// <param name="severity">Ciddiyet seviyesi</param>
    protected AppExceptionBase(
        string message, 
        string code, 
        string detail, 
        ExceptionSeverity severity) 
        : base(message)
    {
        Code = code;
        Detail = detail;
        Severity = severity;
    }
}