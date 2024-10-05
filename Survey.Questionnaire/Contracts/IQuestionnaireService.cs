using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IQuestionnaireService
    {
        Task<int> CreateQuestionnaireAsync(Questionnaire questionnaire);
        Task<(QuestionnaireRepoModel questionnaire, List<QuestionRepoModel> questions, List<MultipleChoiceOptionRepoModel>? options)> GetQuestionnaireByIdAsync(int id);
        Task<bool> UpdateQuestionnaireAsync(Questionnaire questionnaire);
        Task DeleteQuestionnaireAsync(int id);
    }
}