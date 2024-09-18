using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class RangeQuestion : Question
    {
        public RangeQuestion() { }

        public RangeQuestion(short rank, string title)
        {
            Rank = rank;
            Title = title;
            Type = QuestionType.Range;
        }

        [Range(0, 100)]
        public short MinValue { get; set; } = 0;
        public short MaxValue { get; set; } = 100;
    }
}