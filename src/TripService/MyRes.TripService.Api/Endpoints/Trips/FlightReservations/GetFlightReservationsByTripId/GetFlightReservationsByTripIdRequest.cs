using MyRes.TripService.Api.Contracts.Enums;

namespace MyRes.TripService.Api.Endpoints.Trips.FlightReservations.GetFlightReservationsByTripId
{
    public record GetFlightReservationsByTripIdRequest(
        FlightDirection? Direction,
        int Page = 1,
        int PageSize = 20);
}