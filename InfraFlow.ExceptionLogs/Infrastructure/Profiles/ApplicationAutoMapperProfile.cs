using AutoMapper;
using InfraFlow.Domain.Core.Exceptions;
using InfraFlow.EntityFramework.Core.Models;
using InfraFlow.ExceptionLogs.Infrastructure.DTOs.AppExceptionLogs;

namespace InfraFlow.ExceptionLogs.Infrastructure.Profiles;

public class ApplicationAutoMapperProfile : Profile
{
    public ApplicationAutoMapperProfile()
    {
        CreateMap<Paginate<AppException>, Paginate<AppExceptionLogResponseDto>>().ReverseMap();
        CreateMap<AppException, AppExceptionLogResponseDto>().ReverseMap();
    }
}