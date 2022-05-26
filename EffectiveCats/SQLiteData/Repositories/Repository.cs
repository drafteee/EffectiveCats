using BL.Repository;
using Microsoft.EntityFrameworkCore;

namespace SQLiteDAL.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly MainContext _dbContext;

        protected DbSet<T> DbContext => _dbContext.Set<T>();

        public Repository(MainContext context)
        {
            _dbContext = context;
        }

        public void Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }
    }
}
