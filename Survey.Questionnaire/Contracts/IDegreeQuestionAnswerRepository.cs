using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IDegreeQuestionAnswerRepository
    {
        Task<int> CreateAsync(DegreeQuestionAnswerRepoModel answer);
        Task<DegreeQuestionAnswerRepoModel> GetByIdAsync(int id);
        Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetByQuestionIdAsync(int questionId);
        Task UpdateAsync(DegreeQuestionAnswerRepoModel answer);
        Task DeleteAsync(int id);
        Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId);
    }
}