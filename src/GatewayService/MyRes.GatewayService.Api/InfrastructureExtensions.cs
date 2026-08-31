using Microsoft.AspNetCore.DataProtection;
using MyRes.BuildingBlocks.Infrastructure.Extensions;
using MyRes.GatewayService.Api.Authentication;
using StackExchange.Redis;

namespace MyRes.GatewayService.Api
{
    public static class InfrastructureExtensions
    {
        public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        {
            var redisConnectionString = builder.Configuration.GetConnectionString("redis") ?? throw new InvalidOperationException("Redis connection string not found.");

            builder.AddObservability();

            var multiplexer = ConnectionMultiplexer.Connect(redisConnectionString);

            builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            builder.Services.AddSingleton<RedisTicketStore>();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            builder.Services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(multiplexer, "gateway-data-protection-keys");

            return builder;
        }
    }
}
