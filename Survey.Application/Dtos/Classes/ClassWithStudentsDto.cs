namespace Survey.Application.Dtos.Classes
{
    public class ClassWithStudentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public Dictionary<string, string> Students { get; set; }
    }
}