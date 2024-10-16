using Survey.Application.Dtos.Answer;

namespace WebApp.Models
{
    public class AnswerDto
    {
        public List<CreateAnswerDto> CreateAnswers { get; set; }
        public List<UpdateAnswerDto>? UpdateAnswers { get; set; }
    }
}