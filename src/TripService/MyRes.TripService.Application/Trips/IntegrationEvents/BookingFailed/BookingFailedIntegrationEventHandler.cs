using MassTransit;
using Microsoft.Extensions.Logging;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Booking;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Refund;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Trip;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;


namespace MyRes.TripService.Application.Trips.IntegrationEvents.BookingFailed
{
    public class BookingFailedIntegrationEventHandler
        (ITripRepository tripRepository, IPublishEndpoint publishEndpoint, ILogger<BookingFailedIntegrationEventHandler> logger) : IConsumer<BookingFailedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<BookingFailedIntegrationEvent> context)
        {
            logger.LogInformation("Consumed {EventName} for Trip {TripId}", nameof(BookingFailedIntegrationEvent), context.Message.TripId);

            var trip = await tripRepository.GetByGuidIdAsync(context.Message.TripId);

            if (trip is null)
                throw new TripNotFoundException(context.Message.TripId);

            trip.FailBooking();

            await publishEndpoint.Publish(new RefundRequestedIntegrationEvent(trip.Id), context.CancellationToken);
            await publishEndpoint.Publish(new TripFailedIntegrationEvent(trip.Id, trip.UserId, context.Message.Message), context.CancellationToken);

            await tripRepository.SaveChangesAsync();
        }
    }
}
