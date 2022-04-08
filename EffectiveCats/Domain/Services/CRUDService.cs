using Domain.Interfaces;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class CRUDService<T> : ICRUD<T>
        where T : class, IId
    {
        private readonly DbContext _db;

        public CRUDService(MainContext context)
        {
            _db = context;
        }

        private GenericIdRepository<T> _repository;
        public GenericIdRepository<T> Repository => _repository ?? (_repository = new GenericIdRepository<T>(_db));

        public async Task<bool> Create(T entity)
        {
            return await Repository.AddAndSaveAsync(entity);
        }

        public async Task<long> Delete(long id)
        {
            return await Repository.DeleteByIdAsync(id);
        }

        public async Task<T> Get(long id)
        {
            var entity = await Repository.GetByIdNoTrackingAsync(id);
            if (entity == null) throw new KeyNotFoundException($"{typeof(T).Name} not found");
            return entity;
        }

        public async Task<List<T>> GetAll()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<T> Update(T entity)
        {
            var newEntity = await Repository.EditByIdAsync(entity, entity.Id);
            await Repository.SaveAsync();

            return newEntity;
        }
    }
}
