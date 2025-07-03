namespace ExamSystem.Application
{
    public record UpdateStudentDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public int OrderNumber { get; set; }
        public int Grade { get; set; }
    }
}
