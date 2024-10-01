using Survey.Common.Exception;
using System.Security.Claims;

namespace Survey.WebAPI.Middlewares
{
    public static class GlobalLogger
    {
        private static ILoggerFactory _loggerFactory;

        public static void Configure(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public static void LogBusinessError(IBusinessException exception, string trackId)
        {
            var logger = _loggerFactory.CreateLogger("GlobalLogger");
            logger.LogWarning("Business Error [{TrackId}]: Code {Code} - {Message}", trackId, exception.GetCode(), exception.Message);
        }

        public static void LogGlobalError(Exception exception, string trackId)
        {
            var logger = _loggerFactory.CreateLogger("GlobalLogger");
            logger.LogError(exception, "Global Error [{TrackId}]: {Message}", trackId, exception.Message);
        }
    }
}