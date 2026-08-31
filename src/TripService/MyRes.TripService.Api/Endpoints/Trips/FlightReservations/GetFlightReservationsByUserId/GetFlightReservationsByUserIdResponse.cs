using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.GetFlightReservationsByUserId
{
    public sealed record GetFlightReservationsByUserIdResponse(
        IReadOnlyCollection<FlightReservationDto> TripItems);
}
