using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Eatigo.Eatilink.DataObjects.Models
{
    /// <summary>
    ///   Represents url entity as a sequence of url units.
    ///</summary>
    public class ShortUrlRequest
    {
        /// <summary>
        ///     Initializes a new instance of the url class to the value indicated
        ///     by all members.
        /// </summary>
        public ShortUrlRequest() 
        {
            OriginalUrl = string.Empty;
        }

        /// <summary>
        ///     Gets and sets the original urlin the current url object.
        /// </summary>
        /// <returns>
        ///     The original url in the current url.
        ///</returns>
        [JsonPropertyName("original_url")]
        public string OriginalUrl { get; set; }
    }
}
