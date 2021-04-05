using Eatigo.Eatilink.DataObjects.Models;
using Eatigo.Eatilink.Entities.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eatigo.Eatilink.Domain.Mappers
{
    public static class ShortenUrlMapper
    {
        public static ShortenUrl ToEntity(this ShortUrlRequest model, string base62ShortUrl, long uniqueId, int days)
        {
            return new ShortenUrl()
            {
                UniqueId = uniqueId,
                OriginalUrl = model.OriginalUrl,
                ShortUrl = "https://eati.go" + "/" + base62ShortUrl,
                Domain = "eati.go",
                ShortString = base62ShortUrl,
                CreateDate = DateTime.Now.ToString(),
                ExpiryDate = DateTime.Now.AddDays(days).ToString()
            };
        }
        public static ShortUrlResponse ToObject(this ShortenUrl model)
        {
            return new ShortUrlResponse()
            {
                OriginalUrl = model.OriginalUrl,
                ShortUrl = model.ShortUrl
            };
        }
    }
}
