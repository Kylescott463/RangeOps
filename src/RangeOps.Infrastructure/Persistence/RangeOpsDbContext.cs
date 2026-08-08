using Microsoft.EntityFrameworkCore;

namespace RangeOps.Infrastructure.Persistence;

public sealed class RangeOpsDbContext(DbContextOptions<RangeOpsDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RangeOpsDbContext).Assembly);
    }
}
