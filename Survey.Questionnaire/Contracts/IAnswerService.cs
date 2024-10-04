using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IAnswerService
    {
        Task SaveAnswersAsync(
            List<DegreeQuestionAnswerRepoModel>? degreeAnswers = null,
            List<MultipleChoiceQuestionAnswerRepoModel>? multipleChoiceAnswers = null,
            List<RangeQuestionAnswerRepoModel>? rangeAnswers = null,
            List<TextQuestionAnswerRepoModel>? textAnswers = null);

        Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetDegreeAnswersByQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetMultipleChoiceAnswersByQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetRangeAnswersByQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<TextQuestionAnswerRepoModel>> GetTextAnswersByQuestionnaireIdAsync(int questionnaireId);
    }
}