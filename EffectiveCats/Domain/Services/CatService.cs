using DAL.Interfaces;
using DAL.Interfaces.Finders;
using DAL.Models;
using Domain.Interfaces;
using Domain.Interfaces.Finders;
using Domain.Models;
using Domain.Repositories;

namespace Domain.Services
{
    public class CatService : CRUDService<Cat, long>
    {
        public CatService(ICatFinder finder, IRepository<Cat, long> repository, IUnitOfWork unitofWork) : base(repository, unitofWork)
        {
        }
    }
}
