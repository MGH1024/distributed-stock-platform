using MGH.Core.Domain.Events;
using MGH.Core.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Stock.Domain.LiveStocks;

namespace Stock.Infrastructure.Contexts;

public class LiveStockDbContext(DbContextOptions<LiveStockDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditLog).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LiveStockDbContext).Assembly);
        modelBuilder.Ignore<DomainEvent>();
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(64);
        base.ConfigureConventions(configurationBuilder);
    }

    private DbSet<LiveStock> LiveStocks { get; set; }
    private DbSet<OutboxMessage> OutboxMessages { get; set; }
    private DbSet<AuditLog> AuditLogs { get; set; }
}