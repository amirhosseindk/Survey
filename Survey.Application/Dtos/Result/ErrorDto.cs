namespace Survey.Application.Dtos.Result
{
    public class ErrorDto
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Details { get; set; }
    }
}