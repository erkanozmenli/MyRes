using MassTransit;
using Microsoft.Extensions.Logging;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Payment;


namespace MyRes.PaymentService.Application.Payments.IntegrationEvents.PaymentRequested
{
    public class PaymentRequestedIntegrationEventHandler
        (IPublishEndpoint publishEndpoint, ILogger<PaymentRequestedIntegrationEventHandler> logger) : IConsumer<PaymentRequestedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<PaymentRequestedIntegrationEvent> context)
        {
            var success = Random.Shared.Next(100) >= 30; // %70 successful

            if (success)
            {
                await publishEndpoint.Publish(
                    new PaymentSucceededIntegrationEvent(context.Message.TripId, context.Message.UserId),
                    context.CancellationToken);
            }
            else
            {
                var fakeFailMessage = "This is a failure message of a fake payment transaction for workflow demonstration.";

                await publishEndpoint.Publish(
                    new PaymentFailedIntegrationEvent(context.Message.TripId, context.Message.UserId, fakeFailMessage),
                    context.CancellationToken);

                logger.LogInformation("FailMessage: {msg}", fakeFailMessage);
            }
        }
    }
}
