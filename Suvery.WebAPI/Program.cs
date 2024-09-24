using Survey.Application.Extensions;
using Survey.Questionnaires.Extensions;
using Survey.University.Extensions;
using Survey.Users.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureQuestionnaireService(builder.Configuration);
builder.Services.ConfigureUniversityService(builder.Configuration);
builder.Services.ConfigureUserService(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();