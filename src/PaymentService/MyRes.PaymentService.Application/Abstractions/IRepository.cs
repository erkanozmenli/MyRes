namespace MyRes.PaymentService.Application.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByGuidIdAsync(Guid id);
        Task AddAsync(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
    }
}
