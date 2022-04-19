using DAL.Interfaces.Finders;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Finders
{
    public class CatFinder : ICatFinder
    {
        private DbSet<Cat> _dbSet;

        public CatFinder(MainContext context) 
        {
            _dbSet = context.Cats;
        }

        public Task<List<Cat>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<Cat?> GetAsync(Expression<Func<Cat, bool>> condition)
        {
            return _dbSet.FirstOrDefaultAsync(condition);
        }
    }
}
