using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.Entities.Link;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eatigo.Eatilink.Infrastructure.Repository
{
    public interface IRepositoryLinkShortenUrl
    {
        ShortenUrl GetUrlByIdAsync(string id);
        ShortenUrl InsertAsync(ShortenUrl model);
    }
}
