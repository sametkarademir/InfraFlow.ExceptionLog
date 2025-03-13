using InfraFlow.EntityFramework.Core.Models;
using InfraFlow.ExceptionLogs.Infrastructure.DTOs.AppExceptionLogs;

namespace InfraFlow.ExceptionLogs.Infrastructure.Services;

public interface IAppExceptionLogAppService
{
    Task<Paginate<AppExceptionLogResponseDto>> GetAppExceptionLogsFilteredAndPaginatedAsync(GetListAppExceptionLogRequestDto request, CancellationToken cancellationToken = default);
}