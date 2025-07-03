namespace ExamSystem.Application.DTOs.Exam
{
    public record CreateExamDTO
    {
        public DateTime ExamDate { get; set; }
        public int ExamScore { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
    }
}
