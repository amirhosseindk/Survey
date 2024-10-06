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
            services.AddSingleton<IQuestionnaireReadConnectionFactory>(con =>
                new QuestionnaireReadConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IQuestionnaireWriteConnectionFactory>(con =>
                new QuestionnaireWriteConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IQuestionnaireRepository, QuestionnaireRepository>();
            services.AddSingleton<IQuestionRepository, QuestionRepository>();
            services.AddSingleton<ITextQuestionAnswerRepository, TextQuestionAnswerRepository>();
            services.AddSingleton<IDegreeQuestionAnswerRepository, DegreeQuestionAnswerRepository>();
            services.AddSingleton<IRangeQuestionAnswerRepository, RangeQuestionAnswerRepository>();
            services.AddSingleton<IMultipleChoiceQuestionAnswerRepository, MultipleChoiceQuestionAnswerRepository>();
            services.AddSingleton<IMultipleChoiceOptionRepository, MultipleChoiceOptionRepository>();

            services.AddScoped<IQuestionnaireService, QuestionnaireService>();
            services.AddScoped<IAnswerService, AnswerService>();
        }
    }
}