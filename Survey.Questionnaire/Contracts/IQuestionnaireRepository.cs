using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IQuestionnaireRepository
    {
        Task<int> CreateAsync(Questionnaire questionnaire);
        Task<Questionnaire> GetByIdAsync(int id);
        Task<IEnumerable<Questionnaire>> GetAllAsync();
        Task UpdateAsync(Questionnaire questionnaire);
        Task DeleteAsync(int id);
    }
}