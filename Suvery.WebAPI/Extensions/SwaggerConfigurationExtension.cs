using Microsoft.OpenApi.Models;
using Suvery.WebAPI.Options;

namespace Suvery.WebAPI.Extensions
{
    public static class SwaggerConfigurationExtension
    {
        public static void ConfigureSwaggerService(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            var options = new SwaggerDefOptions();
            configuration.GetSection("SwaggerDefOptions").Bind(options);

            if (!options.Enabled)
                return;

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.DocumentVersion, new OpenApiInfo
                {
                    Version = options.Version,
                    Title = options.DocumentTitle
                });
                c.AddSecurityDefinition("X-API-KEY", new OpenApiSecurityScheme
                {
                    Name = "X-API-KEY",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "X-API-KEY",
                    Description = "Input your API key in this format - X-API-KEY {your key here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "X-API-KEY",
                            },
                            Scheme = "X-API-KEY",
                            Name = "X-API-KEY",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
                c.CustomSchemaIds(type => type.ToString());
            });
        }

        public static void ConfigureSwaggerApplication(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Survey API's");
            });
        }
    }
}