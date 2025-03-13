using AutoMapper;
using InfraFlow.EntityFramework.Core.Extensions;
using InfraFlow.EntityFramework.Core.Models;
using InfraFlow.ExceptionLogs.Infrastructure.DTOs.AppExceptionLogs;
using InfraFlow.ExceptionLogs.Infrastructure.Repositories;

namespace InfraFlow.ExceptionLogs.Infrastructure.Services;

public class AppExceptionLogAppService(IAppExceptionLogRepository appExceptionLogRepository, IMapper mapper) : IAppExceptionLogAppService
{
    public async Task<Paginate<AppExceptionLogResponseDto>> GetAppExceptionLogsFilteredAndPaginatedAsync(GetListAppExceptionLogRequestDto request, CancellationToken cancellationToken = default)
    {
        var queryable = appExceptionLogRepository.Query();

        if (request.CorrelationId.HasValue)
        {
            queryable = queryable.Where(item => item.CorrelationId == request.CorrelationId);
        }

        if (request.AppSnapshotId.HasValue)
        {
            queryable = queryable.Where(item => item.AppSnapshotId == request.AppSnapshotId);
        }

        if (request.SessionId.HasValue)
        {
            queryable = queryable.Where(item => item.SessionId == request.SessionId);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(item =>
                item.Type!.Contains(request.Search) ||
                item.Message!.Contains(request.Search)
            );
        }

        var appExceptionLogs = await queryable
            .OrderByDescending(item => item.CreationTime)
            .ToPaginateAsync(request.PageIndex, request.PageSize, cancellationToken);

        return mapper.Map<Paginate<AppExceptionLogResponseDto>>(appExceptionLogs);
    }
}