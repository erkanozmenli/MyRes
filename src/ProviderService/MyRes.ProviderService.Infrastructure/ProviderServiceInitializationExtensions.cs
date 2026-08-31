using Microsoft.AspNetCore.Builder;
using MyRes.BuildingBlocks.Infrastructure.Data;
using MyRes.ProviderService.Infrastructure.Data.Persistence.Contexts;

namespace MyRes.ProviderService.Infrastructure
{
    public static class ProviderServiceInitializationExtensions
    {
        public static async Task<IApplicationBuilder> UseProviderServiceInitialization(this IApplicationBuilder app)
        {
            await app.ApplicationServices.MigrateDatabaseAsync<ProviderDbContext>();

            // 2...

            // 3...

            return app;
        }
    }
}
