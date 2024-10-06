using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;

namespace Survey.Application.Features.Commands.Questionnaires.DeleteQuestionnaire
{
    public class DeleteQuestionnaireCommandHandler : IRequestHandler<DeleteQuestionnaireCommand, ResultDto<bool>>
    {
        private readonly IQuestionnaireService _questionnaireService;

        public DeleteQuestionnaireCommandHandler(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        public async Task<ResultDto<bool>> Handle(DeleteQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var result = await _questionnaireService.DeleteQuestionnaireAsync(request.Id);
            return result.ToResultDto();
        }
    }
}