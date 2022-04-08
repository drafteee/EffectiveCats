using Domain.Models;
using Domain.Repositories;

namespace Domain.Services
{
    public class CatService : CRUDService<Cat>
    {
        public CatService(MainContext context) : base(context)
        {
        }
    }
}
