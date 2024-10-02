using Microsoft.AspNetCore.Mvc;

namespace Survey.Application.Dtos.Result
{
    public class ResultDto<T>
    {
        public bool Succeeded { get; set; }
        public T Result { get; set; }
        public ProblemDetails Error { get; set; }
    }

    public class ResultDto
    {
        public bool Succeeded { get; set; }
        public object Result { get; set; }
        public ProblemDetails Error { get; set; }
    }
}