using Models = Domain.Models;

namespace MediatR.Services
{
    public interface ICatService
    {
        Task<int> Create(Models.Cat entity);
        Task<int> Delete(long id);
        Task<Models.Cat?> Get(long id);
        Task<List<Models.Cat>> GetAll();
        Task<int> Update(Models.Cat entity);
    }
}
