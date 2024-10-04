using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.ClassCourse
{
    public class GetClassesByCourseNameQueryHandler : IRequestHandler<GetClassesByCourseNameQuery, ResultDto<Dictionary<int, string>>>
    {
        private readonly IUniversityService _universityService;

        public GetClassesByCourseNameQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<Dictionary<int, string>>> Handle(GetClassesByCourseNameQuery request, CancellationToken cancellationToken)
        {
            var classes = await _universityService.GetClassesByCourseNameAsync(request.CourseName);
            return classes.ToResultDto();
        }
    }
}