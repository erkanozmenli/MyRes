using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRes.BuildingBlocks.Infrastructure.Data.Seed;
using MyRes.BuildingBlocks.Utilities;

namespace MyRes.BuildingBlocks.Infrastructure.Data
{
    public static class Extensions
    {
        public static async Task MigrateDatabaseAsync<TContext>(this IServiceProvider services) where TContext : DbContext
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            var retryPolicy = RetryPolicies.Exponential(
                    3,
                    1000,
                    typeof(SqlException),
                    typeof(TimeoutException)
                );

            await retryPolicy.ExecuteAsync(async () =>
            {
                await context.Database.MigrateAsync();
            });
        }

        public static async Task SeedDataAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var seeder = scope.ServiceProvider.GetService<IDataSeeder>();

            var retryPolicy = RetryPolicies.Exponential(
                    3,
                    1000,
                    typeof(SqlException),
                    typeof(TimeoutException)
                );

            await retryPolicy.ExecuteAsync(async () =>
            {
                await seeder!.SeedAllAsync();
            });
        }
    }
}
