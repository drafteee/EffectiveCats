using Domain.Interfaces.Finders;
using Domain.Models;
using Domain.Repositories;

namespace Domain.Finders
{
    public class CatFinder : BaseFinder<Cat, long>, ICatFinder
    {
        public CatFinder(MainContext context) : base(context) { }
    }
}
