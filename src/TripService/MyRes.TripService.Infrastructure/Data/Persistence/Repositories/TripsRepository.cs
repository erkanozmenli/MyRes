using Microsoft.EntityFrameworkCore;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Domain.Entities;
using MyRes.TripService.Domain.Entities.AggregateRoots;
using MyRes.TripService.Infrastructure.Data.Persistence.Contexts;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Repositories
{
    public class TripsRepository : Repository<Trip>, ITripRepository
    {
        public TripsRepository(TripDbContext db) : base(db) { }

        public async Task<bool> ExistsByReservationNumberAsync(int reservationNo)
        {
            return await _db.Trips.AnyAsync(x => x.TripNo == reservationNo);
        }

        public async Task<Trip?> GetByReservationNumberAsync(int reservationNo)
        {
            return await _db.Trips.SingleOrDefaultAsync(x => x.TripNo == reservationNo);
        }

        public async Task<Trip?> GetReservationWithFlightsAsync(Guid reservationId)
        {
            //return await _db.Trips
            //    .Include(r => r.Lines)
            //        .ThenInclude(l => ((FlightReservation)l).Flights)
            //            .ThenInclude(f => f.Segments)
            //    .SingleOrDefaultAsync(r => r.Id == reservationId);

            var reservation = await _db.Trips
                .SingleOrDefaultAsync(r => r.Id == reservationId);

            if (reservation is null)
                return null;

            await _db.Entry(reservation)
                .Collection(r => r.Lines)
                .Query()
                .OfType<FlightReservation>()
                .Include(fr => fr.Flights)
                    .ThenInclude(f => f.Segments)
                .LoadAsync();

            return reservation;
        }
    }
}
