using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Easeware.Remsng.API.Utilities
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var val = context.HttpContext.Get(context.Exception);
            var res = JsonConvert.SerializeObject(val,
                                  new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            context.Result = new JsonResult(val);
        }
    }
}
