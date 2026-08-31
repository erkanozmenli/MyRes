using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyRes.BuildingBlocks.Infrastructure.Extensions;
using MyRes.NotificationService.Application;
using MyRes.NotificationService.Infrastructure.Data.Persistence.Contexts;
using MyRes.NotificationService.Infrastructure.Extensions;


namespace MyRes.NotificationService.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.AddObservability();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddMessaging<NotificationDbContext>(builder.Configuration, typeof(AssemblyInfo).Assembly);
            builder.Services.AddSignalR(builder.Configuration);
            builder.Services.AddInfrastructureServices();
            return builder;
        }
    }
}
