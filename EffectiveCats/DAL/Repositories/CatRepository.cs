using DAL.Interfaces.Finders;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace DAL.Repositories
{
    public class CatRepository : BaseRepository<Cat, long>, ICatRepository
    {
        public CatRepository(MainContext context) : base(context) { }

    }
}
