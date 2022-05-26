using BL.Finders;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SQLiteDAL.Repositories;

namespace SQLiteDAL.Finders
{
    public class CatTypeFinder : ICatTypeFinder
    {
        private DbSet<CatType> _dbSet;

        public CatTypeFinder(MainContext context)
        {
            _dbSet = context.CatTypes;
        }

        public Task<List<CatType>> GetAll()
        {
            return _dbSet.ToListAsync();
        }

        public Task<CatType?> GetById(long id)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
