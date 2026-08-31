using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;


namespace MyRes.TripService.Application.Abstractions
{
    public interface ITripQueryService
    {
        Task<TripDto?> GetTripByIdAsync(Guid id);
        Task<IReadOnlyList<TripDto>> GetTripsByUserIdAsync(Guid id);
    }
}
