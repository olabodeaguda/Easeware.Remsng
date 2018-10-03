using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Easeware.Remsng.API.Utilities
{
    public static class ExceptionHandler
    {
        public static string Get(this HttpContext context, Exception ex)
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
            else if (ex.GetType() == typeof(UnknownException))
            {
                responseModel.description = ex.Message ?? $"An unexpected error occured. " +
                    $"Please try again or contact administrator if issue persist";
                responseModel.code = ResponseCode.UNKNOWN;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else
            {
                responseModel.code = ResponseCode.GENERAL_ERROR;
                responseModel.description = $"An unexpected error occured. " +
                    $"Please try again or contact administrator if issue persist";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return JsonConvert.SerializeObject(responseModel,
               new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
