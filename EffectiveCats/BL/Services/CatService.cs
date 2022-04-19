using BL.Interfaces;
using DAL.Interfaces;
using DAL.Interfaces.Finders;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace Domain.Services
{
    public class CatService : ICRUD<Cat, long>
    {
        private ICatFinder _finder;
        private ICatRepository _repository;
        private IUnitOfWork _unitOfWork;

        public CatService(ICatFinder finder, ICatRepository repository, IUnitOfWork unitofWork)
        {
            _finder = finder;
            _repository = repository;
            _unitOfWork = unitofWork;
        }

        public async Task<long> Create(Cat entity)
        {
            _repository.Add(entity);
            await _unitOfWork.Complete();
            return entity.Id;
        }

        public Task<long> Delete(long id)
        {
            return _repository.DeleteByIdAsync(id);
        }

        public Task<Cat?> Get(long id)
        {
            return _finder.GetAsync(x=> x.Id == id);
        }

        public Task<List<Cat>> GetAll()
        {
            return _finder.GetAllAsync();
        }

        public Task<Cat> Update(Cat entity)
        {
            return _repository.EditByIdAsync(entity, entity.Id);
        }
    }
}
