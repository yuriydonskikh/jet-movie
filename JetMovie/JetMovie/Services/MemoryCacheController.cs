using System;
using System.Collections.Concurrent;

namespace JetMovie.Services
{
    public sealed class MemoryCacheController:ICacheController
    {
        private readonly Lazy<ConcurrentDictionary<string, ICacheItem>> _cacheItems;

        public MemoryCacheController()
        {
            _cacheItems = new Lazy<ConcurrentDictionary<string, ICacheItem>>(() => new ConcurrentDictionary<string, ICacheItem>());
        }

        public TItem Use<TItem>(string key, Func<TItem> builder)
        {
            if (_cacheItems.Value.TryGetValue(key, out var item))
            {
                return item.Get<TItem>();
            }

            var result = builder();

            _cacheItems.Value.TryAdd(key, new CacheItem(result));
            return result;
        }

        public void Invalidate(string key)
        {
            _cacheItems.Value.TryRemove(key, out _);
        }
    }
}