using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;

namespace MyRes.TripService.Application.Trips.Queries.GetTripsByUserId
{
    public record GetTripsByUserIdResult(IReadOnlyList<TripDto> Trips);
}
