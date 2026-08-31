using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyRes.PaymentService.Infrastructure.Data.Persistence.Contexts
{
    public class PaymentDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PaymentDbContext>
    {
        public PaymentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PaymentDbContext>();
            optionsBuilder.UseSqlServer("");

            return new PaymentDbContext(optionsBuilder.Options);
        }
    }
}
