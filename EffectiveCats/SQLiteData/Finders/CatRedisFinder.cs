using BL.Finders;
using Domain.Entities;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace SQLiteDAL.Finders
{
    internal class CatRedisFinder : ICatFinder
    {
        private IDatabase _cache;
        private CatFinder _finder;

        public CatRedisFinder(IDatabase cache, CatFinder finder)
        {
            _cache = cache;
            _finder = finder;
        }
        public Task<List<Cat>> GetAll()
        {
            return _finder.GetAll();
        }

        public Task<Cat?> GetById(long id)
        {
            byte[] cacheEntity = _cache.StringGet($"Cat{id}");

            if(cacheEntity != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cacheEntity);
                return Task.Run(() => JsonSerializer.Deserialize<Cat?>(cachedDataString));
            }
            else
            {
                var entity = _finder.GetById(id);

                string cachedData = JsonSerializer.Serialize(entity);
                var dataToCache = Encoding.UTF8.GetBytes(cachedData);

                _cache.StringSet($"Cat{id}", dataToCache);

                return entity;
            }
        }
    }
}
