using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp
{
    public class DataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            string[] roleNames = { "Professor", "Student" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = new User
            {
                UserName = "4003333004",
                Email = "admin@example.com",
                EmailConfirmed = true,
                IsProfessor = true,
                StudentNumber = "4003333004",
            };

            string adminPassword = "Admin@123";

            var admin = await userManager.FindByEmailAsync(adminUser.Email);
            if (admin == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Professor");
                    await SeedCoursesAndClassesAsync(context, adminUser.Id);
                }
            }
            else
            {
                await SeedCoursesAndClassesAsync(context, admin.Id);
            }
        }

        private static async Task SeedCoursesAndClassesAsync(AppDbContext context, string professorId)
        {
            var courses = new[]
            {
                new { Name = "Ehtemal", Classes = new[] { "CS", "CE", "Math" } },
                new { Name = "AP", Classes = new[] { "CS", "Math" } },
                new { Name = "BP", Classes = new[] { "CE", "Math" } },
                new { Name = "Math", Classes = new[] { "CS", "CE", "Math" } },
            };

            foreach (var courseData in courses)
            {
                var course = context.Courses.FirstOrDefault(c => c.Name == courseData.Name && c.ProfessorId == professorId);
                if (course == null)
                {
                    course = new Course
                    {
                        Name = courseData.Name,
                        ProfessorId = professorId
                    };
                    context.Courses.Add(course);
                    await context.SaveChangesAsync();

                    foreach (var className in courseData.Classes)
                    {
                        var classEntity = new Class
                        {
                            Name = className,
                            CourseId = course.Id
                        };
                        context.Classes.Add(classEntity);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}