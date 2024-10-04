using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IRangeQuestionAnswerRepository
    {
        Task<int> CreateAsync(RangeQuestionAnswerRepoModel answer);
        Task<RangeQuestionAnswerRepoModel> GetByIdAsync(int id);
        Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetByQuestionIdAsync(int questionId);
        Task UpdateAsync(RangeQuestionAnswerRepoModel answer);
        Task DeleteAsync(int id);
        Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId);
    }
}