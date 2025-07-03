namespace ExamSystem.Application
{
    public record CreateLessonDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Grade { get; set; }
        public string TeacherFirstName { get; set; } = null!;
        public string TeacherLastName { get; set; } = null!;
    }
}
