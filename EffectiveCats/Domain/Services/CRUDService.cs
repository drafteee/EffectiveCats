using DAL.Interfaces;
using Domain.Interfaces;

namespace Domain.Services
{
    public class CRUDService<T, K> : ICRUD<T, K>
        where T : class, DAL.Interfaces.IId<K>
    {
        private IRepository<T, K> _repository;
        private IUnitOfWork _unitofWork;

        public CRUDService(IRepository<T,K> repository, IUnitOfWork unitofWork)
        {
            _repository = repository;
            _unitofWork = unitofWork;
        }


        public async Task<K> Create(T entity)
        {
            _repository.Add(entity);
            _unitofWork.Complete();
            return entity.Id;
        }

        public async Task<K> Delete(K id)
        {
            var deletedId = await _repository.DeleteByIdAsync(id);
            _unitofWork.Complete();
            return deletedId;
        }

        public async Task<T> Get(K id)
        {
            var entity = await _finder.GetByIdNoTrackingAsync(id);
            if (entity == null) throw new KeyNotFoundException($"{typeof(T).Name} not found");
            return entity;
        }

        public Task<List<T>> GetAll()
        {
            return _finder.GetAllAsync();
        }

        public async Task<T> Update(T entity)
        {
            var newEntity = await _repository.EditByIdAsync(entity, entity.Id);
            _unitofWork.Complete();
            return newEntity;
        }
    }
}
