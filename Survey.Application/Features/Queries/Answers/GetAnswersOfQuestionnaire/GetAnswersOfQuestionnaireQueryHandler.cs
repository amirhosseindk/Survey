using MediatR;
using Survey.Application.Dtos.Answer;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Application.Features.Queries.Answers.GetAnswersOfQuestionnaire
{
    public class GetAnswersOfQuestionnaireQueryHandler : IRequestHandler<GetAnswersOfQuestionnaireQuery, ResultDto<IEnumerable<AnswerDto>>>
    {
        private readonly IAnswerService _answerService;

        public GetAnswersOfQuestionnaireQueryHandler(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        public async Task<ResultDto<IEnumerable<AnswerDto>>> Handle(GetAnswersOfQuestionnaireQuery request, CancellationToken cancellationToken)
        {
            var answers = new List<AnswerDto>();

            var textAnswers = new List<TextQuestionAnswer>();
            var multipleChoiceAnswers = new List<MultipleChoiceQuestionAnswer>();
            var rangeAnswers = new List<RangeQuestionAnswer>();
            var degreeAnswers = new List<DegreeQuestionAnswer>();

            textAnswers.AddRange(await _answerService.GetTextAnswersByQuestionnaireIdAsync(request.QuestionnaireId));
            multipleChoiceAnswers.AddRange(await _answerService.GetMultipleChoiceAnswersByQuestionnaireIdAsync(request.QuestionnaireId));
            rangeAnswers.AddRange(await _answerService.GetRangeAnswersByQuestionnaireIdAsync(request.QuestionnaireId));
            degreeAnswers.AddRange(await _answerService.GetDegreeAnswersByQuestionnaireIdAsync(request.QuestionnaireId));

            answers.AddRange(textAnswers.Select(a => new AnswerDto
            {
                QuestionnaireId = a.QuestionnaireId,
                QuestionId = a.QuestionId,
                StudentId = a.StudentId,
                Type = 0,
                AnswerText = a.AnswerText,
                FillDateTime = a.FillDateTime
            }));

            answers.AddRange(multipleChoiceAnswers.Select(a => new AnswerDto
            {
                QuestionnaireId = a.QuestionnaireId,
                QuestionId = a.QuestionId,
                StudentId = a.StudentId,
                Type = 1,
                AnswerOptionId = a.AnswerOptionId,
                FillDateTime = a.FillDateTime
            }));

            answers.AddRange(rangeAnswers.Select(a => new AnswerDto
            {
                QuestionnaireId = a.QuestionnaireId,
                QuestionId = a.QuestionId,
                StudentId = a.StudentId,
                Type = 2,
                AnswerValue = a.AnswerValue,
                FillDateTime = a.FillDateTime
            }));

            answers.AddRange(degreeAnswers.Select(a => new AnswerDto
            {
                QuestionnaireId = a.QuestionnaireId,
                QuestionId = a.QuestionId,
                StudentId = a.StudentId,
                Type = 3,
                AnswerValue = a.AnswerValue,
                FillDateTime = a.FillDateTime
            }));

            return answers.AsEnumerable().ToResultDto();
        }
    }
}