using MediatR;
using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByStudentId
{
    public class GetQuestionnairesByStudentIdQueryHandler : IRequestHandler<GetQuestionnairesByStudentIdQuery, ResultDto<IEnumerable<QuestionnaireDto>>>
    {
        private readonly IQuestionnaireService _questionnaireService;

        public GetQuestionnairesByStudentIdQueryHandler(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> Handle(GetQuestionnairesByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var questionnaires = await _questionnaireService.GetAllQuestionnairesByStudentIdAsync(request.StudentId);

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