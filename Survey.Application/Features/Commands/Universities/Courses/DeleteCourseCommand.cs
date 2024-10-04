using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Universities.Courses
{
    public class DeleteCourseCommand : BaseRequest<ResultDto<bool>>
    {
        public int CourseId { get; set; }
    }
}