using Microsoft.AspNetCore.Builder;
using MyRes.BuildingBlocks.Infrastructure.Extensions;
using MyRes.ProviderService.Application;
using MyRes.ProviderService.Infrastructure.Data.Persistence.Contexts;
using MyRes.ProviderService.Infrastructure.Extensions;

namespace MyRes.ProviderService.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.AddObservability();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddMessaging<ProviderDbContext>(builder.Configuration, typeof(AssemblyInfo).Assembly);
            builder.Services.AddInfrastructureServices();
            return builder;
        }
    }
}

