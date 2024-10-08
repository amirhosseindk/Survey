using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IRangeQuestionAnswerRepository
    {
        Task<int> CreateAsync(RangeQuestionAnswerRepoModel answer);
        Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetAllAnswersOfQuestionIdAsync(int questionnnaireId, int questionId);
        Task<bool> UpdateAsync(RangeQuestionAnswerRepoModel answer);
        Task<bool> DeleteAsync(RangeQuestionAnswerRepoModel answer);
        Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetAllOfQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetAllAnswersAsync(int QuestionnaireId, string StudentId);
    }
}