using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace MyRes.ProviderService.Infrastructure.Data.Persistence.Contexts
{
    public class ProviderDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProviderDbContext>
    {
        public ProviderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProviderDbContext>();
            optionsBuilder.UseSqlServer("");

            return new ProviderDbContext(optionsBuilder.Options);
        }
    }
}
