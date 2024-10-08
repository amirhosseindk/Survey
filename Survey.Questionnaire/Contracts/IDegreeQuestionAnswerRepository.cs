using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IDegreeQuestionAnswerRepository
    {
        Task<int> CreateAsync(DegreeQuestionAnswerRepoModel answer);
        Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetAllAnswersOfQuestionIdAsync(int questionnnaireId, int questionId);
        Task<bool> UpdateAsync(DegreeQuestionAnswerRepoModel answer);
        Task<bool> DeleteAsync(DegreeQuestionAnswerRepoModel answer);
        Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetAllOfQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetAllAnswersAsync(int QuestionnaireId, string StudentId);
    }
}