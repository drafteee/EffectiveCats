using Domain.Interfaces.Finders;
using Domain.Models;
using Domain.Repositories;

namespace Domain.Finders
{
    public class CatTypeFinder : BaseFinder<CatType, long>, ICatTypeFinder
    {
        public CatTypeFinder(MainContext context) : base(context) { }
    }
}
