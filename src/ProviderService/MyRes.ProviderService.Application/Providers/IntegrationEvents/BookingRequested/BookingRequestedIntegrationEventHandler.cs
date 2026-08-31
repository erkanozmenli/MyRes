using MassTransit;
using Microsoft.Extensions.Logging;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Booking;

namespace MyRes.ProviderService.Application.Providers.IntegrationEvents.BookingRequested
{
    public class BookingRequestedIntegrationEventHandler
        (IPublishEndpoint publishEndpoint, ILogger<BookingRequestedIntegrationEventHandler> logger) : IConsumer<BookingRequestedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<BookingRequestedIntegrationEvent> context)
        {
            var success = Random.Shared.Next(100) >= 20; // %80 successful

            if (success)
            {
                await publishEndpoint.Publish(
                    new BookingSucceededIntegrationEvent(context.Message.TripId, context.Message.UserId),
                    context.CancellationToken);
            }
            else
            {
                var fakeFailMessage = "This is a failure message of a fake provider transaction for workflow demonstration.";

                await publishEndpoint.Publish(
                    new BookingFailedIntegrationEvent(context.Message.TripId, context.Message.UserId, fakeFailMessage),
                    context.CancellationToken);

                logger.LogInformation("FailMessage: {msg}", fakeFailMessage);
            }
        }
    }
}
