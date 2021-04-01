using Eatigo.Eatilink.Api;
using Eatigo.Eatilink.Domain.Interfaces;
using Eatigo.Eatilink.Test.Unit.DependencyResolver;
using Eatigo.Eatilink.Validator;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Eatigo.Eatilink.Test.Unit.Security
{
    public class AutoRefreshingCacheServiceUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        public AutoRefreshingCacheServiceUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void AutoRefreshingCache()
        {
            var autoRefreshingCacheService = _serviceProvider.GetService<IAutoRefreshingCacheService>();
            var result = autoRefreshingCacheService.GetUrlsAsync();
        }

        [Fact]
        public async Task ExpireAndReAddFromCallbackWorks()
        {
            var cache = CreateCache();
            var initialValue = "I'm a value";
            var newValue = "I'm the refreshed value";
            string key = "myKey";
            int refreshCounter = 0;

            var options = new MemoryCacheEntryOptions();
            options.SetAbsoluteExpiration(new TimeSpan(0, 0, 1));
            options.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration()
            {
                EvictionCallback = (subkey, subValue, reason, state) =>
                {
                    if (reason == EvictionReason.Expired)
                    {
                        cache.Set(key, newValue);
                        refreshCounter++;
                    }
                }
            });

            cache.Set(key, initialValue, options);

            await Task.Delay(TimeSpan.FromSeconds(6));

            // Any activity on the cache (Get, Set, Remove) can trigger a background scan for expired items.
            // There's no background thread that scans the cache for expired times
            var result = cache.Get(key);

            await Task.Delay(TimeSpan.FromSeconds(1));

            Assert.True(refreshCounter >= 1);
            Assert.Equal(newValue, cache.Get(key));
        }

        [Fact]
        public async Task TokenExpireAndReAddFromCallbackWorks()
        {
            var cache = CreateCache();
            var initialValue = "I'm a value";
            var newValue = "I'm the refreshed value";
            string key = "myKey";
            int refreshCounter = 0;

            int expirationSeconds = 1;
            var expirationTime = DateTime.Now.AddSeconds(expirationSeconds);
            var expirationToken = new CancellationChangeToken(
                new CancellationTokenSource(TimeSpan.FromSeconds(expirationSeconds + .01)).Token);

            var options = new MemoryCacheEntryOptions();
            options.SetAbsoluteExpiration(expirationTime);
            options.AddExpirationToken(expirationToken);
            options.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration()
            {
                EvictionCallback = (subkey, subValue, reason, state) =>
                {
                    if (reason == EvictionReason.TokenExpired)
                    {
                        cache.Set(key, newValue);
                        refreshCounter++;
                    }
                }
            });

            cache.Set(key, initialValue, options);

            await Task.Delay(TimeSpan.FromSeconds(6));

            Assert.True(refreshCounter >= 1);
            Assert.Equal(newValue, cache.Get(key));
        }

        private IMemoryCache CreateCache()
        {
            return new MemoryCache(new MemoryCacheOptions()
            {
                ExpirationScanFrequency = new TimeSpan(0, 0, 1)
            });
        }
    }
}
