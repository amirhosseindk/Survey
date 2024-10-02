using Survey.Application.Dtos.Result;
using Survey.Common.Exception;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Survey.WebAPI.Middlewares
{
    public class ExceptionHandlerMiddleware : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";

            ProblemDetails problemDetails;

            if (exception is IBusinessException businessException)
            {
                problemDetails = HandleBusinessException(httpContext, businessException);
            }
            else
            {
                problemDetails = HandleNonBusinessException(httpContext, exception);
            }

            var result = JsonConvert.SerializeObject(new ResultDto
            {
                Succeeded = false,
                Error = problemDetails
            });

            await response.WriteAsync(result, cancellationToken);
            return true;
        }

        private ProblemDetails HandleBusinessException(HttpContext context, IBusinessException businessException)
        {
            var trackId = Guid.NewGuid().ToString();
            _logger.LogError(trackId, businessException, "Business error with track ID: {TrackId}");

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Business error",
                Detail = businessException.ReturnDetail().ToString(),
                Instance = trackId
            };
        }

        private ProblemDetails HandleNonBusinessException(HttpContext context, Exception error)
        {
            var trackId = Guid.NewGuid().ToString();
            _logger.LogError(error, "Global error with track ID: {TrackId}", trackId);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
                Detail = error.Message,
                Instance = trackId
            };
        }
    }
}