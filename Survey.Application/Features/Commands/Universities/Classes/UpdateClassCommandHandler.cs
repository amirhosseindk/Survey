using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.Application.Features.Commands.Universities.Classes
{
    public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, ResultDto<bool>>
    {
        private readonly IUniversityService _universityService;

        public UpdateClassCommandHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<bool>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var result = await _universityService.UpdateClassAsync(new Class
            {
                Id = request.Id,
                Name = request.Name,
                Course = new Course { Id = request.CourseId }
            });

            return result.ToResultDto();
        }
    }
}