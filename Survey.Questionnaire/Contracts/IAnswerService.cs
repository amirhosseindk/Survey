using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Contracts
{
    public interface IAnswerService
    {
        Task<IEnumerable<DegreeQuestionAnswer>> GetDegreeAnswersByQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetMultipleChoiceAnswersByQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<RangeQuestionAnswer>> GetRangeAnswersByQuestionnaireIdAsync(int questionnaireId);
        Task<IEnumerable<TextQuestionAnswer>> GetTextAnswersByQuestionnaireIdAsync(int questionnaireId);
        Task<bool> CreateMultipleChoiceAnswerAsync(MultipleChoiceQuestionAnswer answer);
        Task<bool> CreateTextAnswerAsync(TextQuestionAnswer answer);
        Task<bool> CreateRangeAnswerAsync(RangeQuestionAnswer answer);
        Task<bool> CreateDegreeAnswerAsync(DegreeQuestionAnswer answer);
        Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetMultipleChoiceAnswersByQuestionIdAsync(int questionnnaireId, int questionId);
        Task<IEnumerable<TextQuestionAnswer>> GetTextAnswersByQuestionIdAsync(int questionnnaireId, int questionId);
        Task<IEnumerable<RangeQuestionAnswer>> GetRangeAnswersByQuestionIdAsync(int questionnnaireId, int questionId);
        Task<IEnumerable<DegreeQuestionAnswer>> GetDegreeAnswersByQuestionIdAsync(int questionnnaireId, int questionId);
        Task<bool> UpdateMultipleChoiceAnswerAsync(MultipleChoiceQuestionAnswer answer);
        Task<bool> UpdateTextAnswerAsync(TextQuestionAnswer answer);
        Task<bool> UpdateRangeAnswerAsync(RangeQuestionAnswer answer);
        Task<bool> UpdateDegreeAnswerAsync(DegreeQuestionAnswer answer);
        Task<bool> DeleteMultipleChoiceAnswerAsync(MultipleChoiceQuestionAnswer answer);
        Task<bool> DeleteTextAnswerAsync(TextQuestionAnswer answer);
        Task<bool> DeleteRangeAnswerAsync(RangeQuestionAnswer answer);
        Task<bool> DeleteDegreeAnswerAsync(DegreeQuestionAnswer answer);
        Task<IEnumerable<DegreeQuestionAnswer>> GetDegreeAnswersAsync(int questionnaireId, string studentId);
        Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetMultipleChoiceAnswersAsync(int questionnaireId, string studentId);
        Task<IEnumerable<RangeQuestionAnswer>> GetRangeAnswersAsync(int questionnaireId, string studentId);
        Task<IEnumerable<TextQuestionAnswer>> GetTextAnswersAsync(int questionnaireId, string studentId);
    }
}