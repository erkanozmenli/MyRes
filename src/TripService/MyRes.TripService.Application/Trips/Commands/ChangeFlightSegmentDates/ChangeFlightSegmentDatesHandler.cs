using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.TripService.Application.Abstractions;

namespace MyRes.TripService.Application.Trips.Commands.ChangeFlightSegmentDates
{
    internal class ChangeFlightSegmentDatesHandler
        (ITripRepository reservationRepository) : ICommandHandler<ChangeFlightSegmentDatesCommand, ChangeFlightSegmentDatesResult>
    {
        public async Task<ChangeFlightSegmentDatesResult> Handle(ChangeFlightSegmentDatesCommand command, CancellationToken cancellationToken)
        {
            var reservation = await reservationRepository.GetReservationWithFlightsAsync(command.TripId);

            if (reservation is null)
                return new ChangeFlightSegmentDatesResult(false);

            reservation.ChangeFlightSegmentDates(
                command.FlightReservationId,
                command.FlightId,
                command.SegmentId,
                command.FlightSegmentDates.DepartureDate,
                command.FlightSegmentDates.ArrivalDate
                );

            await reservationRepository.SaveChangesAsync();

            return new ChangeFlightSegmentDatesResult(true);
        }
    }
}
