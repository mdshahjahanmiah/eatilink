using Eatigo.Eatilink.DataObjects.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eatigo.Eatilink.Domain.Interfaces
{
    public interface IAutoRefreshingCacheService
    {
        Task<UrlDto> GetUrlsAsync();
    }
}
