using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Universities.Courses
{
    public class CreateCourseCommand : BaseRequest<ResultDto<int>>
    {
        public string Name { get; set; }
        public string ProfessorId { get; set; }
    }
}