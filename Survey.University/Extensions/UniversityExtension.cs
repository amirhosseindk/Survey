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

            services.AddSingleton<IClassRepository, ClassRepository>(service =>
            {
                var readConnectionFactory = service.GetRequiredService<IUniversityReadConnectionFactory>();
                var writeConnectionFactory = service.GetRequiredService<IUniversityWriteConnectionFactory>();
                var logger = service.GetRequiredService<ILogger<ClassRepository>>();
                return new ClassRepository(readConnectionFactory, writeConnectionFactory, logger);
            });

            services.AddSingleton<ICourseRepository, CourseRepository>(service =>
            {
                var readConnectionFactory = service.GetRequiredService<IUniversityReadConnectionFactory>();
                var writeConnectionFactory = service.GetRequiredService<IUniversityWriteConnectionFactory>();
                var logger = service.GetRequiredService<ILogger<CourseRepository>>();
                return new CourseRepository(readConnectionFactory, writeConnectionFactory, logger);
            });

            services.AddScoped<IUniversityService, UniversityService>();
        }
    }
}