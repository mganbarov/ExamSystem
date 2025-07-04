namespace ExamSystem.Application
{
    public record GetLessonDto(
    int Id,
    string Code,
    string Name,
    int Grade,
    string TeacherFirstName,
    string TeacherSurname);
    
}
