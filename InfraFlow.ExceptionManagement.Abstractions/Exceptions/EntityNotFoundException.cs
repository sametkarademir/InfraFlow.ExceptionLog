using InfraFlow.ExceptionManagement.Abstractions.Enums;

namespace InfraFlow.ExceptionManagement.Abstractions.Exceptions;

/// <summary>
/// Veritabanında aranılan varlığın bulunamadığı durumlar için kullanılan exception
/// </summary>
public class EntityNotFoundException : AppExceptionBase
{
    /// <summary>
    /// Bulunamayan varlığın tipi
    /// </summary>
    public string EntityType { get; }
        
    /// <summary>
    /// Bulunamayan varlığın ID'si
    /// </summary>
    public object EntityId { get; }
        
    /// <summary>
    /// Sadece mesaj alan constructor
    /// </summary>
    /// <param name="message">Hata mesajı</param>
    public EntityNotFoundException(string message) 
        : base(message, "NOT_FOUND", string.Empty, ExceptionSeverity.Warning)
    {
    }
        
    /// <summary>
    /// Entity tipi ve ID'si ile constructor
    /// </summary>
    /// <param name="entityType">Entity tipi (örn. "Product", "User")</param>
    /// <param name="entityId">Entity ID'si</param>
    public EntityNotFoundException(string entityType, object entityId) 
        : base($"Entity of type {entityType} with id {entityId} was not found.", 
            "NOT_FOUND", string.Empty, ExceptionSeverity.Warning)
    {
        EntityType = entityType;
        EntityId = entityId;
    }
}