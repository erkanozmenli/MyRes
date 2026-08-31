using MassTransit;
using MyRes.BuildingBlocks.Authentication;

namespace MyRes.BuildingBlocks.Messaging.Filters
{
    public class IdentityPublishFilter<T> : IFilter<PublishContext<T>> where T : class
    {
        private readonly ICurrentIdentityAccessor _accessor;

        public IdentityPublishFilter(ICurrentIdentityAccessor accessor)
        {
            _accessor = accessor;
        }

        public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
        {
            if (_accessor.Identity.IsAuthenticated)
            {
                context.Headers.Set(GatewayHeaders.PrincipalType, _accessor.Identity.PrincipalType.ToString());
                context.Headers.Set(GatewayHeaders.UserId, _accessor.Identity.UserId);
                context.Headers.Set(GatewayHeaders.Username, _accessor.Identity.Username);
                context.Headers.Set(GatewayHeaders.Email, _accessor.Identity.Email);
                context.Headers.Set(GatewayHeaders.ClientId, _accessor.Identity.ClientId);
            }

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("identity");
        }
    }
}
