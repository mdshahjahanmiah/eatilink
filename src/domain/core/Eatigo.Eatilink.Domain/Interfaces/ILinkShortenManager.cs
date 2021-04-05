using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.Entities.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eatigo.Eatilink.Domain.Interfaces
{
    public interface ILinkShortenManager
    {
        ShortUrlResponse Shorten(ShortUrlRequest model);
    }
}
