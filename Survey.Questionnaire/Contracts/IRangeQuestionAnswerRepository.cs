using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IRangeQuestionAnswerRepository
    {
        Task<int> CreateAsync(RangeQuestionAnswer answer);
        Task<RangeQuestionAnswer> GetByIdAsync(int id);
        Task<IEnumerable<RangeQuestionAnswer>> GetByQuestionIdAsync(int questionId);
        Task UpdateAsync(RangeQuestionAnswer answer);
        Task DeleteAsync(int id);
    }
}