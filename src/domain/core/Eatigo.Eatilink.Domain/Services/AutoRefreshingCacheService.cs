using Eatigo.Eatilink.DataObjects.Settings;
using Eatigo.Eatilink.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;

namespace Eatigo.Eatilink.Domain.Services
{
    public class AutoRefreshingCacheService : IAutoRefreshingCacheService
    {
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _memoryCache;

        public AutoRefreshingCacheService(AppSettings appSettings, IMemoryCache memoryCache)
        {
            _appSettings = appSettings;
            _memoryCache = memoryCache;
        }
        public Hashtable CacheValue(string originalUrl, string shortenUrl)
        {
            if (!_memoryCache.TryGetValue(originalUrl, out Hashtable shortenUrlHashtable))
            {
                Hashtable hashtable = new Hashtable
                {
                    { "OriginalUrl", originalUrl },
                    { "ShortenUrl", shortenUrl }
                };

                //Cache expiration
                var expirationTime = DateTime.Now.AddDays(_appSettings.MemoryCache.RefreshTimeInDays);

                var cacheEntryOptions = new MemoryCacheEntryOptions();
                cacheEntryOptions.SetAbsoluteExpiration(expirationTime);

                // Save values in cache for a given key.
                _memoryCache.Set(originalUrl, hashtable, cacheEntryOptions);

            }
            return shortenUrlHashtable;
        }


    }
}
