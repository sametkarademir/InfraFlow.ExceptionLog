using InfraFlow.EntityFramework.Core.Models;
using InfraFlow.ExceptionLogs.Infrastructure.DTOs.AppExceptionLogs;

namespace InfraFlow.ExceptionLogs.Infrastructure.Services;

public interface IAppExceptionLogAppService
{
    Task<AppExceptionLogResponseDto> GetAppExceptionLogByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Paginate<AppExceptionLogResponseDto>> GetAppExceptionLogsFilteredAndPaginatedAsync(GetListAppExceptionLogRequestDto request, CancellationToken cancellationToken = default);
    Task ClearAllAppExceptionLogsAsync(DateTime? olderThan = null, CancellationToken cancellationToken = default);
    Task DeleteAppExceptionLogAsync(Guid id, CancellationToken cancellationToken = default);
}