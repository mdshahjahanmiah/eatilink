using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.Entities.Link;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eatigo.Eatilink.Domain.Interfaces
{
    public interface IAutoRefreshingCacheService
    {
        Hashtable CacheValue(string originalUrl, string shortenUrl);
    }
}
