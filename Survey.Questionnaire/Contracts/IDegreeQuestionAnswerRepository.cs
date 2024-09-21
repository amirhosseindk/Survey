using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IDegreeQuestionAnswerRepository
    {
        Task<int> CreateAsync(DegreeQuestionAnswer answer);
        Task<DegreeQuestionAnswer> GetByIdAsync(int id);
        Task<IEnumerable<DegreeQuestionAnswer>> GetByQuestionIdAsync(int questionId);
        Task UpdateAsync(DegreeQuestionAnswer answer);
        Task DeleteAsync(int id);
        Task<IEnumerable<DegreeQuestionAnswer>> GetByQuestionnaireIdAsync(int questionnaireId);
    }
}