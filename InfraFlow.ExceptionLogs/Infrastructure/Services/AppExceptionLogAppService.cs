using AutoMapper;
using InfraFlow.EntityFramework.Core.Extensions;
using InfraFlow.EntityFramework.Core.Models;
using InfraFlow.ExceptionLogs.Domain.Entities;
using InfraFlow.ExceptionLogs.Infrastructure.DTOs.AppExceptionLogs;
using InfraFlow.ExceptionLogs.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InfraFlow.ExceptionLogs.Infrastructure.Services;

public class AppExceptionLogAppService(IAppExceptionLogRepository appExceptionLogRepository, IMapper mapper) : IAppExceptionLogAppService
{
    public async Task<AppExceptionLogResponseDto> GetAppExceptionLogByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var appExceptionLog = await appExceptionLogRepository.GetByIdAsync(id, cancellationToken: cancellationToken);

        return mapper.Map<AppExceptionLogResponseDto>(appExceptionLog);
    }

    public async Task<Paginate<AppExceptionLogResponseDto>> GetAppExceptionLogsFilteredAndPaginatedAsync(GetListAppExceptionLogRequestDto request, CancellationToken cancellationToken = default)
    {
        IQueryable<AppExceptionLog> queryable = appExceptionLogRepository.Query();

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

        if (request.StartDate.HasValue)
        {
            queryable = queryable.Where(item => item.CreationTime.Date >= request.StartDate);
        }
        
        if (request.EndDate.HasValue)
        {
            queryable = queryable.Where(item => item.CreationTime.Date <= request.EndDate);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(item =>
                item.Type!.Contains(request.Search) ||
                item.Message!.Contains(request.Search)
            );
        }

        if (request.Sorts != null)
        {
            queryable = queryable.ToSort(request.Sorts);
        }
        else
        {
            queryable = queryable.OrderByDescending(item => item.CreationTime);
        }

        var appExceptionLogs = await queryable
            .ToPaginateAsync(request.PageIndex, request.PageSize, cancellationToken);

        return mapper.Map<Paginate<AppExceptionLogResponseDto>>(appExceptionLogs);
    }

    public async Task ClearAllAppExceptionLogsAsync(DateTime? olderThan = null, CancellationToken cancellationToken = default)
    {
        var queryable = appExceptionLogRepository.Query();

        if (olderThan.HasValue)
        {
            queryable = queryable.Where(item => item.CreationTime.Date <= olderThan);
        }

        var appExceptionLogs = await queryable.ToListAsync(cancellationToken: cancellationToken);

        await appExceptionLogRepository.DeleteRangeAsync(appExceptionLogs, true, true);
    }

    public async Task DeleteAppExceptionLogAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var appExceptionLog = await appExceptionLogRepository.GetByIdAsync(id, cancellationToken: cancellationToken);

        await appExceptionLogRepository.DeleteAsync(appExceptionLog, true, true);
    }
}