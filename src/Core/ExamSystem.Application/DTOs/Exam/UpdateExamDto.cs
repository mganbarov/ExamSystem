
namespace ExamSystem.Application
{
    public record UpdateExamDTO
    {
        public int Id { get; set; }
        public DateTime ExamDate { get; set; }
        public int ExamScore { get; set; }
        public int StudentNumber { get; set; }
        public string LessonCode { get; set; }
    }
}
