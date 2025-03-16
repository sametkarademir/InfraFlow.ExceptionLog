using InfraFlow.ExceptionManagement.EntityFramework.Entities;
using InfraFlow.ExceptionManagement.EntityFramework.Mappings;
using Microsoft.EntityFrameworkCore;

namespace InfraFlow.ExceptionManagement.EntityFramework.Contexts;

/// <summary>
/// Exception log kaydı için Entity Framework DbContext
/// </summary>
public class ExceptionLogDbContext : DbContext
{
    /// <summary>
    /// Constructor
    /// </summary>
    public ExceptionLogDbContext(DbContextOptions<ExceptionLogDbContext> options)
        : base(options)
    {
    }
        
    /// <summary>
    /// Exception log kayıtları
    /// </summary>
    public DbSet<ExceptionLogEntity> ExceptionLogs { get; set; }
        
    /// <summary>
    /// Model konfigürasyonu
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        // ExceptionLogEntity konfigürasyonunu uygula
        modelBuilder.ApplyConfiguration(new ExceptionLogEntityTypeConfiguration());
    }
}