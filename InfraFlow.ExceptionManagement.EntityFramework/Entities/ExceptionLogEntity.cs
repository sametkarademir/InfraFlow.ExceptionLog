using InfraFlow.ExceptionManagement.Abstractions.Enums;

namespace InfraFlow.ExceptionManagement.EntityFramework.Entities;

/// <summary>
/// Exception log veritabanı varlığı
/// </summary>
public class ExceptionLogEntity
{
    /// <summary>
    /// Benzersiz tanımlayıcı
    /// </summary>
    public string Id { get; set; }
        
    /// <summary>
    /// Log kaydı zamanı
    /// </summary>
    public DateTime Timestamp { get; set; }
        
    /// <summary>
    /// Exception tipi adı
    /// </summary>
    public string Type { get; set; }
        
    /// <summary>
    /// Hata mesajı
    /// </summary>
    public string Message { get; set; }
        
    /// <summary>
    /// Stack trace bilgisi
    /// </summary>
    public string StackTrace { get; set; }
        
    /// <summary>
    /// Hatanın kaynağı
    /// </summary>
    public string Source { get; set; }
        
    /// <summary>
    /// İç exception mesajı
    /// </summary>
    public string InnerExceptionMessage { get; set; }
        
    /// <summary>
    /// İç exception stack trace
    /// </summary>
    public string InnerExceptionStackTrace { get; set; }
        
    /// <summary>
    /// Hatanın ciddiyeti
    /// </summary>
    public ExceptionSeverity Severity { get; set; }
        
    /// <summary>
    /// Hata kodu
    /// </summary>
    public string Code { get; set; }
        
    /// <summary>
    /// Detaylı açıklama
    /// </summary>
    public string Detail { get; set; }
        
    /// <summary>
    /// İlişkili kullanıcı ID'si
    /// </summary>
    public string UserId { get; set; }
        
    /// <summary>
    /// Ek bilgiler
    /// </summary>
    public string AdditionalInfo { get; set; }
        
    /// <summary>
    /// Kaydın oluşturulma zamanı
    /// </summary>
    public DateTime CreatedAt { get; set; }
}