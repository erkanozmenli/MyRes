using MyRes.BuildingBlocks.Application.CQRS;
using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationsByTripId
{
    public sealed record GetFlightReservationsByTripIdQuery(
        Guid TripId,
        FlightDirection? Direction,
        int Page,
        int PageSize) : IQuery<GetFlightReservationsByTripIdResult>;
}
