using DAL.Interfaces.Finders;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Finders
{
    public class CatTypeFinder : ICatTypeFinder
    {
        private DbSet<CatType> _dbSet;

        public CatTypeFinder(MainContext context)
        {
            _dbSet = context.CatTypes;
        }

        public Task<List<CatType>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<CatType?> GetAsync(Expression<Func<CatType, bool>> condition)
        {
            return _dbSet.FirstOrDefaultAsync(condition);
        }
    }
}
