using MassTransit;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Refund;

namespace MyRes.PaymentService.Application.Payments.IntegrationEvents.PaymentRequested
{
    public class RefundRequestedIntegrationEventHandler
        (IPublishEndpoint publishEndpoint) : IConsumer<RefundRequestedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<RefundRequestedIntegrationEvent> context)
        {
            await Task.Delay(500); // Fake refund

            var integrationEvent = new PaymentRefundedIntegrationEvent(context.Message.TripId);
            await publishEndpoint.Publish(integrationEvent, context.CancellationToken);
        }
    }
}
