using MediatR;
using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnaireByProfessorId
{
    public class GetQuestionnaireByProfessorIdQueryHandler : IRequestHandler<GetQuestionnaireByProfessorIdQuery, ResultDto<IEnumerable<QuestionnaireDto>>>
    {
        private readonly IQuestionnaireService _questionnaireService;
        private readonly IUniversityService _universityService;

        public GetQuestionnaireByProfessorIdQueryHandler(IQuestionnaireService questionnaireService, IUniversityService universityService)
        {
            _questionnaireService = questionnaireService;
            _universityService = universityService;
        }

        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> Handle(GetQuestionnaireByProfessorIdQuery request, CancellationToken cancellationToken)
        {
            var questionnaires = await _questionnaireService.GetAllQuestionnairesByProfessorIdAsync(request.Id);
 
            var result = new List<QuestionnaireDto>();

            foreach (var questionnaire in questionnaires)
            {
                var course = await _universityService.GetCourseWithClassesByIdAsync(questionnaire.ClassId);
                result.Add(new QuestionnaireDto
                {
                    Id = questionnaire.Id,
                    ClassId = questionnaire.ClassId,
                    CourseId = course.Id,
                    ProfessorId = questionnaire.ProfessorId,
                    Title = questionnaire.Title
                });
            }

            return result.AsEnumerable().ToResultDto();
        }
    }
}