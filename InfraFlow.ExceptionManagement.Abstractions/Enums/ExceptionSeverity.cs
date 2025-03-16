namespace InfraFlow.ExceptionManagement.Abstractions.Enums;

/// <summary>
/// Exception'ın önem derecesini belirtir
/// </summary>
public enum ExceptionSeverity
{
    /// <summary>
    /// Bilgi verici hata, işlemleri engellemez
    /// </summary>
    Information = 0,
        
    /// <summary>
    /// Uyarı seviyesinde hata, bazı işlemleri engelleyebilir
    /// </summary>
    Warning = 1,
        
    /// <summary>
    /// Ciddi hata, işlem başarısız oldu
    /// </summary>
    Error = 2,
        
    /// <summary>
    /// Kritik hata, uygulamanın çalışmasını etkileyebilir
    /// </summary>
    Critical = 3
}