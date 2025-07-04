using ExamSystem.Application.Abstraction.Repositories.Generic;
using ExamSystem.Domain;

namespace ExamSystem.Application.Abstraction.Repositories
{
    public interface IExamRepository : IRepository<Exam>
    {
        Task<Exam?> GetByStudentAndLessonAsync(int studentId, int lessonId);
    }
}
