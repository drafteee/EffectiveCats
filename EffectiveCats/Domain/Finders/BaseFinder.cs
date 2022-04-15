using Domain.Interfaces;
using Domain.Interfaces.Finders;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Finders
{
    public class BaseFinder<T, K> : IFinder<T, K>
        where T : class, IId<K>
    {
        private DbSet<T> _dbSet;
        protected DbSet<T> DbSet => _dbSet;

        public BaseFinder(MainContext context)
        {
            _dbSet = context.Set<T>();
        }
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public Task<List<T>> GetAllAsync()
        {
            return _dbSet.AsNoTracking().ToListAsync();
        }

        public Task<T> GetByIdAsync(K id)
        {
            return _dbSet.FirstOrDefaultAsync(r => r.Id.Equals(id));
        }

        public Task<T> GetByIdNoTrackingAsync(K id)
        {
            return _dbSet.AsNoTracking().FirstOrDefaultAsync(r => r.Id.Equals(id));
        }
    }
}
