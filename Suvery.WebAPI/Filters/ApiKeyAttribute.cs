using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Suvery.WebAPI.Filters
{
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string API_KEY_HEADER_NAME = "X-API-KEY";
        private const string VALID_API_KEY = "{8C1DD003-9042-4E17-B71A-21F8A708AFC4}";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = "API Key was not provided."
                };
                return;
            }

            if (!VALID_API_KEY.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = "Unauthorized client."
                };
                return;
            }

            await next();
        }
    }
}