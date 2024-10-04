using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.Application.Features.Commands.Universities.Courses
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, ResultDto<bool>>
    {
        private readonly IUniversityService _universityService;

        public UpdateCourseCommandHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<bool>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _universityService.UpdateCourseAsync(new Course 
            {
                Id = request.CourseId,
                Name = request.Name,
                ProfessorId = request.ProfessorId
            });
            return result.ToResultDto();
        }
    }
}