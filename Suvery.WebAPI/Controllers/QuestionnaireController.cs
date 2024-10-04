using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;
using Survey.Application.Features.Commands.Questionnaires.CreateQuestionnaire;
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
    }
}