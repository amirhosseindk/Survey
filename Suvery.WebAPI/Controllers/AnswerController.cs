using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Dtos.Answer;
using Survey.Application.Dtos.Result;
using Survey.Application.Features.Commands.Answer;
using Survey.Application.Features.Queries.Answers.GetAnswersOfQuestion;
using Survey.Application.Features.Queries.Answers.GetAnswersOfQuestionnaire;
using Survey.Application.Features.Queries.Answers.GetAnswersOfStudent;

namespace Suvery.WebAPI.Controllers
{
    [ApiController]
    public class AnswerController : BaseController
    {
        public AnswerController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("api/[controller]/CreateAnswer/{questionnaireId}")]
        public async Task<ResultDto<bool>> CreateAnswer(int questionnaireId, [FromBody] List<CreateAnswerDto> answers)
        {
            var result = await _mediator.Send(new CreateAnswerCommand
            {
                QuestionnaireId = questionnaireId,
                Answers = answers
            });

            return result;
        }

        [HttpPut]
        [Route("api/[controller]/UpdateAnswer/{questionnaireId}")]
        public async Task<ResultDto<bool>> UpdateAnswer(int questionnaireId, [FromBody] List<UpdateAnswerDto> answers)
        {
            var result = await _mediator.Send(new UpdateAnswerCommand
            {
                QuestionnaireId = questionnaireId,
                Answers = answers
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetAnswersOfStudent/{questionnaireId}/{studentId}")]
        public async Task<ResultDto<IEnumerable<AnswerDto>>> GetAnswersOfStudent(int questionnaireId, string studentId)
        {
            var result = await _mediator.Send(new GetAnswersOfStudentQuery
            {
                QuestionnaireId = questionnaireId,
                StudentId = studentId
            });

            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteAnswersOfStudent/{questionnaireId}/{studentId}")]
        public async Task<ResultDto<bool>> DeleteAnswersOfStudent(int questionnaireId, string studentId)
        {
            var result = await _mediator.Send(new DeleteAnswersOfStudentCommand
            {
                QuestionnaireId = questionnaireId,
                StudentId = studentId
            });
            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetAnswersOfQuestion/{questionnaireId}/{questionId}")]
        public async Task<ResultDto<IEnumerable<AnswerDto>>> GetAnswersOfQuestion(int questionnaireId, int questionId)
        {
            var result = await _mediator.Send(new GetAnswersOfQuestionQuery
            {
                QuestionnaireId = questionnaireId,
                QuestionId = questionId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetAnswersOfQuestionnaire/{questionnaireId}")]
        public async Task<ResultDto<IEnumerable<AnswerDto>>> GetAnswersOfQuestionnaire(int questionnaireId)
        {
            var result = await _mediator.Send(new GetAnswersOfQuestionnaireQuery
            {
                QuestionnaireId = questionnaireId
            });

            return result;
        }
    }
}