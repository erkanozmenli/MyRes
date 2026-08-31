using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRes.TripService.Domain.Entities;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Configurations
{
    public class FlightSegmentConfigurations : IEntityTypeConfiguration<FlightSegment>
    {
        public void Configure(EntityTypeBuilder<FlightSegment> builder)
        {
            builder.ToTable(nameof(FlightSegment));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FromAirport)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(x => x.ToAirport)
                    .HasMaxLength(3)
                    .IsRequired();

            builder.Property(x => x.DepartureTime)
                    .IsRequired();

            builder.Property(x => x.ArrivalTime)
                .IsRequired();

            builder.HasOne(x => x.PreviousSegment)
                .WithMany()
                .HasForeignKey(x => x.PreviousSegmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
