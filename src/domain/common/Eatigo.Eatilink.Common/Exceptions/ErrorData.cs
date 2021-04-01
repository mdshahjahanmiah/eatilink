using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Eatigo.Eatilink.Common.Exceptions
{
    public class ErrorData
    {
        [JsonPropertyName("field")]
        public string Field { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
