using InfraFlow.ExceptionManagement.Abstractions.Models;

namespace InfraFlow.ExceptionManagement.Abstractions.Interfaces;

/// <summary>
/// Exception loglarını depolamak ve sorgulamak için arayüz
/// </summary>
public interface IExceptionStorage
{
    /// <summary>
    /// Exception log kaydeder
    /// </summary>
    /// <param name="log">Kaydedilecek log</param>
    /// <returns>Task</returns>
    Task AddExceptionLogAsync(ExceptionLog log);
        
    /// <summary>
    /// Belirli bir sorguya göre exception logları getirir
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
    /// ID'ye göre exception log getirir
    /// </summary>
    /// <param name="id">Log ID'si</param>
    /// <returns>Exception log kaydı veya null</returns>
    Task<ExceptionLog> GetExceptionLogByIdAsync(string id);
}