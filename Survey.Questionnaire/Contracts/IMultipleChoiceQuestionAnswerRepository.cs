using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IMultipleChoiceQuestionAnswerRepository
    {
        Task<int> CreateAsync(MultipleChoiceQuestionAnswerRepoModel answer);
        Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetByQuestionIdAsync(int questionId);
        Task UpdateAsync(MultipleChoiceQuestionAnswerRepoModel answer);
        Task DeleteAsync(int id);
        Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId);
    }
}