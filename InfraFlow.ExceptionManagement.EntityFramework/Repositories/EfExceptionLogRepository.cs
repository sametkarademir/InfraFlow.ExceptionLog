using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using InfraFlow.ExceptionManagement.Abstractions.Models;
using InfraFlow.ExceptionManagement.EntityFramework.Contexts;
using InfraFlow.ExceptionManagement.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfraFlow.ExceptionManagement.EntityFramework.Repositories;

/// <summary>
/// Entity Framework kullanan exception log repository
/// </summary>
public class EfExceptionLogRepository : IExceptionStorage
{
    private readonly ExceptionLogDbContext _dbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    public EfExceptionLogRepository(ExceptionLogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Exception log kaydeder
    /// </summary>
    public async Task AddExceptionLogAsync(ExceptionLog log)
    {
        if (log == null)
            throw new ArgumentNullException(nameof(log));

        var entity = MapToEntity(log);
        await _dbContext.ExceptionLogs.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// ID'ye göre exception log getirir
    /// </summary>
    public async Task<ExceptionLog> GetExceptionLogByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentException("ID cannot be null or empty", nameof(id));

        var entity = await _dbContext.ExceptionLogs.FindAsync(id);
        return entity != null ? MapToModel(entity) : null;
    }

    /// <summary>
    /// Belirli bir sorguya göre exception logları getirir
    /// </summary>
    public async Task<ExceptionLogResult> GetExceptionLogsAsync(
        int page = 1,
        int pageSize = 20,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string source = null,
        string userId = null)
    {
        // Minimum değerler kontrolü
        page = Math.Max(1, page);
        pageSize = Math.Max(1, pageSize);

        // Sorgu oluştur
        IQueryable<ExceptionLogEntity> query = _dbContext.ExceptionLogs;

        // Filtreleri uygula
        if (startDate.HasValue)
            query = query.Where(l => l.Timestamp >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(l => l.Timestamp <= endDate.Value);

        if (!string.IsNullOrEmpty(source))
            query = query.Where(l => l.Source == source);

        if (!string.IsNullOrEmpty(userId))
            query = query.Where(l => l.UserId == userId);

        // Toplam kayıt sayısı
        var totalCount = await query.CountAsync();

        // Sıralama ve sayfalama
        var entities = await query
            .OrderByDescending(l => l.Timestamp)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Modele dönüştür
        var items = entities.Select(MapToModel);

        // Sonuç nesnesi
        return new ExceptionLogResult
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    /// <summary>
    /// ExceptionLog model nesnesini ExceptionLogEntity'ye dönüştürür
    /// </summary>
    private static ExceptionLogEntity MapToEntity(ExceptionLog model)
    {
        return new ExceptionLogEntity
        {
            Id = string.IsNullOrEmpty(model.Id) ? Guid.NewGuid().ToString() : model.Id,
            Timestamp = model.Timestamp,
            Type = model.Type,
            Message = model.Message,
            StackTrace = model.StackTrace,
            Source = model.Source,
            InnerExceptionMessage = model.InnerExceptionMessage,
            InnerExceptionStackTrace = model.InnerExceptionStackTrace,
            Severity = model.Severity,
            Code = model.Code,
            Detail = model.Detail,
            UserId = model.UserId,
            AdditionalInfo = model.AdditionalInfo,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// ExceptionLogEntity'yi ExceptionLog model nesnesine dönüştürür
    /// </summary>
    private static ExceptionLog MapToModel(ExceptionLogEntity entity)
    {
        return new ExceptionLog
        {
            Id = entity.Id,
            Timestamp = entity.Timestamp,
            Type = entity.Type,
            Message = entity.Message,
            StackTrace = entity.StackTrace,
            Source = entity.Source,
            InnerExceptionMessage = entity.InnerExceptionMessage,
            InnerExceptionStackTrace = entity.InnerExceptionStackTrace,
            Severity = entity.Severity,
            Code = entity.Code,
            Detail = entity.Detail,
            UserId = entity.UserId,
            AdditionalInfo = entity.AdditionalInfo
        };
    }
}