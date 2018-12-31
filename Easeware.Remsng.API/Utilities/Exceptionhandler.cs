using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Easeware.Remsng.API.Utilities
{
    public static class ExceptionHandler
    {
        public static ResponseModel Get(this HttpContext context, Exception ex)
        {
            ResponseModel responseModel = new ResponseModel();
            if (ex.GetType() == typeof(BadRequestException))
            {
                responseModel.description = ex.Message;
                responseModel.code = ResponseCode.BAD_REQUEST;
                context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
            }
            else if (ex.GetType() == typeof(ModelValidationException))
            {
                responseModel.description = ex.Message;
                responseModel.code = ResponseCode.MODEL_VALIDATION;
                context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
            }
            else if (ex.GetType() == typeof(SecurityTokenExpiredException))
            {
                responseModel.description = "Login validation expired";
                responseModel.code = ResponseCode.TOKEN_EXPIRED;
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else if (ex.GetType() == typeof(UnknownException))
            {
                responseModel.description = ex.Message ?? $"An unexpected error occured. " +
                    $"Please try again or contact administrator if issue persist";
                responseModel.code = ResponseCode.UNKNOWN;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else if (ex.GetType() == typeof(NotFoundException))
            {
                responseModel.description = ex.Message;
                responseModel.code = ResponseCode.NOTFOUND;
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (ex.GetType() == typeof(SessionExpiredException))
            {
                responseModel.description = "Session has expired";
                responseModel.code = ResponseCode.SESSION_EXPIRED;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (ex.GetType() == typeof(UnknownException))
            {
                responseModel.description = ex.Message ?? $"An unexpected error occured. " +
                    $"Please try again or contact administrator if issue persist";
                responseModel.code = ResponseCode.SESSION_EXPIRED;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                responseModel.code = ResponseCode.GENERAL_ERROR;
                responseModel.description = $"An unexpected error occured. " +
                    $"Please try again or contact administrator if issue persist";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return responseModel;
        }
    }
}
