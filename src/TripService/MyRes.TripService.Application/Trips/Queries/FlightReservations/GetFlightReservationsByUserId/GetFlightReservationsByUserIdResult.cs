using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;

namespace MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationsByUserId
{
    public sealed record GetFlightReservationsByUserIdResult(IReadOnlyList<FlightReservationDto> TripItems);
}
