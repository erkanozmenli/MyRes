using MassTransit;
using MyRes.BuildingBlocks.Authentication;

namespace MyRes.BuildingBlocks.Messaging.Filters
{
    public class IdentityConsumeFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly ICurrentIdentityAccessor _accessor;
        private readonly MassTransitHeaderRequestIdentityFactory _factory;

        public IdentityConsumeFilter(ICurrentIdentityAccessor accessor, MassTransitHeaderRequestIdentityFactory factory)
        {
            _accessor = accessor;
            _factory = factory;
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            _accessor.SetIdentity(_factory.Create(context.Headers));

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("identity");
        }
    }
}
