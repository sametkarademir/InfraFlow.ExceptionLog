using InfraFlow.ExceptionManagement.Abstractions.Enums;

namespace InfraFlow.ExceptionManagement.Abstractions.Models;

/// <summary>
/// Exception log kaydı
/// </summary>
public class ExceptionLog
{
    /// <summary>
    /// Benzersiz ID
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
        
    /// <summary>
    /// Log kaydı zamanı
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        
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
    /// Hatanın kaynağı (Controller/Service adı vb.)
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
}