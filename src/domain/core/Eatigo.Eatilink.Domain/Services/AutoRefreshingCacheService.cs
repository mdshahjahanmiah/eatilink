using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.DataObjects.Settings;
using Eatigo.Eatilink.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
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

        public async Task<ShortUrlRequest> GetUrlsAsync()
        {
            // Normal lock doesn't work in async code
            if (!_memoryCache.TryGetValue(_appSettings.MemoryCache.CacheKey, out ShortUrlRequest settings))
            {
                SemaphoreSlim certLock = locks.GetOrAdd(_appSettings.MemoryCache.CacheKey, k => new SemaphoreSlim(1, 1));
                await certLock.WaitAsync();

                try
                {
                    if (!_memoryCache.TryGetValue(_appSettings.MemoryCache.CacheKey, out settings))
                    {
                        // This method is not implemented because it can be anything.
                        // The main part is that you want to cache an object.
                        
                        //settings = await GetSettingsFromRemoteLocation();

                        settings = new ShortUrlRequest { OriginalUrl = "Hasan", Domain = "Hasan" };
                        _memoryCache.Set(_appSettings.MemoryCache.CacheKey, settings, GetMemoryCacheEntryOptions(_appSettings.MemoryCache.RefreshTimeInDays));

                        var test = _memoryCache.Get(_appSettings.MemoryCache.CacheKey);
                    }
                }
                finally
                {
                    certLock.Release();
                }
            }
            return settings;
        }

        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions(int expireInSeconds = 3600)
        {
            var expirationTime = DateTime.Now.AddSeconds(expireInSeconds);
            var expirationToken = new CancellationChangeToken(
                new CancellationTokenSource(TimeSpan.FromSeconds(expireInSeconds + .01)).Token);

            var options = new MemoryCacheEntryOptions();
            options.SetAbsoluteExpiration(expirationTime);
            options.AddExpirationToken(expirationToken);

            options.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration()
            {
                EvictionCallback = (key, value, reason, state) =>
                {
                    if (reason == EvictionReason.TokenExpired || reason == EvictionReason.Expired)
                    {
                        // If newValue is not null - update, otherwise just refresh the old value
                        // The condition by which you decide to update or refresh the data depends entirely on you
                        // If you want a cache object that will never expire you can just make the following call:
                        // memoryCache.Set(key, value, GetMemoryCacheEntryOptions(expireInSeconds));
                        
                        //var newValue = await GetSettingsFromRemoteLocation();

                        var newValue = new ShortUrlRequest { OriginalUrl = "Hasan", Domain = "Hasan"};
                        if (newValue != null)
                        {
                            _memoryCache.Set(key, newValue, GetMemoryCacheEntryOptions(expireInSeconds)); 
                        }
                        else
                        {
                            _memoryCache.Set(key, value, GetMemoryCacheEntryOptions(expireInSeconds)); 
                        }
                    }
                }
            });

            var test = _memoryCache.Get(_appSettings.MemoryCache.CacheKey);
            return options;
        }
    }
}
