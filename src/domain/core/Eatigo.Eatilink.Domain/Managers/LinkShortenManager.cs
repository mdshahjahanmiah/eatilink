using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.Domain.Interfaces;
using Eatigo.Eatilink.Domain.Mappers;
using Eatigo.Eatilink.Infrastructure.Repository;

namespace Eatigo.Eatilink.Domain.Managers
{
    public class LinkShortenManager : ILinkShortenManager
    {
        private readonly IRepositoryLinkShortenUrl _repository;
        private readonly ILinkShortenService _linkShortenService;
        private readonly IAutoRefreshingCacheService _autoRefreshingCacheService;
        public LinkShortenManager(IRepositoryLinkShortenUrl repository, ILinkShortenService linkShortenService, IAutoRefreshingCacheService autoRefreshingCacheService)
        {
            _repository = repository;
            _linkShortenService = linkShortenService;
            _autoRefreshingCacheService = autoRefreshingCacheService;
        }

        public ShortUrlResponse Shorten(ShortUrlRequest model)
        {
            var shortUrl = _linkShortenService.GenerateShortUrl();
            var id = _linkShortenService.ShortUrlToId(shortUrl);
            model.ShortUrl = _linkShortenService.IdToBase62(id);
            var entity = ShortenUrlMapper.ToEntity(model);
            var result = _repository.InsertAsync(entity);
            return ShortenUrlMapper.ToObject(result);
        }
    }
}
