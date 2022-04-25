using BL.Finders;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SQLiteDAL.Repositories;

namespace SQLiteDAL.Finders
{
    public class CatFinder : ICatFinder
    {
        private DbSet<Cat> _dbSet;

        public CatFinder(MainContext context) 
        {
            _dbSet = context.Cats;
        }

        public Task<List<Cat>> GetAll()
        {
            return _dbSet.ToListAsync();
        }

        public Task<Cat?> GetById(long id)
        {
            return _dbSet.FirstOrDefaultAsync(x=> x.Id == id);
        }
    }
}
