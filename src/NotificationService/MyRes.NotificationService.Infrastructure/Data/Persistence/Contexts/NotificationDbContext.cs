using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace MyRes.NotificationService.Infrastructure.Data.Persistence.Contexts
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);

            builder.AddInboxStateEntity();
            builder.AddOutboxMessageEntity();
            builder.AddOutboxStateEntity();

            base.OnModelCreating(builder);
        }
    }
}
