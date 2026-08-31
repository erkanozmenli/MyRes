using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;

namespace MyRes.TripService.Api.Endpoints.Trips.GetTripsByUserId
{
    public record GetTripsByUserIdResponse(IReadOnlyCollection<TripDto> Trips);
}
