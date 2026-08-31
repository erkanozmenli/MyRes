using Microsoft.Extensions.DependencyInjection;
using MyRes.PaymentService.Application.Abstractions;
using MyRes.PaymentService.Infrastructure.Data.Persistence.Repositories;

namespace MyRes.PaymentService.Infrastructure.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
