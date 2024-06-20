using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class DegreeQuestion : Question
    {
        public DegreeQuestion(Int16 rank, string title)
        {
            Rank = rank;
            Title = title;
            Type = QuestionType.Degree;
        }

        [Range(0, 5)]
        public Int16 Answer { get; set; }
    }
}