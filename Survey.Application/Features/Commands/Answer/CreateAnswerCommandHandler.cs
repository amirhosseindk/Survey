using MediatR;
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
                    switch (answer.Type)
                    {
                        case 0:
                            if (!string.IsNullOrEmpty(answer.AnswerText))
                            {
                                await _answerService.CreateTextAnswerAsync(new TextQuestionAnswer
                                {
                                    QuestionnaireId = request.QuestionnaireId,
                                    QuestionId = answer.QuestionId,
                                    AnswerText = answer.AnswerText,
                                    StudentId = answer.StudentId,
                                    FillDateTime = answer.FillDateTime
                                });
                            }
                            break;

                        case 1:
                            if (answer.AnswerOptionId.HasValue)
                            {
                                await _answerService.CreateMultipleChoiceAnswerAsync(new MultipleChoiceQuestionAnswer
                                {
                                    QuestionnaireId = request.QuestionnaireId,
                                    QuestionId = answer.QuestionId,
                                    AnswerOptionId = answer.AnswerOptionId.Value,
                                    StudentId = answer.StudentId,
                                    FillDateTime = answer.FillDateTime
                                });
                            }
                            break;

                        case 2:
                            if (answer.AnswerValue.HasValue)
                            {
                                await _answerService.CreateRangeAnswerAsync(new RangeQuestionAnswer
                                {
                                    QuestionnaireId = request.QuestionnaireId,
                                    QuestionId = answer.QuestionId,
                                    AnswerValue = answer.AnswerValue.Value,
                                    StudentId = answer.StudentId,
                                    FillDateTime = answer.FillDateTime
                                });
                            }
                            break;

                        case 3:
                            if (answer.AnswerValue.HasValue)
                            {
                                await _answerService.CreateDegreeAnswerAsync(new DegreeQuestionAnswer
                                {
                                    QuestionnaireId = request.QuestionnaireId,
                                    QuestionId = answer.QuestionId,
                                    AnswerValue = answer.AnswerValue.Value,
                                    StudentId = answer.StudentId,
                                    FillDateTime = answer.FillDateTime
                                });
                            }
                            break;

                        default:
                            throw new ArgumentOutOfRangeException($"Unsupported question type: {answer.Type}");
                    }
                }
                transaction.Complete();
                return true.ToResultDto();
            }
        }
    }
}