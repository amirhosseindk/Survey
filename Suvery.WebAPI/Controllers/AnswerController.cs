using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Dtos.Answer;
using Survey.Application.Dtos.Result;
using Survey.Application.Features.Commands.Answer;
using Suvery.WebAPI.Filters;

namespace Suvery.WebAPI.Controllers
{
    [ApiController]
    [ApiKey]
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
    }
}