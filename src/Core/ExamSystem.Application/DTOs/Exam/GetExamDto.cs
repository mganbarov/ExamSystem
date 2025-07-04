namespace ExamSystem.Application
{
    public record GetExamDTO(int Id,DateTime ExamDate, string LessonName,string StudentName,string StudentSurname,int ExamScore,int StudentNumber, string LessonCode);
    
}
