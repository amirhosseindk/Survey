using MediatR;
using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;

namespace Survey.Application.Features.Queries.Questionnaires.GetAllQuestionnaires
{
    public class GetAllQuestionnairesQueryHandler : IRequestHandler<GetAllQuestionnairesQuery, ResultDto<IEnumerable<QuestionnaireDto>>>
    {
        private readonly IQuestionnaireService _questionnaireService;

        public GetAllQuestionnairesQueryHandler(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> Handle(GetAllQuestionnairesQuery request, CancellationToken cancellationToken)
        {
            var questionnaires = await _questionnaireService.GetAllQuestionnairesAsync();

            var result = new List<QuestionnaireDto>();

            foreach (var questionnaire in questionnaires)
            {
                result.Add(new QuestionnaireDto
                {
                    Id = questionnaire.Id,
                    ClassId = questionnaire.ClassId,
                    ProfessorId = questionnaire.ProfessorId,
                    Title = questionnaire.Title
                });
            }

            return result.AsEnumerable().ToResultDto();
        }
    }
}