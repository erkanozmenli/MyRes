using Microsoft.EntityFrameworkCore;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;
using MyRes.TripService.Infrastructure.Data.Persistence.Contexts;

namespace MyRes.TripService.Infrastructure.Data.QueryService.Trip
{
    internal class TripQueryService(TripDbContext tripsDbContext) : ITripQueryService
    {
        public async Task<TripDto?> GetTripByIdAsync(Guid id)
        {
            return await tripsDbContext.Trips
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new TripDto { Id = x.Id, TripNo = x.TripNo })
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TripDto>> GetTripsByUserIdAsync(Guid userId)
        {
            return await tripsDbContext.Trips
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => new TripDto { Id = x.Id, TripNo = x.TripNo }).ToListAsync();
        }
    }
}
