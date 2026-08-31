using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRes.TripService.Domain.Entities;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Configurations
{
    public class FlightReservationConfigurations : IEntityTypeConfiguration<FlightReservation>
    {
        public void Configure(EntityTypeBuilder<FlightReservation> builder)
        {
            builder.ToTable(nameof(FlightReservation));

            builder.Property(x => x.TripType)
                .IsRequired();

            builder.HasMany(x => x.Flights)
                .WithOne()
                .HasForeignKey(x => x.TripItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
