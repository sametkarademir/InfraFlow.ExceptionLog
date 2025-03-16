namespace InfraFlow.ExceptionManagement.Abstractions.Models;

/// <summary>
/// Sayfalanmış exception log sonuçları
/// </summary>
public class ExceptionLogResult
{
    /// <summary>
    /// Log kayıtları
    /// </summary>
    public IEnumerable<ExceptionLog> Items { get; set; } = new List<ExceptionLog>();
        
    /// <summary>
    /// Toplam kayıt sayısı
    /// </summary>
    public int TotalCount { get; set; }
        
    /// <summary>
    /// Mevcut sayfa numarası
    /// </summary>
    public int Page { get; set; }
        
    /// <summary>
    /// Sayfa başına kayıt sayısı
    /// </summary>
    public int PageSize { get; set; }
        
    /// <summary>
    /// Toplam sayfa sayısı
    /// </summary>
    public int TotalPages { get; set; }
}