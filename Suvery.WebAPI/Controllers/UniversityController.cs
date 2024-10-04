using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Dtos.Classes;
using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;
using Survey.Application.Dtos.StudentClass;
using Survey.Application.Features.Commands.Universities.Classes;
using Survey.Application.Features.Commands.Universities.Courses;
using Survey.Application.Features.Commands.Universities.StudentClass;
using Survey.Application.Features.Queries.Universities.ClassCourse;
using Survey.Application.Features.Queries.Universities.Classes;
using Survey.Application.Features.Queries.Universities.Courses;
using Survey.Application.Features.Queries.Universities.StudentClass;
using Suvery.WebAPI.Filters;

namespace Suvery.WebAPI.Controllers
{
    [ApiController]
    [ApiKey]
    public class UniversityController : BaseController
    {
        public UniversityController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("api/[controller]/CreateCourse")]
        public async Task<ResultDto<int>> CreateCourse([FromBody] CreateCourseDto dto)
        {
            var result = await _mediator.Send(new CreateCourseCommand
            {
                Name = dto.Name,
                ProfessorId = dto.ProfessorId
            });

            return result;
        }

        [HttpPut]
        [Route("api/[controller]/UpdateCourse")]
        public async Task<ResultDto<bool>> UpdateCourse([FromBody] UpdateCourseDto dto)
        {
            var result = await _mediator.Send(new UpdateCourseCommand
            {
                CourseId = dto.Id,
                Name = dto.Name,
                ProfessorId = dto.ProfessorId
            });

            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteCourse")]
        public async Task<ResultDto<bool>> DeleteCourse(int courseId)
        {
            var result = await _mediator.Send(new DeleteCourseCommand
            {
                CourseId = courseId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetCourseById/{courseId}")]
        public async Task<ResultDto<CourseDto>> GetCourseById(int courseId)
        {
            var result = await _mediator.Send(new GetCourseByIdQuery
            {
                CourseId = courseId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetCourseByName/{courseName}")]
        public async Task<ResultDto<CourseDto>> GetCourseByName(string courseName)
        {
            var result = await _mediator.Send(new GetCourseByNameQuery
            {
                CourseName = courseName
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetAllCourses")]
        public async Task<ResultDto<IEnumerable<CourseDto>>> GetAllCourses()
        {
            var result = await _mediator.Send(new GetAllCoursesQuery());
            return result;
        }

        [HttpPost]
        [Route("api/[controller]/CreateClass")]
        public async Task<ResultDto<int>> CreateClass([FromBody] CreateClassDto dto)
        {
            var result = await _mediator.Send(new CreateClassCommand
            {
                Name = dto.Name,
                CourseId = dto.CourseId
            });

            return result;
        }

        [HttpPut]
        [Route("api/[controller]/UpdateClass")]
        public async Task<ResultDto<bool>> UpdateClass([FromBody] UpdateClassDto dto)
        {
            var result = await _mediator.Send(new UpdateClassCommand
            {
                Id = dto.Id,
                Name = dto.Name,
                CourseId = dto.CourseId
            });

            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteClass")]
        public async Task<ResultDto<bool>> DeleteClass(int classId)
        {
            var result = await _mediator.Send(new DeleteClassCommand
            {
                ClassId = classId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetClassById/{classId}")]
        public async Task<ResultDto<ClassDto>> GetClassById(int classId)
        {
            var result = await _mediator.Send(new GetClassByIdQuery
            {
                ClassId = classId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetClassByName/{className}")]
        public async Task<ResultDto<ClassDto>> GetClassByName(string className)
        {
            var result = await _mediator.Send(new GetClassByNameQuery
            {
                ClassName = className
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetAllClasses")]
        public async Task<ResultDto<IEnumerable<ClassDto>>> GetAllClasses()
        {
            var result = await _mediator.Send(new GetAllClassesQuery());

            return result;
        }

        [HttpPost]
        [Route("api/[controller]/AddStudentToClass")]
        public async Task<ResultDto<bool>> AddStudentToClass([FromBody] AddStudentToClassDto dto)
        {
            var result = await _mediator.Send(new AddStudentToClassCommand
            {
                StudentId = dto.StudentId,
                ClassId = dto.ClassId
            });

            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/RemoveStudentFromClass")]
        public async Task<ResultDto<bool>> RemoveStudentFromClass([FromBody] RemoveStudentFromClassDto dto)
        {
            var result = await _mediator.Send(new RemoveStudentFromClassCommand
            {
                StudentId = dto.StudentId,
                ClassId = dto.ClassId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetClassesByCourseId/{courseId}")]
        public async Task<ResultDto<Dictionary<int, string>>> GetClassesByCourseId(int courseId)
        {
            var result = await _mediator.Send(new GetClassesByCourseIdQuery
            {
                CourseId = courseId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetClassesByCourseName/{courseName}")]
        public async Task<ResultDto<Dictionary<int, string>>> GetClassesByCourseName(string courseName)
        {
            var result = await _mediator.Send(new GetClassesByCourseNameQuery
            {
                CourseName = courseName
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetStudentsByClassId/{classId}")]
        public async Task<ResultDto<Dictionary<string, string>>> GetStudentsByClassId(int classId)
        {
            var result = await _mediator.Send(new GetStudentsByClassIdQuery
            {
                ClassId = classId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetStudentsByClassName/{className}")]
        public async Task<ResultDto<Dictionary<string, string>>> GetStudentsByClassName(string className)
        {
            var result = await _mediator.Send(new GetStudentsByClassNameQuery
            {
                ClassName = className
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetCourseWithClassesById/{courseId}")]
        public async Task<ResultDto<CourseWithClassesDto>> GetCourseWithClassesById(int courseId)
        {
            var result = await _mediator.Send(new GetCourseWithClassesByIdQuery
            {
                CourseId = courseId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetCourseWithClassesByName/{courseName}")]
        public async Task<ResultDto<CourseWithClassesDto>> GetCourseWithClassesByName(string courseName)
        {
            var result = await _mediator.Send(new GetCourseWithClassesByNameQuery
            {
                CourseName = courseName
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetClassWithStudentsById/{classId}")]
        public async Task<ResultDto<ClassWithStudentsDto>> GetClassWithStudentsById(int classId)
        {
            var result = await _mediator.Send(new GetClassWithStudentsByIdQuery
            {
                ClassId = classId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetClassWithStudentsByName/{className}")]
        public async Task<ResultDto<ClassWithStudentsDto>> GetClassWithStudentsByName(string className)
        {
            var result = await _mediator.Send(new GetClassWithStudentsByNameQuery
            {
                ClassName = className
            });

            return result;
        }
    }
}