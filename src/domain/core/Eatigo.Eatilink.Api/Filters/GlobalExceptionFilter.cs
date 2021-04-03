using Eatigo.Eatilink.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.IdentityModel;

namespace Eatigo.Eatilink.Api.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            string errorCode, message, field;
            HttpStatusCode statusCode;
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.Unauthorized);
                errorCode = ApplicationErrorCodes.Unauthorized;
                field = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.Unauthorized);
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(DivideByZeroException))
            {
                message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InternalSeverError);
                errorCode = ApplicationErrorCodes.InternalSeverError;
                field = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InternalSeverError);
                statusCode = HttpStatusCode.InternalServerError;
            }
            else if (exceptionType == typeof(MongoDB.Driver.MongoConfigurationException))
            {
                message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.DatabaseError);
                errorCode = ApplicationErrorCodes.DatabaseError;
                field = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.DatabaseError);
                statusCode = HttpStatusCode.BadGateway;
            }
            else
            {
                message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.NotFound);
                errorCode = ApplicationErrorCodes.NotFound;
                field = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.NotFound);
                statusCode = HttpStatusCode.NotFound;
            }

            var error = new Common.Exceptions.ApplicationException
            {
                ErrorCode = errorCode,
                Data = new ErrorData(field, message)
            };

            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(error);
        }
    }
}