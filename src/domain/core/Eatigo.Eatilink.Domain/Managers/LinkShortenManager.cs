using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.DataObjects.Settings;
using Eatigo.Eatilink.Domain.Interfaces;
using Eatigo.Eatilink.Domain.Mappers;
using Eatigo.Eatilink.Infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace Eatigo.Eatilink.Domain.Managers
{
    public class LinkShortenManager : ILinkShortenManager
    {
        private readonly AppSettings _appSettings;
        private readonly IRepositoryLinkShortenUrl _repository;
        private readonly ILinkShortenService _linkShortenService;
        private readonly IAutoRefreshingCacheService _autoRefreshingCacheService;
        private readonly ILogger<LinkShortenManager> _logger;
        public LinkShortenManager(AppSettings appSettings, IRepositoryLinkShortenUrl repository, ILinkShortenService linkShortenService, IAutoRefreshingCacheService autoRefreshingCacheService, ILogger<LinkShortenManager> logger)
        {
            _appSettings = appSettings;
            _repository = repository;
            _linkShortenService = linkShortenService;
            _autoRefreshingCacheService = autoRefreshingCacheService;
            _logger = logger;
        }

        public ShortUrlResponse Shorten(ShortUrlRequest model)
        {
            var shortUrl = _linkShortenService.GenerateShortUrl();
            var uniqueId = _linkShortenService.ShortUrlToId(shortUrl);
            var base62ShortUrl = _linkShortenService.IdToBase62(uniqueId);

            _logger.LogInformation("[Link Shorten Manager] " + "Plain Short Url:" + shortUrl + " Unique Id:" + uniqueId + " Base62 Short Url:" + base62ShortUrl);
            var entity = ShortenUrlMapper.ToEntity(model, base62ShortUrl, uniqueId, _appSettings.MemoryCache.RefreshTimeInDays);

            var cacheResult = _autoRefreshingCacheService.CacheValue(entity.OriginalUrl, entity.ShortUrl);
            if (cacheResult != null) 
            {
                _logger.LogInformation("[Link Shorten Manager][Shorten Url return from cache memory with considaration of time limt.]");
                return new ShortUrlResponse { OriginalUrl = cacheResult["OriginalUrl"] as string, ShortUrl = cacheResult["ShortenUrl"] as string };
            }
            var result = _repository.InsertAsync(entity);
            _logger.LogInformation("[Link Shorten Manager][Shorten Url save to cahce memory and database, then return shorten url]");
            return ShortenUrlMapper.ToObject(result);
        }
    }
}
