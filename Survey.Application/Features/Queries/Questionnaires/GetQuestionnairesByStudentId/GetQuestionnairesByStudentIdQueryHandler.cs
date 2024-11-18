using MediatR;
using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByStudentId
{
    public class GetQuestionnairesByStudentIdQueryHandler : IRequestHandler<GetQuestionnairesByStudentIdQuery, ResultDto<IEnumerable<QuestionnaireDto>>>
    {
        private readonly IQuestionnaireService _questionnaireService;
        private readonly IUniversityService _universityService;

        public GetQuestionnairesByStudentIdQueryHandler(IQuestionnaireService questionnaireService, IUniversityService universityService)
        {
            _questionnaireService = questionnaireService;
            _universityService = universityService;
        }

        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> Handle(GetQuestionnairesByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var questionnaires = await _questionnaireService.GetAllQuestionnairesByStudentIdAsync(request.StudentId);

            var result = await Task.WhenAll(questionnaires.Select(async questionnaire =>
            {
                var course = await _universityService.GetCourseByClassIdAsync(questionnaire.ClassId);
                var @class = await _universityService.GetClassByIdAsync(questionnaire.ClassId);

                return new QuestionnaireDto
                {
                    Id = questionnaire.Id,
                    Title = questionnaire.Title,
                    ClassId = questionnaire.ClassId,
                    ClassName = @class.Name,
                    CourseId = course.Id,
                    CourseName = course.Name,
                    ProfessorId = questionnaire.ProfessorId
                };
            }));

            return result.AsEnumerable().ToResultDto();
        }
    }
}