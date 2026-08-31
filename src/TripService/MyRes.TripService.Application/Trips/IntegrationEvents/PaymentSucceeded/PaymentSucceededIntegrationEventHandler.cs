using MassTransit;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Booking;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Payment;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;


namespace MyRes.TripService.Application.Trips.IntegrationEvents.PaymentSucceeded
{
    public class PaymentSucceededIntegrationEventHandler
        (ITripRepository tripRepository, IPublishEndpoint publishEndpoint) : IConsumer<PaymentSucceededIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<PaymentSucceededIntegrationEvent> context)
        {
            var trip = await tripRepository.GetByGuidIdAsync(context.Message.TripId);

            if (trip is null)
                throw new TripNotFoundException(context.Message.TripId);

            trip.CompletePayment();

            var integrationEvent = new BookingRequestedIntegrationEvent(trip.Id, trip.UserId);
            await publishEndpoint.Publish(integrationEvent, context.CancellationToken);

            await tripRepository.SaveChangesAsync();
        }
    }
}
