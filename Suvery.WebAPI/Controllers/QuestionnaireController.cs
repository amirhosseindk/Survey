using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;
using Survey.Application.Features.Commands.Questionnaires.CreateQuestionnaire;
using Survey.Application.Features.Commands.Questionnaires.DeleteQuestionnaire;
using Survey.Application.Features.Commands.Questionnaires.UpdateQuestionnaire;
using Survey.Application.Features.Queries.Questionnaires.GetAllQuestionnaires;
using Survey.Application.Features.Queries.Questionnaires.GetQuestionnaireById;
using Survey.Application.Features.Queries.Questionnaires.GetQuestionnaireByName;
using Survey.Application.Features.Queries.Questionnaires.GetQuestionnaireByProfessorId;
using Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByClassId;
using Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByCourseId;
using Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByStudentId;
using Suvery.WebAPI.Filters;

namespace Suvery.WebAPI.Controllers
{
    [ApiController]
    [ApiKey]
    public class QuestionnaireController : BaseController
    {
        public QuestionnaireController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("api/[controller]/CreateQuestionnaire")]
        public async Task<ResultDto<int>> CreateQuestionnaire([FromBody] CreateQuestionnaireDto dto)
        {
            var result = await _mediator.Send(new CreateQuestionnaireCommand
            {
                Title = dto.Title,
                Questions = dto.Questions,
                ClassId = dto.ClassId,
                ProffesorId = dto.ProffesorId
            });
            
            return result;
        }

        [HttpPut]
        [Route("api/[controller]/UpdateQuestionnaire")]
        public async Task<ResultDto<bool>> UpdateQuestionnaire([FromBody] UpdateQuestionnaireDto dto)
        {
            var result = await _mediator.Send(new UpdateQuestionnaireCommand
            {
                Id = dto.Id,
                Title = dto.Title,
                Questions = dto.Questions,
                ClassId = dto.ClassId,
                ProffesorId = dto.ProffesorId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetQuestionnaireById")]
        public async Task<ResultDto<QuestionnaireDto>> GetQuestionnaireById(int id)
        {
            var result = await _mediator.Send(new GetQuestionnaireByIdQuery
            {
                Id = id
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetQuestionnaireByTitle")]
        public async Task<ResultDto<QuestionnaireDto>> GetQuestionnaireByTitle(string title)
        {
            var result = await _mediator.Send(new GetQuestionnaireByTitleQuery
            {
                Title = title
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetAllQuestionnaires")]
        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> GetAllQuestionnaires()
        {
            var result = await _mediator.Send(new GetAllQuestionnairesQuery());

            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteQuestionnaire")]
        public async Task<ResultDto<bool>> DeleteQuestionnaire(int id)
        {
            var result = await _mediator.Send(new DeleteQuestionnaireCommand
            {
                Id = id
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetQuestionnaireByProfessorId")]
        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> GetQuestionnaireByProfessorId(string id)
        {
            var result = await _mediator.Send(new GetQuestionnaireByProfessorIdQuery
            {
                Id = id
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetQuestionnairesByClassId")]
        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> GetQuestionnairesByClassId(int classId)
        {
            var result = await _mediator.Send(new GetQuestionnairesByClassIdQuery
            {
                ClassId = classId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetQuestionnairesByCourseId")]
        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> GetQuestionnairesByCourseId(int courseId)
        {
            var result = await _mediator.Send(new GetQuestionnairesByCourseIdQuery
            {
                CourseId = courseId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetQuestionnairesByStudentId")]
        public async Task<ResultDto<IEnumerable<QuestionnaireDto>>> GetQuestionnairesByStudentId(string studentId)
        {
            var result = await _mediator.Send(new GetQuestionnairesByStudentIdQuery
            {
                StudentId = studentId
            });

            return result;
        }
    }
}