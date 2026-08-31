using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace MyRes.ProviderService.Infrastructure.Data.Persistence.Contexts
{
    public class ProviderDbContext : DbContext
    {
        public ProviderDbContext(DbContextOptions<ProviderDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ProviderDbContext).Assembly);

            builder.AddInboxStateEntity();
            builder.AddOutboxMessageEntity();
            builder.AddOutboxStateEntity();

            base.OnModelCreating(builder);
        }
    }
}
