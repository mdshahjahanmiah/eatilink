using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Eatigo.Eatilink.DataObjects.Models
{
    /// <summary>
    ///   Represents url entity as a sequence of url units.
    ///</summary>
    public class UrlDto
    {
        /// <summary>
        ///     Initializes a new instance of the url class to the value indicated
        ///     by all members.
        /// </summary>
        public UrlDto() 
        {
            OriginalUrl = string.Empty;
            Domain = string.Empty;
            UserId = string.Empty;
            ExpireDate = DateTime.Now.ToString();
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
        [JsonPropertyName("expire_date")] 
        public string ExpireDate { get; set; }
    }
}
