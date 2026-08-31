using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRes.TripService.Domain.Entities;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Configurations
{
    public class CarReservationConfigurations : IEntityTypeConfiguration<CarReservation>
    {
        public void Configure(EntityTypeBuilder<CarReservation> builder)
        {
            builder.ToTable(nameof(CarReservation));
        }
    }
}
