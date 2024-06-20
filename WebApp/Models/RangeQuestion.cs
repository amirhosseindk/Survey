using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class RangeQuestion : Question
    {
        public RangeQuestion(Int16 rank, string title) 
        {
            Rank = rank;
            Title = title;
            Type = QuestionType.Range;
        }

        [Range(0,100)]
        public Int16 Answer { get; set; }
    }
}