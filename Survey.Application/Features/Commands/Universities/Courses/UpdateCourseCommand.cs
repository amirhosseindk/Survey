using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Universities.Courses
{
    public class UpdateCourseCommand : BaseRequest<ResultDto<bool>>
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string ProfessorId { get; set; }
    }
}