using InfraFlow.EntityFramework.Core.Repositories;
using InfraFlow.ExceptionLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfraFlow.ExceptionLogs.Infrastructure.Repositories;

public class AppExceptionLogRepository<TContext> : EfRepositoryBase<AppExceptionLog, Guid, TContext>, IAppExceptionLogRepository
    where TContext : DbContext
{
    public AppExceptionLogRepository(TContext dbContext) : base(dbContext)
    {
    }
}