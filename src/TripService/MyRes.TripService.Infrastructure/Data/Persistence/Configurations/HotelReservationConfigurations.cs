using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRes.TripService.Domain.Entities;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Configurations
{
    public class HotelReservationConfigurations : IEntityTypeConfiguration<HotelReservation>
    {
        public void Configure(EntityTypeBuilder<HotelReservation> builder)
        {
            builder.ToTable(nameof(HotelReservation));
        }
    }
}
