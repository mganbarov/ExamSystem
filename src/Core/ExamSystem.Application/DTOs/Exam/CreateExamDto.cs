namespace ExamSystem.Application.DTOs.Exam
{
    public record CreateExamDTO
    {
        public DateTime? ExamDate { get; set; }
        public int ExamScore { get; set; }
        public int StudentNumber { get; set; }
        public string LessonCode { get; set; } = null!;
    }
}
