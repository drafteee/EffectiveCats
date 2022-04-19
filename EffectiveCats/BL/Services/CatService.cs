using BL.Exceptions;
using BL.Services;
using DAL.Interfaces;
using DAL.Interfaces.Finders;
using DAL.Models;

namespace Domain.Services
{
    public class CatService : ICatService
    {
        private ICatFinder _finder;
        private IRepository<Cat> _repository;
        private IUnitOfWork _unitOfWork;

        public CatService(ICatFinder finder, IRepository<Cat> repository, IUnitOfWork unitofWork)
        {
            _finder = finder;
            _repository = repository;
            _unitOfWork = unitofWork;
        }

        public Task<int> Create(Cat entity)
        {
            _repository.Add(entity);
            return _unitOfWork.Complete();
        }

        public async Task<int> Delete(long id)
        {
            var entity = await _finder.GetById(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                return await _unitOfWork.Complete();
            }

            throw new AppException($"Not found Cat id={id}");
        }

        public Task<Cat?> Get(long id)
        {
            return _finder.GetById(id);
        }

        public Task<List<Cat>> GetAll()
        {
            return _finder.GetAll();
        }

        public Task<int> Update(Cat entity)
        {
            _repository.Edit(entity);

            return _unitOfWork.Complete();
        }
    }
}
