using Domain.Models;

namespace Domain.Repositories
{
    public class CatTypeRepository : GenericIdRepository<CatType>
    {
        public CatTypeRepository(MainContext context) : base(context) { }
    }
}
