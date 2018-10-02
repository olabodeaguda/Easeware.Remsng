using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Easeware.Remsng.Common.Exceptions;

namespace Easeware.Remsng.API.Utilities
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Keys
               .SelectMany(key => context.ModelState[key].Errors
               .Select(x =>
                   x.ErrorMessage))
               .ToList();
                if (errors.Count > 0)
                {
                    throw new ModelValidationException(errors.FirstOrDefault());
                }
            }
        }
    }
}
