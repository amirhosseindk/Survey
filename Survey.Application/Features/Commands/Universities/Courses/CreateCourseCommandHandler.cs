using MediatR;
using Survey.Application.Dtos.Result;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.Application.Features.Commands.Universities.Courses
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ResultDto<int>>
    {
        private readonly IUniversityService _universityService;

        public CreateCourseCommandHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<int>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var courseId = await _universityService.CreateCourseAsync(new Course
            {
                Name = request.Name,
                ProfessorId = request.ProfessorId
            });
            return new ResultDto<int> { Succeeded = true, Result = courseId };
        }
    }
}