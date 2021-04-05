using System;
using System.Collections.Generic;
using System.Text;

namespace Eatigo.Eatilink.Domain.Interfaces
{
    public interface ILinkShortenService
    {
        string GenerateShortUrl();
        int ShortUrlToId(string shortUrl);
        string IdToBase62(int number);
    }
}
