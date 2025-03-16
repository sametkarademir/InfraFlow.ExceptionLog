using InfraFlow.ExceptionManagement.Abstractions.Enums;

namespace InfraFlow.ExceptionManagement.Abstractions.Models;

/// <summary>
/// Exception log sorgulama kriterleri
/// </summary>
public class ExceptionQuery
{
    /// <summary>
    /// Başlangıç tarihi
    /// </summary>
    public DateTime? FromDate { get; set; }
        
    /// <summary>
    /// Bitiş tarihi
    /// </summary>
    public DateTime? ToDate { get; set; }
        
    /// <summary>
    /// Kaynak filtresi (Controller/Service adı vb.)
    /// </summary>
    public string Source { get; set; }
        
    /// <summary>
    /// Kullanıcı ID filtresi
    /// </summary>
    public string UserId { get; set; }
        
    /// <summary>
    /// Exception tipi filtresi
    /// </summary>
    public string ExceptionType { get; set; }
        
    /// <summary>
    /// Ciddiyet seviyesi filtresi
    /// </summary>
    public ExceptionSeverity? Severity { get; set; }
        
    /// <summary>
    /// İçerik arama terimi
    /// </summary>
    public string SearchTerm { get; set; }
        
    /// <summary>
    /// Sonuçları azalan şekilde sırala
    /// </summary>
    public bool SortDescending { get; set; } = true;
        
    /// <summary>
    /// Maksimum sonuç sayısı
    /// </summary>
    public int MaxResults { get; set; } = 100;
}