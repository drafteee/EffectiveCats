using EffectiveCats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EffectiveCats.Middlewares
{
    public class ValidationMiddleware : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(y => y.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var e in errors)
                {
                    foreach (var subError in e.Value)
                    {
                        var error = new ErrorModel
                        {
                            FieldName = e.Key,
                            Message = subError
                        };

                        errorResponse.Errors.Add(error);
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}
