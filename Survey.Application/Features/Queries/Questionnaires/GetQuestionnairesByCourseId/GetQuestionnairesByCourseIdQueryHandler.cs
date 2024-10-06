using MediatR;
using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByCourseId
{
    public class GetQuestionnairesByCourseIdQueryHandler : IRequestHandler<GetQuestionnairesByCourseIdQuery, ResultDto<IEnumerable<QuestionnaireDto>>>
    {
        private readonly IQuestionnaireService _questionnaireService;
        private readonly IUniversityService _universityService;

        public GetQuestionnairesByCourseIdQueryHandler(IQuestionnaireService questionnaireService, IUniversityService universityService)
        {
            _questionnaireService = questionnaireService;
            _universityService = universityService;
        }

        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> Handle(GetQuestionnairesByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var @class = await _universityService.GetClassByCourseIdAsync(request.CourseId);

            var questionnaires = await _questionnaireService.GetAllQuestionnairesByClassIdAsync(@class.Id);

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