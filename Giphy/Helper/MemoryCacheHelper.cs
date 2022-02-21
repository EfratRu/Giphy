using Microsoft.Extensions.Caching.Memory;
using System;

namespace Giphy.API
{
    public class MemoryCacheHelper
    {
        IMemoryCache memoryCache;
        public MemoryCacheHelper(IMemoryCache oMemoryCache)
        {
            memoryCache = oMemoryCache;
        }

        public T GetResponse<T>(string key, Action<T> oAction, T response)
        {
            bool isExists = memoryCache.TryGetValue<T>(key, out response);
            if (!isExists)
            {
                 oAction.Invoke(response);
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(2));
                memoryCache.Set(key, response, cacheEntryOptions);
            }
            return response;
        }
    }
}
