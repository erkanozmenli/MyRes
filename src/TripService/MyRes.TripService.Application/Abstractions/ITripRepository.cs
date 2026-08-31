using MyRes.TripService.Domain.Entities.AggregateRoots;

namespace MyRes.TripService.Application.Abstractions
{
    public interface ITripRepository : IRepository<Trip>
    {
        Task<Trip?> GetByReservationNumberAsync(int reservationNo);
        Task<bool> ExistsByReservationNumberAsync(int reservationNo);
        Task<Trip?> GetReservationWithFlightsAsync(Guid reservationId);
    }
}
