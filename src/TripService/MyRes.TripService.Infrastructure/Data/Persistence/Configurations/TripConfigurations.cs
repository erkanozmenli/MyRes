using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRes.TripService.Domain.Entities.AggregateRoots;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Configurations
{
    public class TripConfigurations : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder
                .ToTable(nameof(Trip))
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.TripNo)
                .UseIdentityColumn(1, 1);

            builder.HasIndex(x => x.TripNo)
                .IsUnique();

            builder.HasMany(x => x.Lines)
                .WithOne()
                .HasForeignKey(x => x.TripId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
