using Microsoft.AspNetCore.Http;

namespace MyRes.BuildingBlocks.Authentication
{
    public sealed class GatewayIdentityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GatewayHeaderRequestIdentityFactory _factory;

        public GatewayIdentityMiddleware(RequestDelegate next, GatewayHeaderRequestIdentityFactory factory)
        {
            _next = next;
            _factory = factory;
        }

        public async Task InvokeAsync(HttpContext context, ICurrentIdentityAccessor accessor)
        {
            accessor.SetIdentity(_factory.Create(context));

            await _next(context);
        }
    }
}
