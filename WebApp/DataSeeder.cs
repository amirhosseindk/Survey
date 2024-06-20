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
                UserName = "admin",
                Email = "admin@example.com",
                EmailConfirmed = true,
                IsProfessor = true,
                StudentNumber = "4003333004",
                Field = "CS"
            };

            string adminPassword = "Admin@123";

            var admin = await userManager.FindByEmailAsync(adminUser.Email);
            if (admin == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Professor");

                    var course = new Course
                    {
                        Name = "Ehtemal-CS",
                        ProfessorId = adminUser.Id
                    };

                    context.Courses.Add(course);
                    await context.SaveChangesAsync();
                }
            }
            else
            {
                if (!context.Courses.Any(c => c.Name == "Ehtemal-CS"))
                {
                    var course = new Course
                    {
                        Name = "Ehtemal-CS",
                        ProfessorId = admin.Id
                    };

                    context.Courses.Add(course);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}