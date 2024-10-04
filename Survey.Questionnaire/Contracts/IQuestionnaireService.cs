using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IQuestionnaireService
    {
        Task<int> CreateQuestionnaireAsync(Questionnaire questionnaire);
        Task<(QuestionnaireRepoModel questionnaire, List<QuestionRepoModel> questions, List<MultipleChoiceOptionRepoModel>? options)> GetQuestionnaireByIdAsync(int id);
        Task UpdateQuestionnaireAsync(QuestionnaireRepoModel questionnaire, List<QuestionRepoModel> questions, List<MultipleChoiceOptionRepoModel>? options = null);
        Task DeleteQuestionnaireAsync(int id);
    }
}