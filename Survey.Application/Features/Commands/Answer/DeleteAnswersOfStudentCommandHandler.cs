using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Application.Features.Commands.Answer
{
    public class DeleteAnswersOfStudentCommandHandler : IRequestHandler<DeleteAnswersOfStudentCommand, ResultDto<bool>>
    {
        private readonly IAnswerService _answerService;

        public DeleteAnswersOfStudentCommandHandler(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        public async Task<ResultDto<bool>> Handle(DeleteAnswersOfStudentCommand request, CancellationToken cancellationToken)
        {
            var textAnswers = new List<TextQuestionAnswer>();
            var multipleChoiceAnswers = new List<MultipleChoiceQuestionAnswer>();
            var rangeAnswers = new List<RangeQuestionAnswer>();
            var degreeAnswers = new List<DegreeQuestionAnswer>();

            textAnswers.AddRange(await _answerService.GetTextAnswersAsync(request.QuestionnaireId, request.StudentId));
            multipleChoiceAnswers.AddRange(await _answerService.GetMultipleChoiceAnswersAsync(request.QuestionnaireId, request.StudentId));
            rangeAnswers.AddRange(await _answerService.GetRangeAnswersAsync(request.QuestionnaireId, request.StudentId));
            degreeAnswers.AddRange(await _answerService.GetDegreeAnswersAsync(request.QuestionnaireId, request.StudentId));

            var result = true;

            foreach (var answer in textAnswers) 
            {
                result = await _answerService.DeleteTextAnswerAsync(answer);
                if (!result)
                    return result.ToResultDto();
            }

            foreach (var answer in multipleChoiceAnswers)
            {
                result = await _answerService.DeleteMultipleChoiceAnswerAsync(answer);
                if (!result)
                    return result.ToResultDto();
            }

            foreach (var answer in rangeAnswers)
            {
                result = await _answerService.DeleteRangeAnswerAsync(answer);
                if (!result)
                    return result.ToResultDto();
            }

            foreach (var answer in degreeAnswers)
            {
                result = await _answerService.DeleteDegreeAnswerAsync(answer);
                if (!result)
                    return result.ToResultDto();
            }

            return result.ToResultDto();
        }
    }
}