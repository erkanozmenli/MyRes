using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Application.Trips.Commands.AddFlightReservation.DTOs
{
    public sealed record FlightDto(
        FlightDirection Direction,
        IReadOnlyCollection<FlightSegmentDto> Segments);
}
