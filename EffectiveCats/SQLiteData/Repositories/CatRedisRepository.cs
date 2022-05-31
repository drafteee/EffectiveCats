using BL.Repository;
using Domain.Entities;
using StackExchange.Redis;

namespace SQLiteDAL.Repositories
{
    internal class CatRedisRepository : IRepository<Cat>
    {
        private readonly IDatabase _cache;
        private readonly Repository<Cat> _repository;

        public CatRedisRepository(IDatabase cache, Repository<Cat> repository)
        {
            _cache = cache;
            _repository = repository;
        }
        public void Add(Cat entity)
        {
            _repository.Add(entity);
        }

        public void AddRange(IEnumerable<Cat> entities)
        {
            _repository.AddRange(entities);
        }

        public void Delete(Cat entity)
        {
            _cache.StringGetDelete($"Cat{entity.Id}");
            _repository.Delete(entity);
        }

        public void Edit(Cat entity)
        {
            _cache.StringGetDelete($"Cat{entity.Id}");
            _repository.Edit(entity);
        }
    }
}
