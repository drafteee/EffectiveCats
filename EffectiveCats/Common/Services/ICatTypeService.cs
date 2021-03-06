
using MediatRL.Models;
using Domain.Entities;

namespace MediatR.Services
{
    public interface ICatTypeService
    {
        Task<int> Create(CatType entity);
        Task<int> Delete(long id);
        Task<CatType?> Get(long id);
        Task<List<CatType>> GetAll();
        Task<int> Update(CatType entity);
    }
}
