using Survey.Application.Dtos.Questions;

namespace Survey.Application.Dtos.Questionnaires
{
    public class QuestionnaireDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string ProfessorId { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}