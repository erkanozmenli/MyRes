using Microsoft.EntityFrameworkCore;
using MyRes.BuildingBlocks.Infrastructure.Data.Seed;
using MyRes.TripService.Infrastructure.Data.Persistence.Contexts;

namespace MyRes.TripService.Infrastructure.Data.Seed
{
    public class TripsDataSeeder(TripDbContext dbContext) : IDataSeeder
    {
        public async Task SeedAllAsync()
        {
            if (!await dbContext.Trips.AnyAsync())
                await dbContext.Trips.AddRangeAsync(InitialData.Trips);

            await dbContext.SaveChangesAsync();
        }
    }
}
