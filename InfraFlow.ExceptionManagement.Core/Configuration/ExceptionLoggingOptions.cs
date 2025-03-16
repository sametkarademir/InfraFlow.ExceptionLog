using InfraFlow.ExceptionManagement.Core.Enums;

namespace InfraFlow.ExceptionManagement.Core.Configuration;

/// <summary>
/// Exception loglama ayarları
/// </summary>
public class ExceptionLoggingOptions
{
    /// <summary>
    /// Exception stack trace'in log kayıtlarına dahil edilip edilmeyeceği
    /// </summary>
    public bool IncludeFullStackTrace { get; set; } = true;
        
    /// <summary>
    /// İç exception'ların log kayıtlarına dahil edilip edilmeyeceği
    /// </summary>
    public bool CaptureInnerExceptions { get; set; } = true;
        
    /// <summary>
    /// Logların aynı zamanda standart logger'a da yazılıp yazılmayacağı
    /// </summary>
    public bool LogToStandardLogger { get; set; } = true;
        
    /// <summary>
    /// Log kaydında yer alacak maksimum ek bilgi uzunluğu
    /// </summary>
    public int MaxAdditionalInfoLength { get; set; } = 4000;
        
    /// <summary>
    /// Loglama hedefleri (bitwise flag)
    /// </summary>
    public LogDestination LogDestinations { get; set; } = LogDestination.Console;
        
    /// <summary>
    /// Dosya loglaması için klasör yolu
    /// </summary>
    public string LogFolder { get; set; } = "Logs";
}