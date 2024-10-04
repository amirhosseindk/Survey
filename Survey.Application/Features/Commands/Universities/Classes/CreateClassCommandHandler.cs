using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.Application.Features.Commands.Universities.Classes
{
    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, ResultDto<int>>
    {
        private readonly IUniversityService _universityService;

        public CreateClassCommandHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<int>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            var result = await _universityService.CreateClassAsync(new Class
            {
                Name = request.Name,
                Course = new Course { Id = request.CourseId }
            });

            return result.ToResultDto();
        }
    }
}