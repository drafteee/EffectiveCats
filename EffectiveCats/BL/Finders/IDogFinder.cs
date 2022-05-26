using Domain.Entities;
using DomainMongo;

namespace BL.Finders
{
    public interface IDogFinder
    {
        Task<Dog> GetById(string id);
        Task<List<Dog>> GetAll();
    }
}
