using Microsoft.AspNetCore.Builder;
using MyRes.TripService.Infrastructure.Extensions;
using MyRes.TripService.Infrastructure.Data.Persistence.Contexts;
using MyRes.TripService.Application;
using MyRes.BuildingBlocks.Infrastructure.Extensions;

namespace MyRes.TripService.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.AddObservability();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddMessaging<TripDbContext>(builder.Configuration, typeof(AssemblyInfo).Assembly);
            builder.Services.AddInfrastructureServices();

            return builder;
        }
    }
}
