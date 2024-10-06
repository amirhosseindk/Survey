using MediatR;
using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByClassId
{
    public class GetQuestionnairesByClassIdQueryHandler : IRequestHandler<GetQuestionnairesByClassIdQuery, ResultDto<IEnumerable<QuestionnaireDto>>>
    {
        private readonly IQuestionnaireService _questionnaireService;

        public GetQuestionnairesByClassIdQueryHandler(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> Handle(GetQuestionnairesByClassIdQuery request, CancellationToken cancellationToken)
        {
            var questionnaires = await _questionnaireService.GetAllQuestionnairesByClassIdAsync(request.ClassId);

            var result = questionnaires.Select(questionnaire => new QuestionnaireDto
            {
                Id = questionnaire.Id,
                Title = questionnaire.Title,
                ClassId = questionnaire.ClassId,
                ProfessorId = questionnaire.ProfessorId
            }).ToList();

            return result.AsEnumerable().ToResultDto();
        }
    }
}