using MassTransit;
using Microsoft.EntityFrameworkCore;


namespace MyRes.PaymentService.Infrastructure.Data.Persistence.Contexts
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(PaymentDbContext).Assembly);

            builder.AddInboxStateEntity();
            builder.AddOutboxMessageEntity();
            builder.AddOutboxStateEntity();

            base.OnModelCreating(builder);
        }
    }
}
