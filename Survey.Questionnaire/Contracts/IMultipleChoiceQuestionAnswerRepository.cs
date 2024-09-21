using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IMultipleChoiceQuestionAnswerRepository
    {
        Task<int> CreateAsync(MultipleChoiceQuestionAnswer answer);
        Task<MultipleChoiceQuestionAnswer> GetByIdAsync(int id);
        Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetByQuestionIdAsync(int questionId);
        Task UpdateAsync(MultipleChoiceQuestionAnswer answer);
        Task DeleteAsync(int id);
    }
}