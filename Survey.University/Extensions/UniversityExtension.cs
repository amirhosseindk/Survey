using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.University.Contracts;
using Survey.University.Repositories;
using Survey.University.Services;

namespace Survey.University.Extensions
{
    public static class UniversityExtension
    {
        public static void ConfigureUniversityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUniversityConnectionFactory>(con => new UniversityConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IClassStudentsRepository, ClassStudentsRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            services.AddScoped<IUniversityService, UniversityService>();
        }
    }
}