
namespace ExamSystem.Application
{
    public record CreateStudentDto
    {
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int Grade { get; set; }
        public int OrderNumber { get; set; }
    }
}
