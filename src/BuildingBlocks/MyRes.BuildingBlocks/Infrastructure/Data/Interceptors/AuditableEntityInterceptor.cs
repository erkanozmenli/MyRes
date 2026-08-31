using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyRes.BuildingBlocks.Authentication;
using MyRes.Shared.Domain;

namespace MyRes.BuildingBlocks.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor(ICurrentIdentityAccessor accessor) : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            var identity = accessor.Identity;

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                var now = DateTime.UtcNow;
                var userId = identity.UserId.GetValueOrDefault();

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.CreatedAt = now;

                    entry.Entity.LastModifiedBy = userId;
                    entry.Entity.LastModifiedAt = now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = userId;
                    entry.Entity.LastModifiedAt = now;

                    entry.Property(x => x.CreatedBy).IsModified = false;
                    entry.Property(x => x.CreatedAt).IsModified = false;
                }
            }
        }
    }
}
