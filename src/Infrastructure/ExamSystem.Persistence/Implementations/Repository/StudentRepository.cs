using ExamSystem.Application.Abstraction.Repositories;
using ExamSystem.Domain;
using ExamSystem.Persistence.Context;
using ExamSystem.Persistence.Implementations.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Persistence.Implementations.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Student?> GetByNumberAsync(int number)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.OrderNumber == number);
        }

        public async Task<ICollection<Student>> GetByGradeAsync(int grade)
        {
            return await _context.Students
                .Where(s => s.Grade == grade)
                .OrderBy(s => s.Surname)
                .ThenBy(s => s.FirstName)
                .ToListAsync();
        }

        public IQueryable<Student> GetAllWithFilter(int skip, int take, int? grade = null)
        {
            var query = _context.Students.AsQueryable();

            if (grade.HasValue)
                query = query.Where(s => s.Grade == grade.Value);

            return query.OrderBy(s => s.Grade)
                       .ThenBy(s => s.Surname)
                       .ThenBy(s => s.FirstName)
                       .Skip(skip)
                       .Take(take);
        }

        public async Task<bool> HasExamsAsync(int studentId)
        {
            return await _context.Exams
                .AnyAsync(e => e.StudentId == studentId);
        }

        public async Task<ICollection<Exam>> GetStudentExamResultsAsync(int studentId)
        {
            return await _context.Exams
                .Include(e => e.Lesson)
                .Where(e => e.StudentId == studentId)
                .OrderBy(e => e.ExamDate)
                .ToListAsync();
        }
    }
}
