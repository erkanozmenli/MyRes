using Microsoft.Extensions.DependencyInjection;
using MyRes.NotificationService.Application.Abstractions;
using MyRes.NotificationService.Infrastructure.Notifications.WebSocket;

namespace MyRes.NotificationService.Infrastructure.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationPublisher, SignalRNotificationPublisher>();
            return services;
        }
    }
}
