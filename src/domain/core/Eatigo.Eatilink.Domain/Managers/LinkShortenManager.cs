using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.DataObjects.Settings;
using Eatigo.Eatilink.Domain.Interfaces;
using Eatigo.Eatilink.Domain.Mappers;
using Eatigo.Eatilink.Infrastructure.Repository;

namespace Eatigo.Eatilink.Domain.Managers
{
    public class LinkShortenManager : ILinkShortenManager
    {
        private readonly AppSettings _appSettings;
        private readonly IRepositoryLinkShortenUrl _repository;
        private readonly ILinkShortenService _linkShortenService;
        private readonly IAutoRefreshingCacheService _autoRefreshingCacheService;
        public LinkShortenManager(AppSettings appSettings, IRepositoryLinkShortenUrl repository, ILinkShortenService linkShortenService, IAutoRefreshingCacheService autoRefreshingCacheService)
        {
            _appSettings = appSettings;
            _repository = repository;
            _linkShortenService = linkShortenService;
            _autoRefreshingCacheService = autoRefreshingCacheService;
        }

        public ShortUrlResponse Shorten(ShortUrlRequest model)
        {
            var shortUrl = _linkShortenService.GenerateShortUrl();
            var uniqueId = _linkShortenService.ShortUrlToId(shortUrl);
            var base62ShortUrl = _linkShortenService.IdToBase62(uniqueId);

            var entity = ShortenUrlMapper.ToEntity(model, base62ShortUrl, uniqueId, _appSettings.MemoryCache.RefreshTimeInDays);
            var result = _repository.InsertAsync(entity);
            return ShortenUrlMapper.ToObject(result);
        }
    }
}
