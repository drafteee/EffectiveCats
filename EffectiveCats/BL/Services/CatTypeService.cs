using BL.Interfaces;
using DAL.Interfaces;
using DAL.Interfaces.Finders;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace BL.Services
{
    public class CatTypeService : ICRUD<CatType, long>
    {
        private ICatTypeFinder _finder;
        private ICatTypeRepository _repository;
        private IUnitOfWork _unitOfWork;

        public CatTypeService(ICatTypeFinder finder, ICatTypeRepository repository, IUnitOfWork unitofWork)
        {
            _finder = finder;
            _repository = repository;
            _unitOfWork = unitofWork;
        }

        public async Task<long> Create(CatType entity)
        {
            _repository.Add(entity);
            await _unitOfWork.Complete();
            return entity.Id;
        }

        public Task<long> Delete(long id)
        {
            return _repository.DeleteByIdAsync(id);
        }

        public Task<CatType?> Get(long id)
        {
            return _finder.GetAsync(x => x.Id == id);
        }

        public Task<List<CatType>> GetAll()
        {
            return _finder.GetAllAsync();
        }

        public Task<CatType> Update(CatType entity)
        {
            return _repository.EditByIdAsync(entity, entity.Id);
        }
    }
}