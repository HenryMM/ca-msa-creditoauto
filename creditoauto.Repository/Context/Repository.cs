using creditoauto.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace creditoauto.Repository.Context
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        protected DbSet<T> EntitySet
        {
            get
            {
                return _context.Set<T>();
            }
        }

        public async Task<T> CreateEntityAsync(T entity)
        {
            await EntitySet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> CreateEntitiesAsync(IEnumerable<T> entities)
        {
            await EntitySet.AddRangeAsync(entities);
            return entities;
        }

        public async Task DeleteEntityAsync(int id)
        {
            T entity = await EntitySet.FindAsync(id);
            EntitySet.Remove(entity);
        }

        public async Task<IQueryable<T>> GetAllAsync(string[] incluir)
        {
            IQueryable<T> query = EntitySet.AsQueryable<T>();
            query = IncluirPropiedadesNavegacion(query, incluir);
            return query;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            IQueryable<T> query = EntitySet.AsQueryable<T>();
            return query;
        }

        public async Task<T> GetEntityByIdAsync(int id)
        {
            return await EntitySet.FindAsync(id);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<T>> SearchByAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = EntitySet.AsQueryable<T>();
            return query.Where(predicate);
        }

        public async Task<T> UpdateEntityAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async ValueTask DisposeAsync()
        {
            _context.Dispose();
        }

        private IQueryable<T> IncluirPropiedadesNavegacion(IQueryable<T> query, string[] incluir)
        {
            if (incluir != null)
            {
                foreach (string navprop in incluir)
                {
                    query = query.Include(navprop);
                }
            }

            return query;
        }

    }
}
