namespace InfraFlow.ExceptionManagement.Core.Enums;

/// <summary>
/// Log hedefleri (bitwise flag olarak kullanılır)
/// </summary>
[Flags]
public enum LogDestination
{
    /// <summary>
    /// Loglama yapılmaz
    /// </summary>
    None = 0,
        
    /// <summary>
    /// Konsola loglama
    /// </summary>
    Console = 1,
        
    /// <summary>
    /// Dosyaya loglama
    /// </summary>
    File = 2,
        
    /// <summary>
    /// Veritabanına loglama
    /// </summary>
    Database = 4
}