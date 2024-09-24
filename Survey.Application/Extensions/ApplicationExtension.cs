using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Survey.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}