using MyRes.BuildingBlocks.Application.CQRS;

namespace MyRes.TripService.Application.Trips.Queries.GetTripById
{
    public record GetTripByIdQuery(Guid TripId) : IQuery<GetTripByIdResult>;
}
