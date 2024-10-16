using WebApp;
using Survey.Application.Extensions;
using Survey.Questionnaires.Extensions;
using Survey.University.Extensions;
using Survey.Users.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureQuestionnaireService(builder.Configuration);
builder.Services.ConfigureUniversityService(builder.Configuration);
builder.Services.ConfigureUserService(builder.Configuration);

// Exception Handling Middleware
//builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
//builder.Services.AddProblemDetails();

builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<IStatisticalAnalysisService, StatisticalAnalysisService>();

var app = builder.Build();

// Seed roles and admin user
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        await DataSeeder.SeedRolesAndAdminAsync(services);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred while seeding the database.");
//    }
//}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}");

app.Run();