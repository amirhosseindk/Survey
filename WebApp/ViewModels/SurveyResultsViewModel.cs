namespace WebApp.ViewModels
{
    public class SurveyResultsViewModel
    {
        public Survey.Application.Dtos.Questionnaires.QuestionnaireDto Questionnaire { get; set; }
        public List<MultipleChoiceResult> MultipleChoiceResults { get; set; }
        public List<TextQuestionResult> TextQuestionResults { get; set; }
        public List<RangeQuestionResult> RangeQuestionResults { get; set; }
        public List<DegreeQuestionResult> DegreeQuestionResults { get; set; }
        public int TotalStudents { get; set; }
        public int AnsweredStudents { get; set; }
    }
}