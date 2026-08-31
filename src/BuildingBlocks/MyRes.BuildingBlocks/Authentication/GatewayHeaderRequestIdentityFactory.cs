using Microsoft.AspNetCore.Http;

namespace MyRes.BuildingBlocks.Authentication
{
    public class GatewayHeaderRequestIdentityFactory
    {
        public IRequestIdentity Create(HttpContext context)
        {
            var headers = context.Request.Headers;

            Guid? userId = null;

            if (Guid.TryParse(headers[GatewayHeaders.UserId], out var parsed))
            {
                userId = parsed;
            }

            var clientId = headers[GatewayHeaders.ClientId].FirstOrDefault();

            var principalType = !string.IsNullOrWhiteSpace(clientId) ? PrincipalType.Client : userId.HasValue ? PrincipalType.User : PrincipalType.Anonymous;

            return new RequestIdentity(
                            IsAuthenticated: principalType != PrincipalType.Anonymous,
                            PrincipalType: principalType,
                            UserId: userId,
                            Username: headers[GatewayHeaders.Username].FirstOrDefault(),
                            Email: headers[GatewayHeaders.Email].FirstOrDefault(),
                            ClientId: clientId);

        }
    }
}
