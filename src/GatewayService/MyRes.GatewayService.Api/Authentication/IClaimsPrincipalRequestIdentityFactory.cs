using MyRes.BuildingBlocks.Authentication;
using System.Security.Claims;

namespace MyRes.GatewayService.Api.Authentication
{
    public interface IClaimsPrincipalRequestIdentityFactory
    {
        IRequestIdentity Create(ClaimsPrincipal principal);
    }
}
