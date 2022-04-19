using DAL.Interfaces.Finders;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Finders
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
