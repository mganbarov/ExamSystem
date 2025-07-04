using ExamSystem.Application.Abstraction.Repositories.Generic;
using ExamSystem.Domain;

namespace ExamSystem.Application.Abstraction.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<Lesson?> GetByCodeAsync(string code);
        Task<ICollection<Lesson>> GetByGradeAsync(int grade);
        IQueryable<Lesson> GetAllWithFilter(int skip, int take, int? grade = null);
        Task<bool> HasExamsAsync(int lessonId);
    }
}
