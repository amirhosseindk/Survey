using MediatR;
using Survey.Application.Dtos.Classes;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.Classes
{
    public class GetClassByNameQueryHandler : IRequestHandler<GetClassByNameQuery, ResultDto<ClassDto>>
    {
        private readonly IUniversityService _universityService;

        public GetClassByNameQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<ClassDto>> Handle(GetClassByNameQuery request, CancellationToken cancellationToken)
        {
            var @class = await _universityService.GetClassByNameAsync(request.ClassName);

            var result = new ClassDto
            {
                Id = @class.Id,
                CourseId = @class.Course.Id,
                CourseName = @class.Course.Name,
                Name = @class.Name
            };

            return result.ToResultDto();
        }
    }
}