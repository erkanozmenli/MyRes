using Microsoft.Extensions.DependencyInjection;

namespace MyRes.BuildingBlocks.Authentication
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddGatewayIdentity(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<GatewayHeaderRequestIdentityFactory>();
            services.AddSingleton<MassTransitHeaderRequestIdentityFactory>();
            services.AddScoped<ICurrentIdentityAccessor, CurrentIdentityAccessor>();

            return services;
        }
    }
}
