using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IMultipleChoiceQuestionAnswerRepository
    {
        Task<int> CreateAsync(MultipleChoiceQuestionAnswerRepoModel answer);
        Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetAllAnswersOfQuestionIdAsync(int questionnnaireId, int questionId);
        Task<bool> UpdateAsync(MultipleChoiceQuestionAnswerRepoModel answer);
        Task<bool> DeleteAsync(MultipleChoiceQuestionAnswerRepoModel answer);
        Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetAllOfQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetAllAnswersAsync(int QuestionnaireId, string StudentId);
    }
}