using BL.Repository;
using BLL;
using Domain.Entities;

namespace SQLiteDAL.Repositories
{
    internal class CatCacheRepository : IRepository<Cat>
    {
        private DictionaryCache<Cat> _cache;
        private readonly Repository<Cat> _repository;

        public CatCacheRepository(DictionaryCache<Cat> cache, Repository<Cat> repository)
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
            _cache.Delete($"Cat{entity.Id}");
            _repository.Delete(entity);
        }

        public void Edit(Cat entity)
        {
            _cache.Delete($"Cat{entity.Id}");
            _repository.Edit(entity);
        }
    }
}
