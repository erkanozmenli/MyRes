using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Exceptions;

namespace MyRes.TripService.Application.Trips.Commands.AddFlightReservation
{
    internal class AddFlightReservationHandler
        (ITripRepository tripRepository) : ICommandHandler<AddFlightReservationCommand, AddFlightReservationResult>
    {
        public async Task<AddFlightReservationResult> Handle(AddFlightReservationCommand command, CancellationToken cancellationToken)
        {
            var trip = await tripRepository.GetByGuidIdAsync(command.TripId);

            if (trip is null)
                throw new TripNotFoundException(command.TripId);

            var flightReservation = FlightReservationFactory.Create(command.FlightReservation);

            trip.AddFlightReservation(flightReservation);

            await tripRepository.SaveChangesAsync();

            return new AddFlightReservationResult(trip.Id, flightReservation.Id);
        }
    }
}
