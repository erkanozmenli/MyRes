using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRes.BuildingBlocks.Messaging.Filters;
using System.Net.Sockets;
using System.Reflection;

namespace MyRes.BuildingBlocks.Infrastructure.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMessaging<TDbContext>(this IServiceCollection services, IConfiguration configuration, Assembly consumerAssembly) where TDbContext : DbContext
        {
            var connectionString = configuration.GetConnectionString("rabbitmq") ?? throw new InvalidOperationException("RabbitMQ connection string 'rabbitmq' is not configured.");

            services.AddScoped(typeof(IdentityPublishFilter<>));
            services.AddScoped(typeof(IdentityConsumeFilter<>));

            services.AddMassTransit(x =>
            {
                x.AddConsumers(consumerAssembly);

                x.AddEntityFrameworkOutbox<TDbContext>(o =>
                {
                    o.UseSqlServer();
                    o.UseBusOutbox();
                    o.DuplicateDetectionWindow = TimeSpan.FromMinutes(30);
                });

                x.AddConfigureEndpointsCallback((context, name, cfg) =>
                {
                    cfg.UseEntityFrameworkOutbox<TDbContext>(context);

                    cfg.UseConsumeFilter(typeof(IdentityConsumeFilter<>), context);
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(connectionString));

                    cfg.UsePublishFilter(typeof(IdentityPublishFilter<>), context);

                    cfg.ConfigureEndpoints(context);

                    cfg.UseMessageRetry(r =>
                    {
                        r.Handle<TimeoutException>();
                        r.Handle<SocketException>();
                        r.Handle<HttpRequestException>();
                        r.Interval(3, TimeSpan.FromSeconds(2));
                    });
                });
            });

            return services;
        }
    }
}
