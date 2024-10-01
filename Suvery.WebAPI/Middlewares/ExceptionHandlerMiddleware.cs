using Survey.Application.Dtos.Result;
using Survey.Common.Exception;
using Newtonsoft.Json;
using System.Net;

namespace Survey.WebAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                ErrorDto errorDto;

                if (error is IBusinessException businessException)
                {
                    errorDto = HandleBusinessException(context, businessException);
                }
                else
                {
                    errorDto = HandleNonBusinessException(context, error);
                }

                var result = JsonConvert.SerializeObject(new ResultDto
                {
                    Succeeded = false,
                    Error = errorDto
                });

                await response.WriteAsync(result);
            }
        }

        private ErrorDto HandleBusinessException(HttpContext context, IBusinessException businessException)
        {
            var trackId = Guid.NewGuid().ToString();
            GlobalLogger.LogBusinessError(businessException, trackId);

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            SetHeader(context, businessException.GetCode().ToString(), trackId);

            return new ErrorDto
            {
                Code = businessException.GetCode(),
                Message = businessException.Message,
                Details = businessException.ReturnDetail()
            };
        }

        private ErrorDto HandleNonBusinessException(HttpContext context, Exception error)
        {
            var trackId = Guid.NewGuid().ToString();
            GlobalLogger.LogGlobalError(error, trackId);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            SetHeader(context, "0", trackId);

            return new ErrorDto
            {
                Code = context.Response.StatusCode,
                Message = "An unexpected error occurred.",
                Details = error.Message
            };
        }

        private void SetHeader(HttpContext context, string errorCode, string trackId)
        {
            context.Response.Headers.Add("X-Exception-Code", errorCode);
            context.Response.Headers.Add("X-Track-Id", trackId);
        }
    }
}