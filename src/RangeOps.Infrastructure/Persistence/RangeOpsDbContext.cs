using Microsoft.EntityFrameworkCore;

namespace RangeOps.Infrastructure.Persistence;

public sealed class RangeOpsDbContext(DbContextOptions<RangeOpsDbContext> options)
    : DbContext(options)
{
}
