using Microsoft.AspNetCore.Builder;
using MyRes.BuildingBlocks.Infrastructure.Data;
using MyRes.NotificationService.Infrastructure.Data.Persistence.Contexts;

namespace MyRes.NotificationService.Infrastructure
{
    public static class NotificationServiceInitializationExtensions
    {
        public static async Task<IApplicationBuilder> UseNotificationServiceInitialization(this IApplicationBuilder app)
        {
            await app.ApplicationServices.MigrateDatabaseAsync<NotificationDbContext>();

            // 2...

            // 3...

            return app;
        }
    }
}
