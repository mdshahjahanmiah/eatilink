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
        public const string InvalidUrl = "INVALID_ORIGINAL_URL";
        public const string InvalidToken = "INVALID_ACCESS_TOKEN";
        public const string DatabaseError = "DATABASE_CONFIGURATION_ERROR";
        public const string InternalSeverError = "INTERNAL_SERVER_ERROR";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string NotFound = "NOT_FOUND";

        public static string GetMessage(string value)
        {
            switch (value)
            {
                case InvalidToken:
                    return "Specify Valid Access Token";
                case OriginalUrl:
                    return "Specify Valid Original Url";
                case InvalidUrl:
                    return "Unable to shorten that link. It is not a valid url.";
                case DatabaseError:
                    return "MongoDB configuration exception";
                case InternalSeverError:
                    return "An attempt to divide an integral or System.Decimal value by zero.";
                case Unauthorized:
                    return "Operating system denies access.";
                case NotFound:
                    return "The requested resource does not exist on the server";
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}
