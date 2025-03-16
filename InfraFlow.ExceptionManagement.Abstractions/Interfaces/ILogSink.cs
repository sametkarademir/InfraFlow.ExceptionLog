using InfraFlow.ExceptionManagement.Abstractions.Models;

namespace InfraFlow.ExceptionManagement.Abstractions.Interfaces;

/// <summary>
/// Log yazma hedefi (sink) arayüzü (konsol, dosya, veritabanı vb.)
/// </summary>
public interface ILogSink
{
    /// <summary>
    /// Exception logunu hedef çıktıya yazar
    /// </summary>
    /// <param name="log">Yazılacak log kaydı</param>
    /// <returns>Task</returns>
    Task WriteAsync(ExceptionLog log);
}