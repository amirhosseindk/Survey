using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface ITextQuestionAnswerRepository
    {
        Task<int> CreateAsync(TextQuestionAnswerRepoModel answer);
        Task<IEnumerable<TextQuestionAnswerRepoModel>> GetByQuestionIdAsync(int questionId);
        Task UpdateAsync(TextQuestionAnswerRepoModel answer);
        Task DeleteAsync(int id);
        Task<IEnumerable<TextQuestionAnswerRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId);
    }
}