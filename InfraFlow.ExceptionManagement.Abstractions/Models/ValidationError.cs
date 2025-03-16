namespace InfraFlow.ExceptionManagement.Abstractions.Models;

/// <summary>
/// Bir validasyon hatasını temsil eder
/// </summary>
public class ValidationError
{
    /// <summary>
    /// Hatalı property adı
    /// </summary>
    public string PropertyName { get; set; }
        
    /// <summary>
    /// Hata mesajı
    /// </summary>
    public string ErrorMessage { get; set; }
        
    /// <summary>
    /// Hata kodu (opsiyonel)
    /// </summary>
    public string ErrorCode { get; set; }
        
    /// <summary>
    /// Varsayılan constructor
    /// </summary>
    public ValidationError() { }
        
    /// <summary>
    /// Property adı ve mesaj ile constructor
    /// </summary>
    /// <param name="propertyName">Hatalı property adı</param>
    /// <param name="errorMessage">Hata mesajı</param>
    public ValidationError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }
        
    /// <summary>
    /// Tüm alanları alan constructor
    /// </summary>
    /// <param name="propertyName">Hatalı property adı</param>
    /// <param name="errorMessage">Hata mesajı</param>
    /// <param name="errorCode">Hata kodu</param>
    public ValidationError(string propertyName, string errorMessage, string errorCode)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }
}