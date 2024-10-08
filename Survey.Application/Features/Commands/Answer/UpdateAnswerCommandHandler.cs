using MediatR;
using Survey.Application.Dtos.Answer;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;
using System.Transactions;

namespace Survey.Application.Features.Commands.Answer
{
    public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, ResultDto<bool>>
    {
        private readonly IAnswerService _answerService;

        public UpdateAnswerCommandHandler(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        public async Task<ResultDto<bool>> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
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

        private async Task ProcessAnswerAsync(UpdateAnswerDto answer, int questionnaireId)
        {
            if (answer.IsDeleted)
            {
                await DeleteAnswerAsync(answer, questionnaireId);
            }
            else
            {
                switch (answer.Type)
                {
                    case 0:
                        await _answerService.UpdateTextAnswerAsync(new TextQuestionAnswer
                        {
                            QuestionnaireId = questionnaireId,
                            QuestionId = answer.QuestionId,
                            AnswerText = answer.AnswerText,
                            StudentId = answer.StudentId,
                            FillDateTime = answer.FillDateTime
                        });
                        break;
                    case 1:
                        await _answerService.UpdateMultipleChoiceAnswerAsync(new MultipleChoiceQuestionAnswer
                        {
                            QuestionnaireId = questionnaireId,
                            QuestionId = answer.QuestionId,
                            AnswerOptionId = answer.AnswerOptionId.Value,
                            StudentId = answer.StudentId,
                            FillDateTime = answer.FillDateTime
                        });
                        break;
                    case 2:
                        await _answerService.UpdateRangeAnswerAsync(new RangeQuestionAnswer
                        {
                            QuestionnaireId = questionnaireId,
                            QuestionId = answer.QuestionId,
                            AnswerValue = (short)answer.AnswerValue.Value,
                            StudentId = answer.StudentId,
                            FillDateTime = answer.FillDateTime
                        });
                        break;
                    case 3:
                        await _answerService.UpdateDegreeAnswerAsync(new DegreeQuestionAnswer
                        {
                            QuestionnaireId = questionnaireId,
                            QuestionId = answer.QuestionId,
                            AnswerValue = (short)answer.AnswerValue.Value,
                            StudentId = answer.StudentId,
                            FillDateTime = answer.FillDateTime
                        });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Unsupported question type: {answer.Type}");
                }
            }
        }

        private async Task DeleteAnswerAsync(UpdateAnswerDto answer, int questionnaireId)
        {
            switch (answer.Type)
            {
                case 0:
                    await _answerService.DeleteTextAnswerAsync(new TextQuestionAnswer
                    {
                        QuestionnaireId = questionnaireId,
                        QuestionId = answer.QuestionId,
                        StudentId = answer.StudentId
                    });
                    break;
                case 1:
                    await _answerService.DeleteMultipleChoiceAnswerAsync(new MultipleChoiceQuestionAnswer
                    {
                        QuestionnaireId = questionnaireId,
                        QuestionId = answer.QuestionId,
                        StudentId = answer.StudentId
                    });
                    break;
                case 2:
                    await _answerService.DeleteRangeAnswerAsync(new RangeQuestionAnswer
                    {
                        QuestionnaireId = questionnaireId,
                        QuestionId = answer.QuestionId,
                        StudentId = answer.StudentId
                    });
                    break;
                case 3:
                    await _answerService.DeleteDegreeAnswerAsync(new DegreeQuestionAnswer
                    {
                        QuestionnaireId = questionnaireId,
                        QuestionId = answer.QuestionId,
                        StudentId = answer.StudentId
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unsupported question type: {answer.Type}");
            }
        }
    }
}