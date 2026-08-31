using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRes.TripService.Domain.Entities;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Configurations
{
    public class TripItemConfigurations : IEntityTypeConfiguration<TripItem>
    {
        public void Configure(EntityTypeBuilder<TripItem> builder)
        {
            builder.ToTable(nameof(TripItem));
            builder.HasKey(x => x.Id);
            builder.UseTptMappingStrategy();
        }
    }
}
