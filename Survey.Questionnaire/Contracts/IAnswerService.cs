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
        Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetMultipleChoiceAnswersByQuestionIdAsync(int questionId);
        Task<IEnumerable<TextQuestionAnswer>> GetTextAnswersByQuestionIdAsync(int questionId);
        Task<IEnumerable<RangeQuestionAnswer>> GetRangeAnswersByQuestionIdAsync(int questionId);
        Task<IEnumerable<DegreeQuestionAnswer>> GetDegreeAnswersByQuestionIdAsync(int questionId);
        Task UpdateMultipleChoiceAnswerAsync(MultipleChoiceQuestionAnswer answer);
        Task UpdateTextAnswerAsync(TextQuestionAnswer answer);
        Task UpdateRangeAnswerAsync(RangeQuestionAnswer answer);
        Task UpdateDegreeAnswerAsync(DegreeQuestionAnswer answer);
        Task DeleteMultipleChoiceAnswerAsync(int id);
        Task DeleteTextAnswerAsync(int id);
        Task DeleteRangeAnswerAsync(int id);
        Task DeleteDegreeAnswerAsync(int id);
    }
}