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
            Domain = string.Empty;
            UserId = string.Empty;
            CreateDate = DateTime.Now.ToString();
        }

        /// <summary>
        ///     Gets and sets the original urlin the current url object.
        /// </summary>
        /// <returns>
        ///     The original url in the current url.
        ///</returns>
        [JsonPropertyName("original_url")]
        public string OriginalUrl { get; set; }
        /// <summary>
        ///     Sets the shorten url in the current url object.
        /// </summary>
        /// <returns>
        ///     The shorten url in the current url.
        ///</returns>
        [JsonPropertyName("short_url")]
        public string ShortUrl { get; set; }
        /// <summary>
        ///     Gets and sets the domain in the current url object.
        /// </summary>
        /// <returns>
        ///     The domain in the current url.
        ///</returns>
        [JsonPropertyName("domain")] 
        public string Domain { get; set; }
        /// <summary>
        ///     Gets and sets the user id or key in the current url object.
        /// </summary>
        /// <returns>
        ///     The user id or key in the current url.
        ///</returns>
        [JsonPropertyName("user_id")] 
        public string UserId { get; set; }
        /// <summary>
        ///     Gets and sets the expiration date in the current url object.
        /// </summary>
        /// <returns>
        ///     The expiration date in the current url.
        ///</returns>
        [JsonPropertyName("create_date")] 
        public string CreateDate { get; set; }
    }
}
