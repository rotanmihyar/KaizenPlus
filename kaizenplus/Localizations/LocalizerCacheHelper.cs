using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;

namespace kaizenplus.Localizations
{
    public class LocalizerCacheHelper
    {
        private readonly IMemoryCache _memoryCache;

        public LocalizerCacheHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool TryGetValue(string cacheKey, out ConcurrentDictionary<string, string> localization)
        {
            if (_memoryCache != null)
            {
                return _memoryCache.TryGetValue(cacheKey, out localization);
            }

            throw new InvalidOperationException("Both MemoryCache and DistributedCache are null");
        }

        public void Set(string cacheKey, ConcurrentDictionary<string, string> localization, TimeSpan cacheDuration)
        {
            if (_memoryCache == null)
            {
                throw new InvalidOperationException("Both MemoryCache and DistributedCache are null");
            }

            if (_memoryCache != null)
            {
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(cacheDuration);

                _memoryCache.Set(cacheKey, localization, cacheEntryOptions);
            }
        }

        public void Remove(string cacheKey)
        {
            if (_memoryCache != null)
            {
                _memoryCache.Remove(cacheKey);
            }
        }
    }
}