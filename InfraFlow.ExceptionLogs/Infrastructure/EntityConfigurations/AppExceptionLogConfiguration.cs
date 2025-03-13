using InfraFlow.EntityFramework.Core.Enums;
using InfraFlow.EntityFramework.Core.Extensions;
using InfraFlow.ExceptionLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraFlow.ExceptionLogs.Infrastructure.EntityConfigurations;

public class AppExceptionLogConfiguration(DatabaseProviderTypes databaseProviderType) : IEntityTypeConfiguration<AppExceptionLog>
{
    public void Configure(EntityTypeBuilder<AppExceptionLog> builder)
    {
        builder.ApplyGlobalEntityConfigurations(databaseProviderType);

        builder.ToTable("AppExceptionLogs");

        builder.Property(item => item.Type).HasMaxLength(500).IsRequired(false);
        builder.Property(item => item.Message).IsRequired(false);
        builder.Property(item => item.Source).IsRequired(false);
        builder.Property(item => item.StackTrace).IsRequired(false);
        builder.Property(item => item.InnerException).IsRequired(false);
    }
}