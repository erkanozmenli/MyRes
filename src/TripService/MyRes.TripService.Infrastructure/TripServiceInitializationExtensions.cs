using Microsoft.AspNetCore.Builder;
using MyRes.BuildingBlocks.Infrastructure.Data;
using MyRes.TripService.Infrastructure.Data.Persistence.Contexts;


namespace MyRes.TripService.Infrastructure
{
    public static class TripServiceInitializationExtensions
    {
        public static async Task<IApplicationBuilder> UseTripServiceInitialization(this IApplicationBuilder app)
        {
            await app.ApplicationServices.MigrateDatabaseAsync<TripDbContext>();
            await app.ApplicationServices.SeedDataAsync();

            // 2...

            // 3...

            return app;
        }
    }
}
