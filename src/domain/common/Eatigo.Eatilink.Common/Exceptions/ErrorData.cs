using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Eatigo.Eatilink.Common.Exceptions
{
    public class ErrorData
    {
        public ErrorData() 
        {
            Field = string.Empty;
            Message = string.Empty;
        }

        public ErrorData(string field, string message)
        {
            Field = field;
            Message = message;
        }

        [JsonPropertyName("field")]
        public string Field { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
