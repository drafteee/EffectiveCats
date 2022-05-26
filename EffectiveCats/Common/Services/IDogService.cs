using DomainMongo;

namespace MediatR.Services
{
    public interface IDogService
    {
        void Create(Dog entity);
        Task Delete(string id);
        Task<Dog?> Get(string id);
        Task<List<Dog>> GetAll();
        void Update(Dog entity);
    }
}
