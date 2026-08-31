using Microsoft.AspNetCore.Builder;

namespace MyRes.BuildingBlocks.Authentication
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGatewayIdentity(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GatewayIdentityMiddleware>();
        }
    }
}
