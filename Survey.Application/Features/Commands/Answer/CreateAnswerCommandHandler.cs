using MediatR;
using Survey.Application.Dtos.Answer;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;
using System.Transactions;

namespace Survey.Application.Features.Commands.Answer
{
    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, ResultDto<bool>>
    {
        private readonly IAnswerService _answerService;

        public CreateAnswerCommandHandler(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        public async Task<ResultDto<bool>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var answer in request.Answers)
                {
                    await ProcessAnswerAsync(answer, request.QuestionnaireId);
                }
                transaction.Complete();
                return true.ToResultDto();
            }
        }

        private async Task ProcessAnswerAsync(CreateAnswerDto answer, int questionnaireId)
        {
            switch (answer.Type)
            {
                case 0:
                    await CreateTextAnswerAsync(answer, questionnaireId);
                    break;
                case 1:
                    await CreateMultipleChoiceAnswerAsync(answer, questionnaireId);
                    break;
                case 2:
                    await CreateRangeAnswerAsync(answer, questionnaireId);
                    break;
                case 3:
                    await CreateDegreeAnswerAsync(answer, questionnaireId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unsupported question type: {answer.Type}");
            }
        }

        private async Task CreateTextAnswerAsync(CreateAnswerDto answer, int questionnaireId)
        {
            if (!string.IsNullOrEmpty(answer.AnswerText))
            {
                await _answerService.CreateTextAnswerAsync(new TextQuestionAnswer
                {
                    QuestionnaireId = questionnaireId,
                    QuestionId = answer.QuestionId,
                    AnswerText = answer.AnswerText,
                    StudentId = answer.StudentId,
                    FillDateTime = answer.FillDateTime
                });
            }
        }

        private async Task CreateMultipleChoiceAnswerAsync(CreateAnswerDto answer, int questionnaireId)
        {
            if (answer.AnswerOptionId.HasValue)
            {
                await _answerService.CreateMultipleChoiceAnswerAsync(new MultipleChoiceQuestionAnswer
                {
                    QuestionnaireId = questionnaireId,
                    QuestionId = answer.QuestionId,
                    AnswerOptionId = answer.AnswerOptionId.Value,
                    StudentId = answer.StudentId,
                    FillDateTime = answer.FillDateTime
                });
            }
        }

        private async Task CreateRangeAnswerAsync(CreateAnswerDto answer, int questionnaireId)
        {
            if (answer.AnswerValue.HasValue)
            {
                await _answerService.CreateRangeAnswerAsync(new RangeQuestionAnswer
                {
                    QuestionnaireId = questionnaireId,
                    QuestionId = answer.QuestionId,
                    AnswerValue = answer.AnswerValue.Value,
                    StudentId = answer.StudentId,
                    FillDateTime = answer.FillDateTime
                });
            }
        }

        private async Task CreateDegreeAnswerAsync(CreateAnswerDto answer, int questionnaireId)
        {
            if (answer.AnswerValue.HasValue)
            {
                await _answerService.CreateDegreeAnswerAsync(new DegreeQuestionAnswer
                {
                    QuestionnaireId = questionnaireId,
                    QuestionId = answer.QuestionId,
                    AnswerValue = answer.AnswerValue.Value,
                    StudentId = answer.StudentId,
                    FillDateTime = answer.FillDateTime
                });
            }
        }
    }
}