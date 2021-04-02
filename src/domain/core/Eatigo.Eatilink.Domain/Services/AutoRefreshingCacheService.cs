using Eatigo.Eatilink.DataObjects.Settings;
using Eatigo.Eatilink.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Eatigo.Eatilink.Domain.Services
{
    public class AutoRefreshingCacheService : IAutoRefreshingCacheService
    {
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<object, SemaphoreSlim> locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        public AutoRefreshingCacheService(AppSettings appSettings, IMemoryCache memoryCache)
        {
            _appSettings = appSettings;
            _memoryCache = memoryCache;
        }
        public Hashtable RefreshingCache(string originalUrl, string shortenUrl)
        {
            // Normal lock doesn't work in async code
            if (!_memoryCache.TryGetValue(originalUrl, out Hashtable shortenUrlHashtable))
            {
                SemaphoreSlim certLock = locks.GetOrAdd(originalUrl, k => new SemaphoreSlim(1, 1));
                certLock.WaitAsync();
                try
                {
                    if (!_memoryCache.TryGetValue(originalUrl, out shortenUrlHashtable))
                    {
                        Hashtable hashtable = new Hashtable
                        {
                            { "OriginalUrl", originalUrl },
                            { "ShortenUrl", shortenUrl }
                        };
                        _memoryCache.Set(originalUrl, hashtable, GetMemoryCacheEntryOptions(hashtable, _appSettings.MemoryCache.RefreshTimeInDays));
                    }
                }
                finally
                {
                    certLock.Release();
                }
            }
            return shortenUrlHashtable;
        }

        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions(Hashtable hashtable, int expireInDays = 1)
        {
            var expirationTime = DateTime.Now.AddDays(expireInDays);
            var options = new MemoryCacheEntryOptions();
            options.SetAbsoluteExpiration(expirationTime);

            options.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration()
            {
                EvictionCallback = (key, value, reason, state) =>
                {
                    if (reason == EvictionReason.TokenExpired || reason == EvictionReason.Expired)
                    {
                        var newValue = hashtable;
                        if (newValue != null)
                        {
                            _memoryCache.Set(key, newValue, GetMemoryCacheEntryOptions(hashtable, expireInDays));
                        }
                        else
                        {
                            _memoryCache.Set(key, value, GetMemoryCacheEntryOptions(hashtable, expireInDays));
                        }
                    }
                }
            });
            return options;
        }
    }
}
