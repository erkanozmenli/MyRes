using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyRes.NotificationService.Infrastructure.Data.Persistence.Contexts
{
    public class NotificationDesignTimeDbContextFactory : IDesignTimeDbContextFactory<NotificationDbContext>
    {
        public NotificationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NotificationDbContext>();
            optionsBuilder.UseSqlServer("");

            return new NotificationDbContext(optionsBuilder.Options);
        }
    }
}
