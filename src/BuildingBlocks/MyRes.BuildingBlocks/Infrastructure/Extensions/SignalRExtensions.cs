using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MyRes.BuildingBlocks.Infrastructure.Extensions
{
    public static class SignalRExtensions
    {
        public static IServiceCollection AddSignalR(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetConnectionString("redis") ?? throw new InvalidOperationException("Redis connection string not found.");
            services.AddSignalR().AddStackExchangeRedis(redisConnectionString, options =>
            {
                var serviceName = configuration["Service:Name"]!;
                options.Configuration.ChannelPrefix = RedisChannel.Literal(serviceName);
            });

            return services;
        }
    }
}
