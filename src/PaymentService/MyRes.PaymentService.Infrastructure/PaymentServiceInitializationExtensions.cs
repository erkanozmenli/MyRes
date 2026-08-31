using Microsoft.AspNetCore.Builder;
using MyRes.BuildingBlocks.Infrastructure.Data;
using MyRes.PaymentService.Infrastructure.Data.Persistence.Contexts;

namespace MyRes.PaymentService.Infrastructure
{
    public static class PaymentServiceInitializationExtensions
    {
        public static async Task<IApplicationBuilder> UsePaymentServiceInitialization(this IApplicationBuilder app)
        {
            await app.ApplicationServices.MigrateDatabaseAsync<PaymentDbContext>();

            // 2...

            // 3...

            return app;
        }
    }
}
