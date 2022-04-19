using DAL.Models;

namespace BL.Services
{
    public interface ICatService
    {
        Task<int> Create(Cat entity);
        Task<int> Delete(long id);
        Task<Cat?> Get(long id);
        Task<List<Cat>> GetAll();
        Task<int> Update(Cat entity);
    }
}
