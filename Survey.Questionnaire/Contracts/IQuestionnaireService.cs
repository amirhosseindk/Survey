using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IQuestionnaireService
    {
        Task<int> CreateQuestionnaireAsync(Questionnaire questionnaire);
        Task<Questionnaire> GetQuestionnaireByIdAsync(int id);
        Task<Questionnaire> GetQuestionnaireByTitleAsync(string title);
        Task<IEnumerable<Questionnaire>> GetAllQuestionnairesAsync();
        Task<IEnumerable<Questionnaire>> GetAllQuestionnairesByProfessorIdAsync(string id);
        Task<IEnumerable<Questionnaire>> GetAllQuestionnairesByClassIdAsync(int classId);
        Task<IEnumerable<Questionnaire>> GetAllQuestionnairesByStudentIdAsync(string studentId);
        Task<bool> UpdateQuestionnaireAsync(Questionnaire questionnaire);
        Task<bool> DeleteQuestionnaireAsync(int id);
    }
}