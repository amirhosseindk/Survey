using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.ClassCourse
{
    public class GetClassesByCourseIdQueryHandler : IRequestHandler<GetClassesByCourseIdQuery, ResultDto<Dictionary<int, string>>>
    {
        private readonly IUniversityService _universityService;

        public GetClassesByCourseIdQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<Dictionary<int, string>>> Handle(GetClassesByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var classes = await _universityService.GetClassesByCourseIdAsync(request.CourseId);
            return classes.ToResultDto();
        }
    }
}