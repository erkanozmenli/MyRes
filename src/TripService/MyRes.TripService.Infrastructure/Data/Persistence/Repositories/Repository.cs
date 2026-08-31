using Microsoft.EntityFrameworkCore;
using MyRes.TripService.Application.Abstractions;
using MyRes.TripService.Infrastructure.Data.Persistence.Contexts;

namespace MyRes.TripService.Infrastructure.Data.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TripDbContext _db;
        protected readonly DbSet<T> _set;

        public Repository(TripDbContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public async Task AddAsync(T entity) => await _set.AddAsync(entity);
        public async Task<T?> GetByIdAsync(int id) => await _set.FindAsync(id);
        public async Task<T?> GetByGuidIdAsync(Guid id) => await _set.FindAsync(id);
        public void Remove(T entity) => _set.Remove(entity);
        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
