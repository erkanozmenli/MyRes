using MyRes.TripService.Application.Queries.Shared.Models;

namespace MyRes.TripService.Application.Abstractions
{
    public interface IFlightReservationQueryService
    {
        Task<IReadOnlyList<FlightReservationFlatRow>> GetFlightReservationByIdAsync(Guid tripId, int Id);
        Task<IReadOnlyList<FlightReservationFlatRow>> GetFlightReservationsByTripIdAsync(Guid tripId);
        Task<IReadOnlyList<FlightReservationFlatRow>> GetFlightReservationsByUserIdAsync(Guid userId);
    }
}
