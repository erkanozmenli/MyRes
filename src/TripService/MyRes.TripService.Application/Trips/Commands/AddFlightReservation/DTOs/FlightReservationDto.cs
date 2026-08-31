using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Application.Trips.Commands.AddFlightReservation.DTOs
{
    public sealed record FlightReservationDto(
        TripType TripType,
        IReadOnlyCollection<FlightDto> Flights);
}
