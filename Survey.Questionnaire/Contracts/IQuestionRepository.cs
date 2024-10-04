using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IQuestionRepository
    {
        Task<int> CreateAsync(QuestionRepoModel question);
        Task<QuestionRepoModel> GetByIdAsync(int id);
        Task<IEnumerable<QuestionRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId);
        Task UpdateAsync(QuestionRepoModel question);
        Task DeleteAsync(int id);
    }
}