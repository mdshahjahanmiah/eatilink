using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Eatigo.Eatilink.Common.Exceptions
{
    public class ApplicationException
    {
        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("data")]
        public ErrorData Data { get; set; }
    }
}
