using MyRes.BuildingBlocks.Application.CQRS;

namespace MyRes.TripService.Application.Trips.Queries.FlightReservations.GetFlightReservationById
{
    public record GetFlightReservationByIdQuery(Guid TripId, int Id) : IQuery<GetFlightReservationByIdResult>;
}
