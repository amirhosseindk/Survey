using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IMultipleChoiceOptionRepository
    {
        Task<int> CreateAsync(MultipleChoiceOption option);
        Task<MultipleChoiceOption> GetByIdAsync(int id);
        Task<IEnumerable<MultipleChoiceOption>> GetByQuestionIdAsync(int questionId);
        Task UpdateAsync(MultipleChoiceOption option);
        Task DeleteAsync(int id);
    }
}