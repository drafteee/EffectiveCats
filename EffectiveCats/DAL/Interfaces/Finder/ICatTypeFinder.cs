using DAL.Interfaces.Finder;
using DAL.Models;

namespace DAL.Interfaces.Finders
{
    public interface ICatTypeFinder
    {
        Task<CatType?> GetById(long id);
        Task<List<CatType>> GetAll();
    }
}
