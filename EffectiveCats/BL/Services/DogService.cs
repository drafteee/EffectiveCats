using BL.Finders;
using BL.Repository;
using DomainMongo;
using MediatR.Services;

namespace BL.Services
{
    public class DogService : IDogService
    {
        private IDogFinder _finder;
        private IRepository<Dog> _repository;

        public DogService(IDogFinder finder, IRepository<Dog> repository)
        {
            _finder = finder;
            _repository = repository;
        }

        public void Create(Dog entity)
        {
            _repository.Add(entity);
        }

        public async Task Delete(string id)
        {
            var entity = await _finder.GetById(id);
            if (entity != null)
            {
                _repository.Delete(entity);
            }
        }

        public Task<Dog> Get(string id)
        {
            return _finder.GetById(id);
        }

        public Task<List<Dog>> GetAll()
        {
            return _finder.GetAll();
        }

        public void Update(Dog entity)
        {
            _repository.Edit(entity);
        }
    }
}
