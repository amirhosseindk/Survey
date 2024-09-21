using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IQuestionnaireService
    {
        Task<int> CreateQuestionnaireAsync(Questionnaire questionnaire, List<Question> questions, List<MultipleChoiceOption>? options = null);
        Task<(Questionnaire questionnaire, List<Question> questions, List<MultipleChoiceOption>? options)> GetQuestionnaireByIdAsync(int id);
        Task UpdateQuestionnaireAsync(Questionnaire questionnaire, List<Question> questions, List<MultipleChoiceOption>? options = null);
        Task DeleteQuestionnaireAsync(int id);
    }
}