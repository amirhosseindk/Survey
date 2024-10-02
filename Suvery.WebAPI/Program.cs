using Survey.Application.Extensions;
using Survey.Questionnaires.Extensions;
using Survey.University.Extensions;
using Survey.Users.Extensions;
using Survey.WebAPI.Middlewares;
using Suvery.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger Service Configuration
builder.ConfigureSwaggerService(builder.Configuration);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// CORS Configuration
builder.Services.ConfigureCorsOriginServices(builder.Configuration);

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureQuestionnaireService(builder.Configuration);
builder.Services.ConfigureUniversityService(builder.Configuration);
builder.Services.ConfigureUserService(builder.Configuration);

// Exception Handling Middleware
builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
builder.Services.AddProblemDetails();

// Logging with Log4Net
builder.Logging.AddLog4Net("log4net.config");

var app = builder.Build();

// Swagger Application Configuration
app.ConfigureSwaggerApplication();

// CORS Application Configuration
app.ConfigureCorsOriginApplication();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();