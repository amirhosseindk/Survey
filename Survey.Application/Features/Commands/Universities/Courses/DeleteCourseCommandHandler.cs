using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Commands.Universities.Courses
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, ResultDto<bool>>
    {
        private readonly IUniversityService _universityService;

        public DeleteCourseCommandHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _universityService.DeleteCourseAsync(request.CourseId);
            return result.ToResultDto();
        }
    }
}