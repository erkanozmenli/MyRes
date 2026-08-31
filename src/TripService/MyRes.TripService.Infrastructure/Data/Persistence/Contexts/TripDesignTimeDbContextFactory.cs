using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Contexts
{
    public class TripDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TripDbContext>
    {
        public TripDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TripDbContext>();
            optionsBuilder.UseSqlServer("");

            return new TripDbContext(optionsBuilder.Options);
        }
    }
}
