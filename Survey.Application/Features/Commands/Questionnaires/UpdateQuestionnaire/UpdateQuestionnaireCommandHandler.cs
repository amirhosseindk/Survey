using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;
using Survey.Questionnaires.Types;

namespace Survey.Application.Features.Commands.Questionnaires.UpdateQuestionnaire
{
    public class UpdateQuestionnaireCommandHandler : IRequestHandler<UpdateQuestionnaireCommand, ResultDto<bool>>
    {
        private readonly IQuestionnaireService _questionnaireService;

        public UpdateQuestionnaireCommandHandler(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        public async Task<ResultDto<bool>> Handle(UpdateQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = new Questionnaire
            {
                Id = request.Id,
                Title = request.Title,
                ClassId = request.ClassId,
                ProfessorId = request.ProffesorId,
                Questions = request.Questions.Select(q => new Question
                {
                    Id = q.Id,
                    Title = q.Title,
                    Type = (QuestionType)q.Type,
                    Rank = (short)q.Rank,
                    Options = q.Options?.Select(o => new MultipleChoiseOptions
                    {
                        QuestionId = o.QuestionId,
                        OptionText = o.OptionText
                    }).ToList()
                }).ToList()
            };

            var result = await _questionnaireService.UpdateQuestionnaireAsync(questionnaire);
            return result.ToResultDto();
        }
    }
}