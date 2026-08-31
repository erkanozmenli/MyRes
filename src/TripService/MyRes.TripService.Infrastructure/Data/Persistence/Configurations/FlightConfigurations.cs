using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRes.TripService.Domain.Entities;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Configurations
{
    public class FlightConfigurations : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.ToTable(nameof(Flight));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Direction)
                .IsRequired();

            builder.HasMany(x => x.Segments)
                .WithOne()
                .HasForeignKey(x => x.FlightId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
