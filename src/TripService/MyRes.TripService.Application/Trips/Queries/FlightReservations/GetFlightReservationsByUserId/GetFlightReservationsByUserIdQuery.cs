using MyRes.BuildingBlocks.Application.CQRS;

namespace MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationsByUserId
{
    public sealed record GetFlightReservationsByUserIdQuery : IQuery<GetFlightReservationsByUserIdResult>;
}
