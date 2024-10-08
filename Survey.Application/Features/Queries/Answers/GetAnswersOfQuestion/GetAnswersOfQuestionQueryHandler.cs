using MediatR;
using Survey.Application.Dtos.Answer;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;
using Survey.Questionnaires.Types;

namespace Survey.Application.Features.Queries.Answers.GetAnswersOfQuestion
{
    public class GetAnswersOfQuestionQueryHandler : IRequestHandler<GetAnswersOfQuestionQuery, ResultDto<IEnumerable<AnswerDto>>>
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionnaireService _questionnaireService;

        public GetAnswersOfQuestionQueryHandler(IAnswerService answerService, IQuestionnaireService questionnaireService)
        {
            _answerService = answerService;
            _questionnaireService = questionnaireService;
        }

        public async Task<ResultDto<IEnumerable<AnswerDto>>> Handle(GetAnswersOfQuestionQuery request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionnaireService.GetQuestionnaireByIdAsync(request.QuestionnaireId);

            var questionType = questionnaire.Questions.Find(q => q.Id == request.QuestionId).Type;
            if (questionType == null)
                throw new Exception("there isnt any question");

            var answers = new List<AnswerDto>();

            var textAnswers = new List<TextQuestionAnswer>();
            var multipleChoiceAnswers = new List<MultipleChoiceQuestionAnswer>();
            var rangeAnswers = new List<RangeQuestionAnswer>();
            var degreeAnswers = new List<DegreeQuestionAnswer>();

            switch (questionType)
            {
                case QuestionType.Text:
                    textAnswers.AddRange(await _answerService.GetTextAnswersByQuestionIdAsync(request.QuestionnaireId, request.QuestionId));
                    answers.AddRange(textAnswers.Select(a => new AnswerDto
                    {
                        QuestionnaireId = a.QuestionnaireId,
                        QuestionId = a.QuestionId,
                        StudentId = a.StudentId,
                        Type = 0,
                        AnswerText = a.AnswerText,
                        FillDateTime = a.FillDateTime
                    }));
                    return answers.AsEnumerable().ToResultDto();
                case QuestionType.MultipleChoice:
                    multipleChoiceAnswers.AddRange(await _answerService.GetMultipleChoiceAnswersByQuestionIdAsync(request.QuestionnaireId, request.QuestionId));
                    answers.AddRange(multipleChoiceAnswers.Select(a => new AnswerDto
                    {
                        QuestionnaireId = a.QuestionnaireId,
                        QuestionId = a.QuestionId,
                        StudentId = a.StudentId,
                        Type = 1,
                        AnswerOptionId = a.AnswerOptionId,
                        FillDateTime = a.FillDateTime
                    }));
                    return answers.AsEnumerable().ToResultDto();
                case QuestionType.Range:
                    rangeAnswers.AddRange(await _answerService.GetRangeAnswersByQuestionIdAsync(request.QuestionnaireId, request.QuestionId));
                    answers.AddRange(rangeAnswers.Select(a => new AnswerDto
                    {
                        QuestionnaireId = a.QuestionnaireId,
                        QuestionId = a.QuestionId,
                        StudentId = a.StudentId,
                        Type = 2,
                        AnswerValue = a.AnswerValue,
                        FillDateTime = a.FillDateTime
                    }));
                    return answers.AsEnumerable().ToResultDto();
                case QuestionType.Degree:
                    degreeAnswers.AddRange(await _answerService.GetDegreeAnswersByQuestionIdAsync(request.QuestionnaireId, request.QuestionId));
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
                default:
                    throw new Exception("there is no such a question type");
            }
        }
    }
}