using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public int? AnswerOptionId { get; set; }
        public string StudentId { get; set; }
        public User Student { get; set; }
    }
}