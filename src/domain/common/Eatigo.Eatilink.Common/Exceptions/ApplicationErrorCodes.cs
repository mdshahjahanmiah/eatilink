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
                    return EatilinkErrorMessage.InvalidToken;
                case OriginalUrl:
                    return EatilinkErrorMessage.OriginalUrl;
                case InvalidUrl:
                    return EatilinkErrorMessage.InvalidUrl;
                case DatabaseError:
                    return EatilinkErrorMessage.DatabaseError;
                case InternalSeverError:
                    return EatilinkErrorMessage.InternalSeverError;
                case Unauthorized:
                    return EatilinkErrorMessage.Unauthorized;
                case NotFound:
                    return EatilinkErrorMessage.NotFound;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}
