using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Repositories;
using Survey.Questionnaires.Services;

namespace Survey.Questionnaires.Extensions
{
    public static class QuestionnaireExtension
    {
        public static void ConfigureQuestionnaireService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IQuestionnaireConnectionFactory>(con => new QuestionnaireConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ITextQuestionAnswerRepository, TextQuestionAnswerRepository>();
            services.AddScoped<IDegreeQuestionAnswerRepository, DegreeQuestionAnswerRepository>();
            services.AddScoped<IRangeQuestionAnswerRepository, RangeQuestionAnswerRepository>();
            services.AddScoped<IMultipleChoiceQuestionAnswerRepository, MultipleChoiceQuestionAnswerRepository>();
            services.AddScoped<IMultipleChoiceOptionRepository, MultipleChoiceOptionRepository>();
        }
    }
}