using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;

namespace MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationsByTripId
{
    public record GetFlightReservationsByTripIdResult(IReadOnlyList<FlightReservationDto> TripItems);
}
