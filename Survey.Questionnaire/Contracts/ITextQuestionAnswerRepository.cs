using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface ITextQuestionAnswerRepository
    {
        Task<int> CreateAsync(TextQuestionAnswer answer);
        Task<TextQuestionAnswer> GetByIdAsync(int id);
        Task<IEnumerable<TextQuestionAnswer>> GetByQuestionIdAsync(int questionId);
        Task UpdateAsync(TextQuestionAnswer answer);
        Task DeleteAsync(int id);
    }
}