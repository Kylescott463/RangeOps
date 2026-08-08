using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RangeOps.Infrastructure.Persistence;

namespace RangeOps.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string databaseConnectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(databaseConnectionString);

        services.AddDbContext<RangeOpsDbContext>(options =>
            options.UseNpgsql(
                databaseConnectionString,
                npgsqlOptions => npgsqlOptions.MigrationsAssembly(
                    typeof(RangeOpsDbContext).Assembly.FullName)));

        return services;
    }
}
