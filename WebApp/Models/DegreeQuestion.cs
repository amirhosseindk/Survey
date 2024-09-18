using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class DegreeQuestion : Question
    {
        public DegreeQuestion() { }

        public DegreeQuestion(short rank, string title)
        {
            Rank = rank;
            Title = title;
            Type = QuestionType.Degree;
        }

        [Range(0, 5)]
        public short MaxDegree { get; set; } = 5;
    }
}