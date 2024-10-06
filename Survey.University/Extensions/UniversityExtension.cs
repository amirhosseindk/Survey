using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Survey.University.Contracts;
using Survey.University.Repositories;
using Survey.University.Services;

namespace Survey.University.Extensions
{
    public static class UniversityExtension
    {
        public static void ConfigureUniversityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUniversityReadConnectionFactory>(con => new UniversityReadConnectionFactory(configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IUniversityWriteConnectionFactory>(con => new UniversityWriteConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IClassRepository, ClassRepository>();
            services.AddSingleton<ICourseRepository, CourseRepository>();

            services.AddScoped<IUniversityService, UniversityService>();
        }
    }
}