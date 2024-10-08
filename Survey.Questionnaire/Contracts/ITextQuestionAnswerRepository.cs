using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface ITextQuestionAnswerRepository
    {
        Task<int> CreateAsync(TextQuestionAnswerRepoModel answer);
        Task<IEnumerable<TextQuestionAnswerRepoModel>> GetAllAnswersOfQuestionIdAsync(int questionnnaireId, int questionId);
        Task<bool> UpdateAsync(TextQuestionAnswerRepoModel answer);
        Task<bool> DeleteAsync(TextQuestionAnswerRepoModel answer);
        Task<IEnumerable<TextQuestionAnswerRepoModel>> GetAllOfQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<TextQuestionAnswerRepoModel>> GetAllAnswersAsync(int QuestionnaireId, string StudentId);
    }
}