using MassTransit;
using Microsoft.EntityFrameworkCore;
using MyRes.TripService.Domain.Entities;
using MyRes.TripService.Domain.Entities.AggregateRoots;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Contexts
{
    public class TripDbContext : DbContext
    {
        public DbSet<Trip> Trips => Set<Trip>();
        public DbSet<FlightReservation> FlightReservations => Set<FlightReservation>();

        public TripDbContext(DbContextOptions<TripDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(TripDbContext).Assembly);

            builder.AddInboxStateEntity();
            builder.AddOutboxMessageEntity();
            builder.AddOutboxStateEntity();

            base.OnModelCreating(builder);
        }
    }
}
