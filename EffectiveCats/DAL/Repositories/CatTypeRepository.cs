using DAL.Interfaces.Finders;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace DAL.Repositories
{
    public class CatTypeRepository : BaseRepository<CatType, long>, ICatTypeRepository
    {
        public CatTypeRepository(MainContext context) : base(context) { }
    }
}
