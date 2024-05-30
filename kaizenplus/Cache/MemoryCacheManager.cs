using CacheManager.Core;

namespace kaizenplus.Cache
{
    public interface IMemoryCacheManager<T> : ICacheManager<T>
    {
    }

    public class MemoryCacheManager<T> : BaseCacheManager<T>, IMemoryCacheManager<T>
    {
        public MemoryCacheManager() : base(new ConfigurationBuilder()
                    .WithJsonSerializer()
                    .WithMicrosoftMemoryCacheHandle("inProcessCache")
                    .Build())
        {
        }
    }
}