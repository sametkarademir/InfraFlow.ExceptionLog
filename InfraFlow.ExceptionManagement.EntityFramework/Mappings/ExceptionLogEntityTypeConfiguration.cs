using InfraFlow.ExceptionManagement.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraFlow.ExceptionManagement.EntityFramework.Mappings;

/// <summary>
    /// ExceptionLogEntity için Entity Framework konfigürasyonu
    /// </summary>
    public class ExceptionLogEntityTypeConfiguration : IEntityTypeConfiguration<ExceptionLogEntity>
    {
        /// <summary>
        /// Entity konfigürasyonunu uygular
        /// </summary>
        public void Configure(EntityTypeBuilder<ExceptionLogEntity> builder)
        {
            builder.ToTable("ExceptionLogs");
            
            // Primary key
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasMaxLength(50);
            
            // Timestamp
            builder.Property(e => e.Timestamp).IsRequired();
            
            // Temel alanlar
            builder.Property(e => e.Type).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Message).IsRequired().HasMaxLength(2000);
            builder.Property(e => e.Source).HasMaxLength(255);
            builder.Property(e => e.Code).HasMaxLength(100);
            builder.Property(e => e.Detail).HasMaxLength(2000);
            builder.Property(e => e.UserId).HasMaxLength(100);
            
            // İç exception
            builder.Property(e => e.InnerExceptionMessage).HasMaxLength(2000);
            
            // Büyük metin alanları
            builder.Property(e => e.StackTrace).HasColumnType("nvarchar(max)");
            builder.Property(e => e.InnerExceptionStackTrace).HasColumnType("nvarchar(max)");
            builder.Property(e => e.AdditionalInfo).HasColumnType("nvarchar(max)");
            
            // Ciddiyet seviyesi
            builder.Property(e => e.Severity).IsRequired();
            
            // Oluşturulma zamanı
            builder.Property(e => e.CreatedAt).IsRequired();
            
            // İndeksler
            builder.HasIndex(e => e.Timestamp);
            builder.HasIndex(e => e.Type);
            builder.HasIndex(e => e.Severity);
            builder.HasIndex(e => e.UserId);
            builder.HasIndex(e => e.Source);
        }
    }