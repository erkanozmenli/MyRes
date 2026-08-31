using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.GetFlightReservationsByTripId
{
    public record GetFlightReservationsByTripIdResponse(IReadOnlyCollection<FlightReservationDto> TripItems);
}
