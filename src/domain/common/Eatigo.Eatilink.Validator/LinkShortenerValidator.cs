using Eatigo.Eatilink.Common.Exceptions;
using Eatigo.Eatilink.DataObjects.Settings;
using Eatigo.Eatilink.Security.Handlers;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.RegularExpressions;
using ApplicationException = Eatigo.Eatilink.Common.Exceptions.ApplicationException;

namespace Eatigo.Eatilink.Validator
{
    public class LinkShortenerValidator : ILinkShortenerValidator
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly AppSettings _appSettings;
        public LinkShortenerValidator(IJwtTokenHandler jwtTokenHandler, AppSettings appSettings) 
        {
            _jwtTokenHandler = jwtTokenHandler;
            _appSettings = appSettings;
        }

        (int, ApplicationException) ILinkShortenerValidator.PayloadValidator(string accessToken, string originalUrl, string domain)
        {
            int statusCode = StatusCodes.Status200OK;
            ApplicationException result = null;

            if (_appSettings.JsonWebTokens.IsEnabled)
            {
                var token = _jwtTokenHandler.PrepareTokenFromAccessToekn(accessToken);
                if (string.IsNullOrEmpty(token))
                {
                    statusCode = StatusCodes.Status401Unauthorized;
                    result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidToken, Data = new ErrorData() { Field = "Token", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidToken) } };
                    return (statusCode, result);
                }
                var (isVerified, userId) = _jwtTokenHandler.VerifyJwtSecurityToken(token);
                if ((!isVerified) || string.IsNullOrEmpty(userId))
                {
                    statusCode = StatusCodes.Status401Unauthorized;
                    result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidToken, Data = new ErrorData() { Field = "Token", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidToken) } };
                    return (statusCode, result);
                }
            }
            if (string.IsNullOrEmpty(originalUrl))
            {
                statusCode = StatusCodes.Status422UnprocessableEntity;
                result = new ApplicationException { ErrorCode = ApplicationErrorCodes.OriginalUrl, Data = new ErrorData() { Field = "original_url", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.OriginalUrl) } };
            }
            else if (!Uri.IsWellFormedUriString(originalUrl, UriKind.Absolute)) 
            {
                statusCode = StatusCodes.Status422UnprocessableEntity;
                result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidUrl, Data = new ErrorData() { Field = "original_url", Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidUrl) } };
            }
            return (statusCode, result);
        }
    }
}
