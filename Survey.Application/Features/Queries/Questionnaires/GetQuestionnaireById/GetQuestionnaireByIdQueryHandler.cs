using MediatR;
using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Questions;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnaireById
{
    public class GetQuestionnaireByIdQueryHandler : IRequestHandler<GetQuestionnaireByIdQuery, ResultDto<QuestionnaireDto>>
    {
        private readonly IQuestionnaireService _questionnaireService;

        public GetQuestionnaireByIdQueryHandler(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        public async Task<ResultDto<QuestionnaireDto>> Handle(GetQuestionnaireByIdQuery request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionnaireService.GetQuestionnaireByIdAsync(request.Id);

            var result = new QuestionnaireDto
            {
                Id = questionnaire.Id,
                ClassId = questionnaire.ClassId,
                ProfessorId = questionnaire.ProfessorId,
                Title = questionnaire.Title,
                Questions = questionnaire.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    Type = (int)q.Type,
                    Rank = q.Rank,
                    QuestionnaireId = q.QuestionnaireId,
                    Options = q.Options?.Select(o => new MultipleChoiseOptionsDto()
                    {
                        QuestionId = o.QuestionId,
                        OptionText = o.OptionText
                    }).ToList()
                }).ToList()
            };

            return result.ToResultDto();
        }
    }
}