using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;
using Survey.Questionnaires.Types;

namespace Survey.Application.Features.Commands.Questionnaires.CreateQuestionnaire
{
    public class CreateQuestionnaireCommandHandler : IRequestHandler<CreateQuestionnaireCommand, ResultDto<int>>
    {
        private readonly IQuestionnaireService _questionnaireService;

        public CreateQuestionnaireCommandHandler(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        public async Task<ResultDto<int>> Handle(CreateQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = new Questionnaire
            {
                Title = request.Title,
                ClassId = request.ClassId,
                ProfessorId = request.ProffesorId,
                Questions = request.Questions.Select(q => new Question
                {
                    Title = q.Title,
                    Type = (QuestionType)q.Type,
                    Rank = (short)q.Rank,
                    Options = q.Options?.Select(o => new MultipleChoiseOptions
                    {
                        OptionText = o.OptionText
                    }).ToList()
                }).ToList()
            };

            var result = await _questionnaireService.CreateQuestionnaireAsync(questionnaire);
            return result.ToResultDto();
        }
    }
}