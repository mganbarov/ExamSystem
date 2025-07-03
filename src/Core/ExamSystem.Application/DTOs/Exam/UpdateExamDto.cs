
namespace ExamSystem.Application
{
    public record UpdateExamDTO
    {
        public int Id { get; set; }
        public DateTime ExamDate { get; set; }
        public int ExamScore { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
    }
}
