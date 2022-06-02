using StackExchange.Redis;

namespace BLL
{
    public class DictionaryCache<T> : IDisposable
        where T : class
    {
        private readonly ISubscriber _subscriber;
        private Dictionary<string, WeakReference<T>> _values = new();
        private ReaderWriterLockSlim _rwls = new ReaderWriterLockSlim();
        public DictionaryCache(IConnectionMultiplexer connectionMultiplexer)
        {
            _subscriber = connectionMultiplexer.GetSubscriber();

            _subscriber.SubscribeAsync("memory_cache_ping", Listen).GetAwaiter().GetResult();
        }

        private void Listen(RedisChannel channel, RedisValue value)
        {
            _rwls.EnterWriteLock();

            var data = System.Text.Json.JsonSerializer.Deserialize<CacheData<T>>(value);
            _values.Add(data.CacheKey, new WeakReference<T>(data.Data));

            _rwls.ExitWriteLock();
        }

        public T? Get(string key)
        {
            if (_values.ContainsKey(key))
            {
                _rwls.EnterReadLock();

                _values[key].TryGetTarget(out var returnValue);

                _rwls.ExitReadLock();
                return returnValue;
            }

            return null;
        }

        public async void Set(string key, T value)
        {
            _rwls.EnterWriteLock();

            if (_values.ContainsKey(key))
                _values[key] = new WeakReference<T>(value);
            else
                _values.Add(key, new WeakReference<T>(value));

            _rwls.ExitWriteLock();

            var updatedData = new CacheData<T>
            {
                CacheKey = key,
                Data = value
            };

            var redisChannelData = System.Text.Json.JsonSerializer.Serialize(updatedData);

            try
            {
                await _subscriber.PublishAsync("memory_cache_ping", redisChannelData);
            }
            catch(Exception ex)
            {
                _rwls.EnterWriteLock();

                _values.Remove(key);

                _rwls.ExitWriteLock();
            }
        }

        public void Delete(string key)
        {
            _rwls.EnterWriteLock();

            if(_values.ContainsKey(key))
                _values.Remove(key);

            _rwls.ExitWriteLock();
        }

        public void Dispose()
        {
            _subscriber.Unsubscribe("memory_cache_ping");
        }
    }

    public class CacheData<T>
    {
        public string CacheKey { get; set; }
        public T Data { get; set; }
    }
}
