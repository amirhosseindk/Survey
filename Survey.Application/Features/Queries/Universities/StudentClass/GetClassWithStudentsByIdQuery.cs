using Survey.Application.Dtos.Classes;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.StudentClass
{
    public class GetClassWithStudentsByIdQuery : BaseRequest<ResultDto<ClassWithStudentsDto>>
    {
        public int ClassId { get; set; }
    }
}