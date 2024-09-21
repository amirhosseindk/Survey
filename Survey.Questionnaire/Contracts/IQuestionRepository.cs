using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IQuestionRepository
    {
        Task<int> CreateAsync(Question question);
        Task<Question> GetByIdAsync(int id);
        Task<IEnumerable<Question>> GetByQuestionnaireIdAsync(int questionnaireId);
        Task UpdateAsync(Question question);
        Task DeleteAsync(int id);
    }
}