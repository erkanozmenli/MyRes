using MassTransit;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Booking;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Trip;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;


namespace MyRes.TripService.Application.Trips.IntegrationEvents.BookingSucceeded
{
    public class BookingSucceededIntegrationEventHandler
        (ITripRepository tripRepository, IPublishEndpoint publishEndpoint) : IConsumer<BookingSucceededIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<BookingSucceededIntegrationEvent> context)
        {
            var trip = await tripRepository.GetByGuidIdAsync(context.Message.TripId);

            if (trip is null)
                throw new TripNotFoundException(context.Message.TripId);

            trip.CompleteBooking();

            var integrationEvent = new TripCompletedIntegrationEvent(trip.Id, trip.UserId);
            await publishEndpoint.Publish(integrationEvent, context.CancellationToken);

            await tripRepository.SaveChangesAsync();
        }
    }
}
