using Suvery.WebAPI.Options;

namespace Suvery.WebAPI.Extensions
{
    public static class CorsOriginExtensions
    {
        public static void ConfigureCorsOriginServices(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new CorsOriginOptions();
            configuration.GetSection("CorsOrigin").Bind(options);

            if (!options.AcceptCorsEnable) return;

            services.AddCors(o => o.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(options.AllowCorsOrigin.Split(","))
                    .WithHeaders(options.AllowAnyHeader.Split(","))
                    .WithMethods(options.AllowAnyMethod.Split(","))
                    .AllowCredentials();
            }));
        }

        public static void ConfigureCorsOriginApplication(this WebApplication app)
        {
            var options = new CorsOriginOptions();
            app.Configuration.GetSection("CorsOrigin").Bind(options);

            if (!options.AcceptCorsEnable) return;

            app.UseCors();
        }
    }
}