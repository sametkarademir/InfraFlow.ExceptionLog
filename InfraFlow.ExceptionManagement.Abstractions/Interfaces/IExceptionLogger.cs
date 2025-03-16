using InfraFlow.ExceptionManagement.Abstractions.Models;

namespace InfraFlow.ExceptionManagement.Abstractions.Interfaces;

/// <summary>
/// Exception loglaması için temel arayüz
/// </summary>
public interface IExceptionLogger
{
    /// <summary>
    /// Exception'ı loglar
    /// </summary>
    /// <param name="exception">Loglanan exception</param>
    /// <param name="source">Exception kaynağı (örn. Controller adı)</param>
    /// <param name="userId">İlişkili kullanıcı ID'si (opsiyonel)</param>
    /// <param name="additionalInfo">Ek bilgiler (opsiyonel)</param>
    /// <returns>Task</returns>
    Task LogExceptionAsync(Exception exception, string source, string userId = null, string additionalInfo = null);
        
    /// <summary>
    /// Belirli bir sorgu kriterine göre exception loglarını getirir
    /// </summary>
    /// <param name="page">Sayfa numarası</param>
    /// <param name="pageSize">Sayfa başına kayıt sayısı</param>
    /// <param name="startDate">Başlangıç tarihi</param>
    /// <param name="endDate">Bitiş tarihi</param>
    /// <param name="source">Kaynak filtresi</param>
    /// <param name="userId">Kullanıcı ID filtresi</param>
    /// <returns>Sayfalanmış exception log sonuçları</returns>
    Task<ExceptionLogResult> GetExceptionLogsAsync(
        int page = 1, 
        int pageSize = 20, 
        DateTime? startDate = null, 
        DateTime? endDate = null, 
        string source = null, 
        string userId = null);
        
    /// <summary>
    /// ID'ye göre exception log kaydını getirir
    /// </summary>
    /// <param name="id">Log ID'si</param>
    /// <returns>Exception log kaydı veya null</returns>
    Task<ExceptionLog> GetExceptionLogByIdAsync(string id);
}