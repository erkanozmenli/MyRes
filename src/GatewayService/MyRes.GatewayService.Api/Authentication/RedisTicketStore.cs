using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StackExchange.Redis;

namespace MyRes.GatewayService.Api.Authentication;

public class RedisTicketStore : ITicketStore
{
    private const string KeyPrefix = "bff-auth-ticket:";
    private readonly IConnectionMultiplexer _redis;

    public RedisTicketStore(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task RemoveAsync(string key)
    {

    }

    public async Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        var data = TicketSerializer.Default.Serialize(ticket);
        var db = _redis.GetDatabase();
        await db.StringSetAsync(KeyPrefix + key, data, ticket.Properties.ExpiresUtc - DateTimeOffset.UtcNow);
    }

    public async Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        var db = _redis.GetDatabase();
        var data = await db.StringGetAsync(KeyPrefix + key);

        if (data.IsNullOrEmpty)
        {
            return null;
        }

        return TicketSerializer.Default.Deserialize(data!);
    }

    public async Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var key = Guid.NewGuid().ToString("N");
        var data = TicketSerializer.Default.Serialize(ticket);
        var db = _redis.GetDatabase();
        await db.StringSetAsync(KeyPrefix + key, data, ticket.Properties.ExpiresUtc - DateTimeOffset.UtcNow);

        return key;
    }
}
