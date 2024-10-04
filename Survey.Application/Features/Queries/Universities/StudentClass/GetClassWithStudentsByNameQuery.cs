using Survey.Application.Dtos.Classes;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.StudentClass
{
    public class GetClassWithStudentsByNameQuery : BaseRequest<ResultDto<ClassWithStudentsDto>>
    {
        public string ClassName { get; set; }
    }
}