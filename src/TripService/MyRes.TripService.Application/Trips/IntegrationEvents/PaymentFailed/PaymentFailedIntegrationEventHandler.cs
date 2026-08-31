using MassTransit;
using Microsoft.Extensions.Logging;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Payment;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Trip;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;

namespace MyRes.TripService.Application.Trips.IntegrationEvents.PaymentFailed
{
    public class PaymentFailedIntegrationEventHandler
        (ITripRepository tripRepository, IPublishEndpoint publishEndpoint, ILogger<PaymentFailedIntegrationEventHandler> logger) : IConsumer<PaymentFailedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<PaymentFailedIntegrationEvent> context)
        {
            logger.LogInformation("Consumed {EventName} for Trip {TripId}", nameof(PaymentFailedIntegrationEvent), context.Message.TripId);

            var trip = await tripRepository.GetByGuidIdAsync(context.Message.TripId);

            if (trip is null)
                throw new TripNotFoundException(context.Message.TripId);

            trip.FailPayment();

            await publishEndpoint.Publish(new TripFailedIntegrationEvent(trip.Id, trip.UserId, context.Message.Message), context.CancellationToken);

            await tripRepository.SaveChangesAsync();
        }
    }
}
