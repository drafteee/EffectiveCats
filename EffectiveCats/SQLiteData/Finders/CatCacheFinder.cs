using BL.Finders;
using BLL;
using Domain.Entities;

namespace SQLiteDAL.Finders
{
    internal class CatCacheFinder : ICatFinder
    {
        private DictionaryCache<Cat> _cache;
        private CatFinder _finder;

        public CatCacheFinder(DictionaryCache<Cat> cache, CatFinder finder)
        {
            _cache = cache;
            _finder = finder;
        }

        public Task<List<Cat>> GetAll()
        {
            return _finder.GetAll();
        }

        public async Task<Cat?> GetById(long id)
        {
            Cat? cacheEntity = _cache.Get($"Cat{id}");

            if (cacheEntity != null) return cacheEntity;
            else
            {
                var entity = await _finder.GetById(id);

                if(entity != null)
                    _cache.Set($"Cat{id}", entity);

                return entity;
            }
        }
    }
}
