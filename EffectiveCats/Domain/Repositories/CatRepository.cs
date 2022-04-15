using Domain.Interfaces.Finders;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Domain.Repositories
{
    public class CatRepository : BaseRepository<Cat, long>, ICatRepository
    {
        public CatRepository(MainContext context, IFinder<Cat, long> finder) : base(context, finder) { }
    }
}
