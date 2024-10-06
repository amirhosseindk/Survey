using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IQuestionnaireRepository
    {
        Task<int> CreateAsync(QuestionnaireRepoModel questionnaire);
        Task<QuestionnaireRepoModel> GetByIdAsync(int id);
        Task<QuestionnaireRepoModel> GetByTitleAsync(string title);
        Task<IEnumerable<QuestionnaireRepoModel>> GetAllAsync();
        Task<IEnumerable<QuestionnaireRepoModel>> GetAllByProfessorIdAsync(string id);
        Task<IEnumerable<QuestionnaireRepoModel>> GetAllByClassIdAsync(int classId);
        Task<IEnumerable<QuestionnaireRepoModel>> GetAllByStudentIdAsync(string studentId);
        Task<bool> UpdateAsync(QuestionnaireRepoModel questionnaire);
        Task<bool> DeleteAsync(int id);
    }
}