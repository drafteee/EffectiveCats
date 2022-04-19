using DAL.Interfaces.Finder;
using DAL.Models;

namespace DAL.Interfaces.Finders
{
    public interface ICatFinder
    {
        Task<Cat?> GetById(long id);
        Task<List<Cat>> GetAll();
    }
}
