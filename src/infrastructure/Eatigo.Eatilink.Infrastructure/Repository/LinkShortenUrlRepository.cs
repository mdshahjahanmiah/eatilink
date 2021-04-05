using Eatigo.Eatilink.Entities.Link;
using System;
using System.Threading.Tasks;

namespace Eatigo.Eatilink.Infrastructure.Repository
{
    public class LinkShortenUrlRepository:  IRepositoryLinkShortenUrl
    {
        private readonly IRepository<ShortenUrl> _repository;
        public LinkShortenUrlRepository(IRepository<ShortenUrl> repository) 
        {
            _repository = repository;
        }
        public ShortenUrl InsertAsync(ShortenUrl model)
        {
            _repository.Add(model);
            return model;
        }
        public ShortenUrl GetUrlByIdAsync(string id)
        {
            var result = _repository.Get(c => c.OriginalUrl == id);
            return result;
        }
    }
}
