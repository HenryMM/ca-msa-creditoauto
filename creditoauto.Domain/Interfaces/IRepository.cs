using System.Linq.Expressions;

namespace creditoauto.Domain.Interfaces
{
    public interface IRepository<T> : IAsyncDisposable where T : class
    {
        Task<T> GetEntityByIdAsync(int id);
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> SearchByAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateEntityAsync(T entity);
        Task<IEnumerable<T>> CreateEntitiesAsync(IEnumerable<T> entity);
        Task<T> UpdateEntityAsync(T entity);
        Task<int> SaveAsync();
        Task DeleteEntityAsync(int id);


    }
}
