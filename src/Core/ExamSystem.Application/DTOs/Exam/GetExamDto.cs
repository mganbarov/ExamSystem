namespace ExamSystem.Application
{
    public record GetExamDTO(int Id,DateTime ExamDate, int ExamScore, GetStudentDto Student, GetLessonDto Lesson);
    
}
