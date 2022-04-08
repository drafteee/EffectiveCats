using Domain.Models;

namespace Domain.Repositories
{
    public class CatRepository : GenericIdRepository<Cat>
    {
        public CatRepository(MainContext context) : base(context) { }
    }
}
