using Domain.Models;

namespace BL.Finders
{
    public interface ICatFinder
    {
        Task<Cat?> GetById(long id);
        Task<List<Cat>> GetAll();
    }
}
