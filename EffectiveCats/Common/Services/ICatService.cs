namespace MediatR.Services
{
    public interface ICatService
    {
        Task<int> Create(Domain.Entities.Cat entity);
        Task Delete(long id);
        Task<Domain.Entities.Cat?> Get(long id);
        Task<List<Domain.Entities.Cat>> GetAll();
        Task<int> Update(Domain.Entities.Cat entity);
    }
}
