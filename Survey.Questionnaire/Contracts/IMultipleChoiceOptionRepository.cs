using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IMultipleChoiceOptionRepository
    {
        Task<int> CreateAsync(MultipleChoiceOptionRepoModel option);
        Task<MultipleChoiceOptionRepoModel> GetByIdAsync(int id);
        Task<IEnumerable<MultipleChoiceOptionRepoModel>> GetByQuestionIdAsync(int questionId);
        Task UpdateAsync(MultipleChoiceOptionRepoModel option);
        Task DeleteAsync(int id);
        Task<IEnumerable<MultipleChoiceOptionRepoModel>> GetAllAsync();
    }
}