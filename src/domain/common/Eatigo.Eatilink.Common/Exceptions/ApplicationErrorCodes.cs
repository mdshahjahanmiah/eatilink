using Eatigo.Eatilink.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eatigo.Eatilink.Common.Exceptions
{
    public sealed class ApplicationErrorCodes : StringEnum
    {
        public ApplicationErrorCodes(string value) : base(value)
        {
        }
        public const string OriginalUrl = "EMPTY_ORIGINAL_URL";
        public const string InvalidUrl = "INVALID_URL";
        public const string InvalidToken = "INVALID_ACCESS_TOKEN";

        public static string GetMessage(string value)
        {
            switch (value)
            {
                case InvalidToken:
                    return "Specify Valid Access Token";
                case OriginalUrl:
                    return "Specify Valid Url";
                case InvalidUrl:
                    return "Unable to shorten that link. It is not a valid url.";
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}
