using ExamSystem.Application.Abstraction.Repositories.Generic;
using ExamSystem.Domain;

namespace ExamSystem.Application.Abstraction.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetByNumberAsync(int number);
        Task<ICollection<Student>> GetByGradeAsync(int grade);
        IQueryable<Student> GetAllWithFilter(int skip, int take, int? grade = null);
        Task<bool> HasExamsAsync(int studentId);
        Task<ICollection<Exam>> GetStudentExamResultsAsync(int studentId);
    }
}
