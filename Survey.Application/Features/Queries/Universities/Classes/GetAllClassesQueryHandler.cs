using MediatR;
using Survey.Application.Dtos.Classes;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.Classes
{
    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, ResultDto<IEnumerable<ClassDto>>>
    {
        private readonly IUniversityService _universityService;

        public GetAllClassesQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<IEnumerable<ClassDto>>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            var classes = await _universityService.GetAllClassesAsync();

            var result = new List<ClassDto>();

            foreach (var @class in classes)
            {
                result.Add(new ClassDto
                {
                    Id = @class.Id,
                    Name = @class.Name,
                    CourseId = @class.Course.Id,
                    CourseName = @class.Course.Name
                });
            }

            return result.AsEnumerable().ToResultDto();
        }
    }
}