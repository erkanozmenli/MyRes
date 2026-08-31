using MyRes.BuildingBlocks.Authentication;
using System.Security.Claims;

namespace MyRes.GatewayService.Api.Authentication
{
    public class ClaimsPrincipalRequestIdentityFactory : IClaimsPrincipalRequestIdentityFactory
    {
        public IRequestIdentity Create(ClaimsPrincipal principal)
        {
            var clientId = principal.FindFirst(CustomClaimTypes.ClientId)?.Value;

            if (principal.Identity?.IsAuthenticated != true)
            {
                return new RequestIdentity(
                    IsAuthenticated: false,
                    PrincipalType: PrincipalType.Anonymous,
                    UserId: null,
                    Username: null,
                    Email: null,
                    ClientId: null);
            }

            // Client Credentials token mı?
            if (!string.IsNullOrWhiteSpace(clientId))
            {
                return new RequestIdentity(
                    IsAuthenticated: true,
                    PrincipalType: PrincipalType.Client,
                    UserId: null,
                    Username: null,
                    Email: null,
                    ClientId: clientId);
            }

            // User token veya Cookie
            Guid? userId = null;

            var sub = principal.FindFirst(CustomClaimTypes.SubjectId)?.Value ?? principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(sub, out var parsed))
            {
                userId = parsed;
            }

            return new RequestIdentity(
                IsAuthenticated: true,
                PrincipalType: PrincipalType.User,
                UserId: userId,
                Username: principal.FindFirst(CustomClaimTypes.PreferredUsername)?.Value ?? principal.Identity?.Name,
                Email: principal.FindFirst(ClaimTypes.Email)?.Value ?? principal.FindFirst("email")?.Value,
                ClientId: null);
        }
    }
}
