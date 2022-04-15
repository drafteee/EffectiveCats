using Domain.Interfaces.Finders;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Domain.Repositories
{
    public class CatTypeRepository : BaseRepository<CatType, long>, ICatTypeRepository
    {
        public CatTypeRepository(MainContext context, IFinder<CatType, long> finder) : base(context, finder) { }
    }
}
