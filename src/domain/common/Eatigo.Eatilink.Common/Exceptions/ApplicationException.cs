using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Eatigo.Eatilink.Common.Exceptions
{
    public class ApplicationException
    {
        public ApplicationException()
        {
            ErrorCode = string.Empty;
            Data = new ErrorData();
        }
        public ApplicationException(string errorCode, ErrorData errorData)
        {
            ErrorCode = errorCode;
            Data = new ErrorData(errorData.Field, errorData.Message);
        }

        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("data")]
        public ErrorData Data { get; set; }
    }
}
