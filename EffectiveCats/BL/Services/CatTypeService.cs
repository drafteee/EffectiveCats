using BL.Finders;
using BL.Repository;
using Domain.Exceptions;
using Domain.Models;
using MediatR.Services;

namespace BL.Services
{
    public class CatTypeService : ICatTypeService
    {
        private ICatTypeFinder _finder;
        private IRepository<CatType> _repository;
        private IUnitOfWork _unitOfWork;

        public CatTypeService(ICatTypeFinder finder, IRepository<CatType> repository, IUnitOfWork unitofWork)
        {
            _finder = finder;
            _repository = repository;
            _unitOfWork = unitofWork;
        }

        public Task<int> Create(CatType entity)
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

            throw new AppException($"Not found CatType id={id}");
        }

        public Task<CatType?> Get(long id)
        {
            return _finder.GetById(id);
        }

        public Task<List<CatType>> GetAll()
        {
            return _finder.GetAll();
        }

        public Task<int> Update(CatType entity)
        {
            _repository.Edit(entity);
            return _unitOfWork.Complete();
        }
    }
}