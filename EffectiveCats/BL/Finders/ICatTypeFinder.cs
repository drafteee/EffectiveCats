using Domain.Models;

namespace BL.Finders
{
    public interface ICatTypeFinder
    {
        Task<CatType?> GetById(long id);
        Task<List<CatType>> GetAll();
    }
}
