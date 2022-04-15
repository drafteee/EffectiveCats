using Domain.Interfaces;
using Domain.Interfaces.Finders;
using Domain.Models;
using Domain.Repositories;

namespace Domain.Services
{
    public class CatService : CRUDService<Cat, long>
    {
        public CatService(IFinder<Cat, long> finder, IRepository<Cat, long> repository, IUnitOfWork unitofWork) : base(finder, repository, unitofWork)
        {
        }
    }
}
