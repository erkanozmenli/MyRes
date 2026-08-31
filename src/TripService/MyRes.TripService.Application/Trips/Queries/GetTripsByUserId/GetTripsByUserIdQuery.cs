using MyRes.BuildingBlocks.Application.CQRS;

namespace MyRes.TripService.Application.Trips.Queries.GetTripsByUserId
{
    public record GetTripsByUserIdQuery : IQuery<GetTripsByUserIdResult>;
}
