using MassTransit;

namespace MyRes.BuildingBlocks.Authentication
{
    public sealed class MassTransitHeaderRequestIdentityFactory
    {
        public IRequestIdentity Create(Headers headers)
        {
            Guid? userId = null;

            if (headers.TryGetHeader(GatewayHeaders.UserId, out var value) && Guid.TryParse(value?.ToString(), out var parsed))
            {
                userId = parsed;
            }

            var clientId = headers.TryGetHeader(GatewayHeaders.ClientId, out var client) ? client?.ToString() : null;
            var username = headers.TryGetHeader(GatewayHeaders.Username, out var user) ? user?.ToString() : null;
            var email = headers.TryGetHeader(GatewayHeaders.Email, out var mail) ? mail?.ToString() : null;
            var principalType = !string.IsNullOrWhiteSpace(clientId) ? PrincipalType.Client : userId.HasValue ? PrincipalType.User : PrincipalType.Anonymous;

            return new RequestIdentity(
                            IsAuthenticated: principalType != PrincipalType.Anonymous,
                            PrincipalType: principalType,
                            UserId: userId,
                            Username: username,
                            Email: email,
                            ClientId: clientId);
        }
    }
}
