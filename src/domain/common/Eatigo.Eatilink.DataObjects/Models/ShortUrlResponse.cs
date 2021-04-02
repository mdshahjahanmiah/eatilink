using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Eatigo.Eatilink.DataObjects.Models
{
    public class ShortUrlResponse
    {
        [JsonPropertyName("original_url")]
        public string OriginalUrl { get; set; }

        [JsonPropertyName("short_url")]
        public string ShortUrl { get; set; }

    }
}
