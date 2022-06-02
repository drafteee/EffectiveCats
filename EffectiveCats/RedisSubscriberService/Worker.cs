using StackExchange.Redis;
using Microsoft.Extensions.Caching.Memory;

namespace RedisSubscriberService
{
    public class Worker : BackgroundService
    {
        private readonly ISubscriber _subscriber;

        public Worker(IConnectionMultiplexer connectionMultiplexer)
        {
            _subscriber = connectionMultiplexer.GetSubscriber();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _subscriber.SubscribeAsync(RedisChannelConstant.MemoryCache, async (a, updatedData) =>
            {
                await _subscriber.PublishAsync(RedisChannelConstant.MemoryCachePing, updatedData);
            });
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _subscriber.UnsubscribeAsync(RedisChannelConstant.MemoryCache);
            await base.StopAsync(cancellationToken);
        }
    }

    public class MemoryCacheDataDto
    {
        public string CacheKey { get; set; }
        public object Data { get; set; }
    }

    public static class RedisChannelConstant
    {
        public const string MemoryCache = "memory_cache";
        public const string MemoryCachePing = "memory_cache_ping";
    }
}