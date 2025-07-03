namespace ExamSystem.Application
{
    public record UpdateLessonDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Grade { get; set; }
        public string TeacherFirstName { get; set; } = null!;
        public string TeacherLastName { get; set; } = null!;
    }
}
