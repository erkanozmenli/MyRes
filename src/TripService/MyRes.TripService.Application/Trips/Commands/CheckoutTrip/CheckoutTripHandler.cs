using MassTransit;
using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.BuildingBlocks.Messaging.IntegrationEvents.Payment;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;

namespace MyRes.TripService.Application.Trips.Commands.CheckoutTrip
{
    internal class CheckoutTripHandler(ITripRepository tripRepository, IPublishEndpoint publishEndpoint)
         : ICommandHandler<CheckoutTripCommand, CheckoutTripResult>
    {
        public async Task<CheckoutTripResult> Handle(CheckoutTripCommand command, CancellationToken cancellationToken)
        {
            var trip = await tripRepository.GetByGuidIdAsync(command.TripId);

            if (trip is null)
                throw new TripNotFoundException(command.TripId);

            trip.StartCheckout();

            var integrationEvent = new PaymentRequestedIntegrationEvent(trip.Id, trip.UserId);

            await publishEndpoint.Publish(integrationEvent, cancellationToken);

            await tripRepository.SaveChangesAsync();

            return new CheckoutTripResult(trip.Id);
        }
    }
}
