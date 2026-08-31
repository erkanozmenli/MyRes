using Microsoft.Extensions.DependencyInjection;

namespace MyRes.ProviderService.Infrastructure.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
