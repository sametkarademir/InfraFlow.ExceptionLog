using InfraFlow.EntityFramework.Core.Repositories;
using InfraFlow.ExceptionLogs.Domain.Entities;

namespace InfraFlow.ExceptionLogs.Infrastructure.Repositories;

public interface IAppExceptionLogRepository : IRepository<AppExceptionLog, Guid>
{

}