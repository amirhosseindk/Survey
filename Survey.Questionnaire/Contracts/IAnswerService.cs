using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IAnswerService
    {
        Task SaveAnswersAsync(
            List<DegreeQuestionAnswer>? degreeAnswers = null,
            List<MultipleChoiceQuestionAnswer>? multipleChoiceAnswers = null,
            List<RangeQuestionAnswer>? rangeAnswers = null,
            List<TextQuestionAnswer>? textAnswers = null);

        Task<IEnumerable<DegreeQuestionAnswer>> GetDegreeAnswersByQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetMultipleChoiceAnswersByQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<RangeQuestionAnswer>> GetRangeAnswersByQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<TextQuestionAnswer>> GetTextAnswersByQuestionnaireIdAsync(int questionnaireId);
    }
}