using Microsoft.AspNetCore.Builder;
using MyRes.BuildingBlocks.Infrastructure.Extensions;
using MyRes.PaymentService.Application;
using MyRes.PaymentService.Infrastructure.Data.Persistence.Contexts;
using MyRes.PaymentService.Infrastructure.Extensions;


namespace MyRes.PaymentService.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.AddObservability();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddMessaging<PaymentDbContext>(builder.Configuration, typeof(AssemblyInfo).Assembly);
            builder.Services.AddInfrastructureServices();
            return builder;
        }
    }
}
