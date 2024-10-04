using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IQuestionnaireRepository
    {
        Task<int> CreateAsync(QuestionnaireRepoModel questionnaire);
        Task<QuestionnaireRepoModel> GetByIdAsync(int id);
        Task<IEnumerable<QuestionnaireRepoModel>> GetAllAsync();
        Task UpdateAsync(QuestionnaireRepoModel questionnaire);
        Task DeleteAsync(int id);
    }
}